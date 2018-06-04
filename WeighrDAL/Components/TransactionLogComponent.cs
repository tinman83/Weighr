using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class TransactionLogComponent
    {
        public void AddTransactionLog(TransactionLog transactionLog)
        {
            using (var db = new WeighrContext())
            {
                db.TransactionLogs.Add(transactionLog);
                db.SaveChanges();

            }
        }

        public void UpdateTransactionLog(TransactionLog transactionLog)
        {
            using (var db = new WeighrContext())
            {
                db.TransactionLogs.Update(transactionLog);
                db.SaveChanges();
            }
        }
        public TransactionLog GetTransactionLog(int transactionLogId)
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs
                    .Where(p => p.TransactionLogId == transactionLogId).FirstOrDefault();

            }
        }
        public TransactionLog GetTransactionLogByRowGuid(Guid rowguid)
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs
                    .Where(p => p.rowguid == rowguid).FirstOrDefault();

            }
        }
        public List<TransactionLog> GetTransactionLogs()
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs.ToList();

            }
        }

        public List<TransactionLog> GetTransactionLogsByProductCode(string productCode)
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs
                    .Where(t=>t.ProductCode==productCode)
                    .ToList();

            }
        }
    }
}
