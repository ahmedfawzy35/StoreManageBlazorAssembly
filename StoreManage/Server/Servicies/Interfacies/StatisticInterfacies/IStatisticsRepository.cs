using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.StatisticsDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.StatisticIntergacies
{
    public interface IStatisticsRepository 
    {
            public DayStatisticDto GetDaySimpleStatisticForBranche(DayDto day);
            //public DayStatisticDto GetDayDetailedStatistic(int brancheId);
            //public DayStatisticDto GetTimeSimpleStatistic(int brancheId);
            //public DayStatisticDto GetTimeDetailedStatistic(int brancheId);
    }
}
