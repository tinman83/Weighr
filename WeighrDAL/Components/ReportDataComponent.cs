using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WeighrDAL.ReportModels;

namespace WeighrDAL.Components
{
   public class ReportDataComponent
    {
        public ProductionSummaryReport GetProductionSummaryReport(string productCode,DateTime dateFrom,DateTime dateTo)
        {
            using (var db = new WeighrContext())
            {

                var data = db.TransactionLogs
                        .Where(p => p.ProductCode == productCode && (p.TransactionDate >= dateFrom && p.TransactionDate <= dateTo))
                        .GroupBy(p => p.ProductCode)
                        .Select(s => new ProductionSummaryReport
                        {
                            Units = s.Count(),
                            RequiredWeight=s.Sum(p=>p.TargetWeight),
                            ActualWeight = s.Sum(p => p.ActualWeight),
                            AverageWeight=s.Average(p=>p.ActualWeight),
                            PercDiffWeight= ((s.Sum(p => p.ActualWeight)- s.Sum(p => p.TargetWeight))/s.Sum(p => p.TargetWeight))*100,
                            AverageFillTime=s.Average(p=>p.ActualFillTime)
                        }).FirstOrDefault();

                return (ProductionSummaryReport)data;
                     
            }

        }
    }
}
