using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHS.Model
{
    public class ReportDAO
    {
        private QLHSEntities db;
        public ReportDAO()
        {
            db = new QLHSEntities();
        }
        public List<ReportVM> BaoCaoTongKetMon(int SubjectID, int SemeterID)
        {
            var sl = SoluongDatMon(SubjectID, SemeterID);
            var tl = TiLeDatMon(SubjectID, SemeterID);
            var list = (from sb in db.tb_Subjects 
                        join ts in db.tb_TranScripts on sb.ID equals ts.SubjectID
                        join st in db.tb_Students on ts.StudentID equals st.ID
                        join cl in db.tb_Class.DefaultIfEmpty() on st.ClassID equals cl.ID
                        join sm in db.tb_Semesters.DefaultIfEmpty() on ts.SemesterID equals sm.ID
                        where (ts.IsDeleted == false && st.IsDeleted == false && ts.SemesterID == SemeterID && ts.SubjectID == SubjectID)
                        select new ReportVM
                        {
                            ClassID = st.ClassID,
                            ClassName = cl.Name,
                            SiSo = cl.Quantity,
                            SoLuongDat = sl,
                            TiLeDat = tl
                        }).Distinct().ToList();
            if (list.Count > 0)
            {
                int i = 1;
                foreach (var item in list)
                {
                    item.STT = i;
                    i++;
                }
            }
            return list;
        }


        public List<ReportVM> BaoCaoTongKetHocKy(int SemeterID)
        {
            var sl = SoluongDatHK(SemeterID);
            var tl = TiLeDatHK(SemeterID);
            var list = (from sm in db.tb_Semesters
                        join ts in db.tb_TranScripts.DefaultIfEmpty() on sm.ID equals ts.SemesterID
                        join st in db.tb_Students on ts.StudentID equals st.ID
                        join cl in db.tb_Class.DefaultIfEmpty() on st.ClassID equals cl.ID
                        where (ts.IsDeleted == false && st.IsDeleted == false && ts.SemesterID == SemeterID)
                        select new ReportVM
                        {
                            ClassID = st.ClassID,
                            ClassName = cl.Name,
                            SiSo = cl.Quantity,
                            SoLuongDat = sl,
                            TiLeDat = tl
                        }).Distinct().ToList();
            if (list.Count > 0)
            {
                int i = 1;
                foreach (var item in list)
                {
                    item.STT = i;
                    i++;
                }
            }
            return list;
        }

        private int SoluongDatMon(int SubjectID, int SemeterID)//, int ClassID
        {
            var rs = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SubjectID == SubjectID && x.SemesterID == SemeterID && ((x.Grade15 + x.Grade45 * 2 + x.GradeSemester * 3) / 5) >= 5).ToList();
            return rs.Count;
        }
        private double TiLeDatMon(int SubjectID, int SemeterID)
        {
            var rs = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SubjectID == SubjectID && x.SemesterID == SemeterID && ((x.Grade15 + x.Grade45 * 2 + x.GradeSemester * 3) / 5) >= 5).ToList();
            var total = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SubjectID == SubjectID && x.SemesterID == SemeterID).ToList();
            if(total.Count != 0)
            {
                return Math.Round((rs.Count / total.Count) * 100.0);
            }
            return 0;
        }

        private int SoluongDatHK(int SemeterID)
        {
            var rs = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SemesterID == SemeterID && ((x.Grade15 + x.Grade45 * 2 + x.GradeSemester * 3) / 5) >= 5).Count();
            return rs;
        }
        private double TiLeDatHK(int SemeterID)
        {
            double rs = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SemesterID == SemeterID && ((x.Grade15 + x.Grade45 * 2 + x.GradeSemester * 3) / 5) >= 5).Count();
            double total = db.tb_TranScripts.Where(x => x.IsDeleted == false && x.SemesterID == SemeterID).Count();
            if (total != 0)
            {
                return Math.Round((rs / total) * 100.0);
            }
            return 0;
        }
    }
}

public class ReportVM
{
    public int STT { get; set; }
    public int? ClassID { get; set; }
    public string ClassName { get; set; }
    public int? SiSo { get; set; }
    public int SoLuongDat { get; set; }
    public double TiLeDat { get; set; }
}