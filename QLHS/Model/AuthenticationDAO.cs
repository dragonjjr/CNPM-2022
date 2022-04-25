using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLHS.Model
{
    public class AuthenticationDAO
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        private QLHSEntities DB;

        public AuthenticationDAO()
        {
            DB = new QLHSEntities();
        }
        public int Login(string username, string password)
        {
            // return 0 : Sai tên tài khoản
            // return 1 : Đăng nhập thành công
            // return 2 : Sai mật khẩu
            var user = DB.tb_Users.SingleOrDefault(u => u.Username == username && u.IsDeleted == false);


            if (user != null)
            {
                var sha1 = new SHA1CryptoServiceProvider();
                var bytesPwhashSalt = StringToByteArray(user.Password);
                //SHA1 hash -> 20 byte => bytesPwhashSalt - 20 = byteSalt
                var LenPwhash = 20;
                var bytesPwhash = new byte[LenPwhash];
                Array.Copy(bytesPwhashSalt, bytesPwhash, LenPwhash);

                //hash password user input
                var PwUserInput = ASCIIEncoding.ASCII.GetBytes(password);
                var bytesPwUserHash = sha1.ComputeHash(PwUserInput);

                // chuyển array byte to string
                var strPwhash = ArrByteToString(bytesPwhash);
                var strPwUserIpHash = ArrByteToString(bytesPwUserHash);

                // Đăng nhập thành công
                if (strPwhash == strPwUserIpHash)
                {
                    return 1;
                }

                // Sai mật khẩu
                else
                {
                    return 2;
                }
            }

            // tên đăng nhập không tồn tại
            else
            {
                return 0;
            }
        }

        public int Register(string username, string password, string confirmpass)
        {
            // return 0 : tài khoản đã tồn tại
            // return 1 : Đăng ký thành công
            // return 2 : Mật khẩu xác nhận lại ko đúng


            //chuyển mật khẩu sang mảng byte
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesPw = Encoding.ASCII.GetBytes(password);

            //salt và chuyển salt sang byte
            var salt = DateTime.Now.Millisecond.ToString();
            var bytesSalt = Encoding.ASCII.GetBytes(salt);

            //hash
            var bytesPwhashed = sha1.ComputeHash(bytesPw);

            //gắn salt lưu xuống database
            var bytesResult = new byte[bytesPwhashed.Length + bytesSalt.Length];
            Array.Copy(bytesPwhashed, bytesResult, bytesPwhashed.Length);
            Array.Copy(bytesSalt, 0, bytesResult, bytesPwhashed.Length, bytesSalt.Length);


            // chuyển array byte sang string
            var strPwHash = ArrByteToString(bytesResult);
            // Lấy Username kiểm tra tồn tại chưa
            var user = DB.tb_Users.SingleOrDefault(u => u.Username == username);

            // Tên tài khoản đã tồn tại
            if (user != null)
            {
                return 0;
            }
            else
            {
                // Đăng ký thành công
                if (confirmpass == password)
                {
                    tb_Users users = new tb_Users();
                    users.Username = username;
                    users.Password = strPwHash;
                    users.RoleID = 1;
                    users.CreatedDate = DateTime.Now;
                    users.LastUpdatedDate = DateTime.Now;
                    users.IsDeleted = false;

                    DB.tb_Users.Add(users);
                    DB.SaveChanges();

                    return 1;
                }

                // Mật khẩu xác nhận lại không đúng
                else
                {
                    return 2;
                }
            }

        }

        private string ArrByteToString(Byte[] arrByte)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in arrByte)
            {
                //get 4 bit first
                sb.Append(arrHexa[(b >> 4)]);
                //get 4 bit second
                sb.Append(arrHexa[(b & 15)]);
            }

            return sb.ToString();
        }

        private byte[] StringToByteArray(string str)
        {
            return Enumerable.Range(0, str.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
