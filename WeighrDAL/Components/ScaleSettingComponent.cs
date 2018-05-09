using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class ScaleSettingComponent
    {
        public void AddScaleSetting(ScaleSetting scaleSetting)
        {
            using (var db = new WeighrContext())
            {
                db.ScaleSettings.Add(scaleSetting);
                db.SaveChanges();

            }
        }
        public void UpdateScaleSetting(ScaleSetting scaleSetting)
        {
            using (var db = new WeighrContext())
            {
                db.ScaleSettings.Update(scaleSetting);
                db.SaveChanges();
            }
        }

        public ScaleSetting GetScaleSettingDefault()
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleSettings
                    .OrderByDescending(s=>s.ScaleSettingId)
                    .Take(1).FirstOrDefault();

            }
        }

        public ScaleSetting GetScaleSetting(int scaleSettingId)
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleSettings
                    .Where(p => p.ScaleSettingId == scaleSettingId).FirstOrDefault();

            }
        }
        public List<ScaleSetting> GetScaleSettings()
        {
            using (var db = new WeighrContext())
            {
                return db.ScaleSettings.ToList();

            }
        }
    }
}
