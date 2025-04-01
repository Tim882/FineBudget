using Models.DbModels.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.BaseDto.BalanceItem
{
    public class BalanceItemRequestDto: BaseRequestDto
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
    }
}
