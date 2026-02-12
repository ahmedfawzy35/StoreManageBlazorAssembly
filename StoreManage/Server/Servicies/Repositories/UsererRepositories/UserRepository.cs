using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.UserInterfacies;
using StoreManage.Shared.Dtos.UserDtos;
using StoreManage.Shared.Models;
using System.Linq;

namespace StoreManage.Server.Servicies.Repositories.UsererRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        private BaseRepository<User> _userRepository;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _userRepository = new BaseRepository<User>(_context);
        }

        public async Task<LogInResponseDto> LoginAsync(LoginDto entity)
        {
            var login = new LogInResponseDto();

            var user = await _userRepository.FindAsync(x => x.UserName == entity.UserName && x.Password == entity.Password);
            if (user != null && user.Enabled)
            {
                if (user.IsDeleted == true)
                {
                    login.errorMessage = "خطأ بإسم المستخد او كلمة المرور";
                    return login;
                }
                var allBranches = await _context.Branches.ToListAsync();
                var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
                var userBranches = await _context.UserBranches.Include(b => b.Branche).Where(x => x.UserId == user.Id).ToListAsync();
                List<Clime>? climes = new List<Clime>();
                foreach (var role in userRoles)
                {
                    var roleClimes = await _context.RoleClimes.Include(c => c.Clime).Where(x => x.RoleId == role.Id).ToListAsync();
                    foreach (var roleClime in roleClimes)
                    {
                        climes.Add(roleClime.Clime);
                    }


                }
                login.user = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Enabled = user.Enabled,
                    IdUserDeleIt = user.IdUserDeleIt,
                    DateDeleted = user.DateDeleted,
                    EditCount = user.EditCount,
                    FullName = user.FullName,
                    IsDeleted = user.IsDeleted,
                    IsEdit = user.IsEdit
                };
                login.Climes = ToClimeDto(climes);
                login.Login = true;
                login.UserBranches = ToBranchesDto(userBranches);
                login.AllBranches = ToBrancheDto(allBranches);
                return login;
            }
            login.errorMessage = "خطأ بإسم المستخد او كلمة المرور";
            return login;
        }

        private List<ClimeDto> ToClimeDto(List<Clime> climes)
        {
            List<ClimeDto> climeDtos = new List<ClimeDto>();
            foreach (var clime in climes)
            {
                climeDtos.Add(new ClimeDto() { Name = clime.Name });
            }

            return climeDtos;
        }
        private List<BrancheDto> ToBranchesDto(List<UserBranches> userBranches)
        {
            List<BrancheDto> userBranchesDto = new List<BrancheDto>();
            foreach (var userBranche in userBranches)
            {
                userBranchesDto.Add(new BrancheDto() { Id = userBranche.Branche.Id, Name = userBranche.Branche.Name });
            }

            return userBranchesDto;
        }
        private List<BrancheDto> ToBrancheDto(List<Branche> branches)
        {
            List<BrancheDto> brancheDto = new List<BrancheDto>();
            foreach (var branche in branches)
            {
                brancheDto.Add(new BrancheDto() { Id = branche.Id, Name = branche.Name });
            }

            return brancheDto;
        }

    }
}
