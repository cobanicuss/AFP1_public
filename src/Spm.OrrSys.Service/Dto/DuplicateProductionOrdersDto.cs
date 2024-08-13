using System;

namespace Spm.OrrSys.Service.Dto
{
    public class DuplicateProductionOrdersDto
    {
        public string SapProductionOrderNumber { get; set; }
        public DateTime? HistoryAdded { get; set; }
        public DateTime? DateRequested { get; set; }
    }
}