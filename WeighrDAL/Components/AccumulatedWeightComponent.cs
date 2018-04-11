using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class AccumulatedWeightComponent
    {
        public void AddAccumulatedWeight(AccumulatedWeight accumulatedWeight)
        {
            using (var db = new WeighrContext())
            {
                db.AccumulatedWeights.Add(accumulatedWeight);
                db.SaveChanges();

            }
        }
        public void UpdateAccumulatedWeight(AccumulatedWeight accumulatedWeight)
        {
            using (var db = new WeighrContext())
            {
                db.AccumulatedWeights.Update(accumulatedWeight);
                db.SaveChanges();
            }
        }

        public AccumulatedWeight GetAccumulatedWeight(int accumulatedWeightId)
        {
            using (var db = new WeighrContext())
            {
                return db.AccumulatedWeights
                    .Where(p => p.AccumulatedWeightId == accumulatedWeightId).FirstOrDefault();

            }
        }
        public List<AccumulatedWeight> GetAccumulatedWeights()
        {
            using (var db = new WeighrContext())
            {
                return db.AccumulatedWeights.ToList();

            }
        }
    }
}
