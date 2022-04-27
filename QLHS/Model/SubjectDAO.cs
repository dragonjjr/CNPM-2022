using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHS.Model
{
    public class SubjectDAO
    {
        private QLHSEntities db;

        public ObservableCollection<tb_Subjects> GetInfoSubjects()
        {
            var list = new ObservableCollection<tb_Subjects>(db.tb_Subjects.Where(sj => sj.IsDeleted == false).ToList());

            return list;
        }

        public tb_Subjects GetSubjectByName(string name)
        {
            return db.tb_Subjects.FirstOrDefault(sj => sj.Name == name && sj.IsDeleted == false);
        }

        public bool AddInfoSubject(tb_Subjects newSubject)
        {
            db.tb_Subjects.Add(newSubject);
            return db.SaveChanges() > 0;
        }

        public bool UpdateInfoNameSubject(int subjectId, string newName)
        {
            var subjectInfo = db.tb_Subjects.Single(st => st.ID == subjectId && st.IsDeleted == false);
            subjectInfo.Name = newName;

            return db.SaveChanges() > 0;
        }

        public bool DeleteInfoSubject(int subjectId)
        {
            var subjectInfo = db.tb_Subjects.Single(st => st.ID == subjectId);
            subjectInfo.IsDeleted = true;

            return db.SaveChanges() > 0;
        }

        public SubjectDAO()
        {
            db = new QLHSEntities();

        }
    }
}
