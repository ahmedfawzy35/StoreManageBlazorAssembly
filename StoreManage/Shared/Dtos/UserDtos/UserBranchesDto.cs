using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.UserDtos
{
    public class UserBranchesDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
       
        public int BrancheId { get; set; }
       
    }
}
