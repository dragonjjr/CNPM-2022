using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLHS.DB;

namespace QLHS.Model
{
    public class LoginModel
    {
        public static bool Login(string UserName, string password)
        {
            QLHSEntities dbContext = new QLHSEntities();
            if(UserName != string.Empty && password != string.Empty)
            {
                var model = dbContext.tb_Users.Where(x => x.Username == UserName && x.Password == password).FirstOrDefault();
                if(model != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
