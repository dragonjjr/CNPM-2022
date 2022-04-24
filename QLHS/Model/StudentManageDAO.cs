using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHS.Model
{
    public class StudentManageDAO
    {
        private QLHSEntities DB;
        public StudentManageDAO()
        {
            DB = new QLHSEntities();
        }
        public ObservableCollection<tb_Students> ListStudentRecord()
        {
            var listStudentRecord = new ObservableCollection<tb_Students>(DB.tb_Students.Where(x => x.IsDeleted == false).Select(x => x).ToList());

            return listStudentRecord;
        }

        public ObservableCollection<tb_Students> SearchStudent(string stringSearch)
        {
            var listStudentSearch = new ObservableCollection<tb_Students>((from t in DB.tb_Students
                                                                           where t.IsDeleted == false && (t.Name.Contains(stringSearch) || t.Email.Contains(stringSearch) || t.Address.Contains(stringSearch))
                                                                           select t).ToList());
            return listStudentSearch;
        }




        public bool AddStudent(tb_Students Students)
        {
            DB.tb_Students.Add(Students);
            return DB.SaveChanges() > 0;
        }

        public bool DeleteStudent(int ID)
        {
            var student = DB.tb_Students.First(x => x.ID == ID);
            student.IsDeleted = true;

            return DB.SaveChanges() > 0;
        }

        public bool UpdateStudent(tb_Students students)
        {
            var student = DB.tb_Students.Where(x => x.ID == students.ID).Single();
            student.Name = students.Name;
            student.Gender = students.Gender;
            student.DateOfBirth = students.DateOfBirth;
            student.Address = students.Address;
            student.Email = students.Email;
            students.LastUpdatedDate = DateTime.Now;

            DB.Entry(student).State = System.Data.EntityState.Modified;

            return DB.SaveChanges() > 0;
        }

    }
}
