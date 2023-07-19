using DataFormatter.Models;

namespace DataFormatter.Services
{
  public interface IDataService
  {
    List<LeasescheduleDataOut> GetMapLeaseScheduleData(List<LeasescheduleData> data);
  }
}
