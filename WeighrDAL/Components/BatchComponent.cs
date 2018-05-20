using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
   public class BatchComponent
    {
        public void AddBatch(Batch Batch)
        {
            using (var db = new WeighrContext())
            {
                db.Batches.Add(Batch);
                db.SaveChanges();

            }
        }
        public void UpdateBatch(Batch Batch)
        {
            using (var db = new WeighrContext())
            {
                db.Batches.Update(Batch);
                db.SaveChanges();
            }
        }

        public Batch GetBatch(int batchId)
        {
            using (var db = new WeighrContext())
            {
                return db.Batches
                      .Where(p => p.BatchId == batchId).FirstOrDefault();

            }
        }
        public List<Batch> GetBatches()
        {
            using (var db = new WeighrContext())
            {
                return db.Batches.ToList();

            }
        }

        public Batch SetCurrentBatch(string batchCode)
        {
            using (var db = new WeighrContext())
            {
                var previousCurrent = db.Batches
                    .Where(p => p.isCurrent == true).FirstOrDefault();

                if (previousCurrent != null)
                {
                    previousCurrent.EndTime = DateTime.Now;
                    previousCurrent.isCurrent = false;
                    db.Batches.Update(previousCurrent);
                }


                var current = db.Batches
                    .Where(p => p.BatchCode == batchCode).FirstOrDefault();

                current.isCurrent = true;
                db.Batches.Update(current);
                db.SaveChanges();

                return current;
            }
        }

        public Batch GetCurrentBatch()
        {
            using (var db = new WeighrContext())
            {
                return db.Batches
                      .Where(p => p.isCurrent == true).FirstOrDefault();

            }
        }
    }
}
