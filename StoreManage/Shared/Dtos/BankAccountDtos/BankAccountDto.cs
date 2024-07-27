using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.BankAccountDtos
{
    public class BankAccountDto
    {

        public int Id { get; set; }
        public string BankName { get; set; }
        public string BankBrancheName { get; set; }
        public string BankAccountNumber { get; set; }
        public double StartAccount { get; set; } = 0;
        public string? Notes { get; set; }
    }
}
