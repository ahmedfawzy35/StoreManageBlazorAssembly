using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.StatisticIntergacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.StatisticsDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.StatisticRepositories
{
    public class StatisticRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticRepository(AppDbContext context)
        {
            _context = context;
        }


        public DayStatisticDto GetDaySimpleStatisticForBranche(DayDto day)
        {
            throw new NotImplementedException();
        }
    }
}
