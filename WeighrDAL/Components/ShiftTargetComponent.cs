using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class ShiftTargetComponent
    {
        public void AddShiftTarget(ShiftTarget shiftTarget)
        {
            using (var db = new WeighrContext())
            {
                db.ShiftTargets.Add(shiftTarget);
                db.SaveChanges();

            }
        }
        public void UpdateShiftTarget(ShiftTarget shiftTarget)
        {
            using (var db = new WeighrContext())
            {
                db.ShiftTargets.Update(shiftTarget);
                db.SaveChanges();
            }
        }

        public ShiftTarget GetShiftTarget(int shiftTargetId)
        {
            using (var db = new WeighrContext())
            {
                return db.ShiftTargets
                    .Where(p => p.ShiftTargetId == shiftTargetId).FirstOrDefault();

            }
        }
        public List<ShiftTarget> GetShiftTargets()
        {
            using (var db = new WeighrContext())
            {
                return db.ShiftTargets.ToList();

            }
        }
    }
}
