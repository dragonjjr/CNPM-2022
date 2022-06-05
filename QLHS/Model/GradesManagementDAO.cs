using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHS.Model
{
    public class GradesManagementDAO
    {
        private QLHSEntities db;
        public GradesManagementDAO()
        {
            db = new QLHSEntities();
        }
        public List<GradesVM> GetGradesList(int ClassID, int SemeterID, int SubjectID)
        {
            var list = (from st in db.tb_Students
                        join ts in db.tb_TranScripts.DefaultIfEmpty() on st.ID equals ts.StudentID
                        join sm in db.tb_Semesters.DefaultIfEmpty() on ts.SemesterID equals sm.ID
                        join sb in db.tb_Subjects.DefaultIfEmpty() on ts.SubjectID equals sb.ID
                        where(ts.IsDeleted == false && st.IsDeleted == false && ts.SemesterID == SemeterID && ts.SubjectID == SubjectID)
                        select new GradesVM
                        {
                            ID = ts.ID,
                            StudentID = st.ID,
                            Name = st.Name,
                            SemesterID = ts.SemesterID ?? null,
                            Semester = sm.Name ?? null,
                            SubjectID = ts.SubjectID ?? null,
                            SubjectName = sb.Name ?? null,
                            Grade15 = ts.Grade15 ?? null,
                            Grade45 = ts.Grade45 ?? null,
                            GradeSemester = ts.GradeSemester ?? null
                        }).ToList();
            if(list.Count > 0)
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

        public ObservableCollection<tb_Students> GetInfoStudentsOfClass(int classId)
        {
            var list = new ObservableCollection<tb_Students>(db.tb_Students.Where(st => st.ClassID == classId && st.IsDeleted == false).ToList());

            return list;
        }

        public ObservableCollection<tb_Semesters> GetInfoSemeters()
        {
            var list = new ObservableCollection<tb_Semesters>(db.tb_Semesters.Where(s => s.IsDeleted == false).ToList());

            return list;
        }


        public bool AddGrades(tb_TranScripts ts)
        {
            db.tb_TranScripts.Add(ts);
            return db.SaveChanges() > 0;
        }


        public bool UpdateGrades(tb_TranScripts ts)
        {
            var model = db.tb_TranScripts.Where(x => x.ID == ts.ID && x.IsDeleted == false).Single();
            model.Grade15 = ts.Grade15;
            model.Grade45 = ts.Grade45;
            model.GradeSemester = ts.GradeSemester;
            model.LastUpdatedDate = DateTime.Now;

            db.Entry(model).State = System.Data.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public bool CheckGrades(tb_TranScripts ts)
        {
            var model = db.tb_TranScripts.Where(x => x.StudentID == ts.StudentID && x.SemesterID == ts.SemesterID && x.SubjectID == ts.SubjectID && x.IsDeleted == false).ToList();
            if(model.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteGrades(int ID)
        {
            var student = db.tb_TranScripts.First(x => x.ID == ID);
            student.IsDeleted = true;

            return db.SaveChanges() > 0;
        }
    }

    public class GradesVM
    {
        public int STT { get; set; }
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int? SemesterID { get; set; }
        public string Semester { get; set; }
        public int? SubjectID { get; set; }
        public string SubjectName { get; set; }
        public double? Grade15 { get; set; }
        public double? Grade45 { get; set; }
        public double? GradeSemester { get; set; }
    }
}
