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

        public TransactionLog GetTransactionLog(int transactionLogId)
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs
                    .Where(p => p.TransactionLogId == transactionLogId).FirstOrDefault();

            }
        }
        public List<TransactionLog> GetTransactionLogs()
        {
            using (var db = new WeighrContext())
            {
                return db.TransactionLogs.ToList();

            }
        }
    }
}
