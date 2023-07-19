using DataFormatter.Mappers;
using DataFormatter.Models;

namespace DataFormatter.Services
{
  public class DataService : IDataService
  {
    private readonly INoticeOfLeaseMapper _mapper;
    public DataService(INoticeOfLeaseMapper mapper)
    {
      _mapper = mapper;
    }

    public List<LeasescheduleDataOut> GetMapLeaseScheduleData(List<LeasescheduleData> data)
    {
      var returnList = new List<LeasescheduleDataOut>();
      foreach (var item in data)
      {
        var mappedItem = new LeasescheduleDataOut
        {
          Leaseschedule = new LeasescheduleOut
          {
            ScheduleType = item.Leaseschedule.ScheduleType,
            ScheduleEntry = MapScheduleEntry(item.Leaseschedule.ScheduleEntry),
          }
        };

        returnList.Add(mappedItem);
      }
      return returnList;
    }

    private List<ScheduleEntryOut> MapScheduleEntry(List<ScheduleEntryIn> scheduleEntries)
    {
      var list = new List<ScheduleEntryOut>();
      foreach (var entry in scheduleEntries)
      {
        list.Add(new ScheduleEntryOut
        {
          EntryNumber = entry.EntryNumber,
          EntryDate = entry.EntryDate,
          EntryType = entry.EntryType,
          NoticeOfLease = _mapper.Map(entry.EntryText),
        });
      }
      return list;
    }
  }
}
