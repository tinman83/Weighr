using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
   public class DeviceInfoComponent
    {
        public void AddDeviceInfo(DeviceInfo DeviceInfo)
        {
            using (var db = new WeighrContext())
            {
                db.DeviceInfos.Add(DeviceInfo);
                db.SaveChanges();

            }
        }
        public void UpdateDeviceInfo(DeviceInfo DeviceInfo)
        {
            using (var db = new WeighrContext())
            {
                db.DeviceInfos.Update(DeviceInfo);
                db.SaveChanges();
            }
        }

        public DeviceInfo GetDeviceInfo()
        {
            using (var db = new WeighrContext())
            {
                return db.DeviceInfos.FirstOrDefault();

            }
        }

        public void DeleteDeviceInfo()
        {
            using (var db = new WeighrContext())
            {
                var dev= db.DeviceInfos.FirstOrDefault();

                if (dev != null)
                {
                    db.DeviceInfos.Remove(dev);
                    db.SaveChanges();
                }
                
            }
        }


    }
}
