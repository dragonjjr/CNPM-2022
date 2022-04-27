using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLHS.DB;

namespace QLHS.Model
{
    public class ClassManageDAO
    {
        private QLHSEntities db;

        public ObservableCollection<tb_Groups> GetInfoGrade()
        {
            var list = new ObservableCollection<tb_Groups>(db.tb_Groups.Where(gr => gr.IsDeleted == false).ToList());

            return list;
        }

        public ObservableCollection<tb_Class> GetInfoClass(int gradeId)
        {
            var list = new ObservableCollection<tb_Class>(db.tb_Class.Where(cl => cl.GroupID == gradeId && cl.IsDeleted == false).ToList());

            return list;
        }

        public tb_Class GetInfoClassByName(string name)
        {
            return db.tb_Class.FirstOrDefault(cl => cl.Name == name && cl.IsDeleted == false);
        }

        public ObservableCollection<tb_Students> GetInfoStudentsOfClass(int classId, string studentNameSearch)
        {
            var list = new ObservableCollection<tb_Students>(db.tb_Students.Where(st => st.ClassID == classId && st.IsDeleted == false && st.Name.Contains(studentNameSearch)).ToList());

            return list;
        }

        public ObservableCollection<tb_Students> GetInfoStudentsWithoutClass(string studentNameSearch)
        {
            var list = new ObservableCollection<tb_Students>(db.tb_Students.Where(st => st.ClassID == null && st.IsDeleted == false && st.Name.Contains(studentNameSearch)).ToList());

            return list;
        }

        public bool CheckQuantityStudentOfclass(int classId, int quantityStudentsAddToClass)
        {
            int? quantity = db.tb_Class.Where(cl => cl.ID == classId).Select(cl => cl.Quantity).Single();
            int quantityCurrent = db.tb_Students.Where(st => st.ClassID == classId).Count();
            return quantity >= (quantityCurrent + quantityStudentsAddToClass);
        }

        public bool AddStudentIntoClass(int classId, int studentId)
        {
            var student = db.tb_Students.Single(st => st.ID == studentId && st.IsDeleted == false);
            student.ClassID = classId;

            return db.SaveChanges() > 0;
        }

        public bool DeleteStudentFromClass(int studentId)
        {
            var student = db.tb_Students.Single(st => st.ID == studentId && st.IsDeleted == false);
            student.ClassID = null;

            return db.SaveChanges() > 0;
        }

        public bool AddInfoClass(tb_Class newClass)
        {
            db.tb_Class.Add(newClass);
            return db.SaveChanges() > 0;
        }

        public bool UpdateInfoNameClass(int classId, string newName)
        {
            var classInfo = db.tb_Class.Single(st => st.ID == classId && st.IsDeleted == false);
            classInfo.Name = newName;

            return db.SaveChanges() > 0;
        }

        public bool DeleteInfoClass(int classId)
        {
            var classInfo = db.tb_Class.Single(st => st.ID == classId);
            classInfo.IsDeleted = true;

            return db.SaveChanges() > 0;
        }

        public ClassManageDAO()
        {
            db = new QLHSEntities();
        }

    }
}
