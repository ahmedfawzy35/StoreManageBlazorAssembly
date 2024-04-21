using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
         private readonly AppDbContext _context;


    public CustomerRepository(AppDbContext context) : base(context)
    {
    }


}
}
