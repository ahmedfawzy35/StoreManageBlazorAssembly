using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos
{
    public class TimeDto
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; } 
        public int BrancheId { get; set; }
    } 
    public class DayDto
    {

        public DateTime Date { get; set; }
        public int BrancheId { get; set; }
    }

}
