using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
   public class ShiftComponent
    {
        public void AddShift(Shift shift)
        {
            using (var db = new WeighrContext())
            {
                db.Shifts.Add(shift);
                db.SaveChanges();

            }
        }
        public void UpdateShift(Shift shift)
        {
            using (var db = new WeighrContext())
            {
                db.Shifts.Update(shift);
                db.SaveChanges();
            }
        }

        public Shift GetShift(int shiftId)
        {
            using (var db = new WeighrContext())
            {
                return db.Shifts
                    .Where(p => p.ShiftId == shiftId).FirstOrDefault();

            }
        }
        public List<Shift> GetShifts()
        {
            using (var db = new WeighrContext())
            {
                return db.Shifts.ToList();

            }
        }
    }
}
