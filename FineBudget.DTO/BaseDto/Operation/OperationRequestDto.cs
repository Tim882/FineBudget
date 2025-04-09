using Models.DbModels.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BaseDto.Operation
{
    public class OperationRequestDto: BaseRequestDto
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;

        public Guid AccountId { get; set; }

        public Guid? AssetId { get; set; }

        public Guid? LiabilityId { get; set; }
    }
}
