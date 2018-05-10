using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class ScaleConfigComponent
    {
        public void AddScaleConfig(ScaleConfig scaleConfig)
        {
            using (var db = new WeighrContext())
            {
                db.ScaleConfigs.Add(scaleConfig);
                db.SaveChanges();

            }
        }
        public void UpdateScaleConfig(ScaleConfig scaleConfig)
        {
            using (var db = new WeighrContext())
            {
                db.ScaleConfigs.Update(scaleConfig);
                db.SaveChanges();
            }
        }

        public ScaleConfig GetScaleConfig()
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleConfigs
                    .Take(1)
                    .FirstOrDefault();

            }
        }
        public List<ScaleConfig> GetScaleConfigs()
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleConfigs.ToList();

            }
        }

        public ScaleConfig ZeroScale(decimal offset)
        {
            using (var db = new WeighrContext())
            {

                var current = db.ScaleConfigs.FirstOrDefault();
                // current.offset = offset;

                db.ScaleConfigs.Update(current);
                db.SaveChanges();

                return current;
            }
        }
    }
}
