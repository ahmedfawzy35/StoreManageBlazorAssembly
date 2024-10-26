using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.UserDtos
{
    public class LogInResponseDto
    {

        public UserDto? user { get; set; }
        public List<ClimeDto>? Climes { get; set; }
        public List<BrancheDto>? UserBranches { get; set; }
        public List<BrancheDto>? AllBranches { get; set; }
        public bool Login { get; set; } = false;
        public string? errorMessage { get; set; } 
    }
}
