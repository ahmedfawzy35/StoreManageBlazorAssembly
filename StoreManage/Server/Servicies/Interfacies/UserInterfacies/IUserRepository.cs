using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.UserDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.UserInterfacies
{
    public interface IUserRepository : IBaseRepository<User>
    {

        public Task<LogInResponseDto> LoginAsync(LoginDto entity);
    }
}
