using QLHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QLHS.Model
{
    public class RegulationDAO
    {
        private QLHSEntities db;

        public tb_Regulations GetInfoRegulation()
        {
            return db.tb_Regulations.Where(re => re.IsDeleted == false).FirstOrDefault();
        }

        public int getQuantityClass()
        {
            return db.tb_Class.Where(cl=>cl.IsDeleted == false).Count();
        }

        public int getQuantitySubject()
        {
            return db.tb_Subjects.Where(sj => sj.IsDeleted == false).Count();
        }

        //public void UpdateQuantity

        public bool UpdateRegulation(tb_Regulations newRegulation)
        {

            tb_Regulations oldRegulation = db.tb_Regulations.Single(re => re.ID == newRegulation.ID);
            oldRegulation.MaxAge = newRegulation.MaxAge;
            oldRegulation.MinAge = newRegulation.MinAge;
            oldRegulation.MaxQuantity = newRegulation.MaxQuantity;
            oldRegulation.ClassQuantity = newRegulation.ClassQuantity;
            oldRegulation.SubjectQuantity = newRegulation.SubjectQuantity;
            oldRegulation.PassingGrade = newRegulation.PassingGrade;

            return db.SaveChanges()>0;

        }

        public RegulationDAO()
        {
            db = new QLHSEntities();
        }
    }
}
