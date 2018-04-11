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

        public ScaleConfig GetScaleConfig(int scaleConfigId)
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleConfigs
                    .Where(p => p.ScaleConfigId == scaleConfigId).FirstOrDefault();

            }
        }
        public List<ScaleConfig> GetScaleConfigs()
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleConfigs.ToList();

            }
        }
    }
}
