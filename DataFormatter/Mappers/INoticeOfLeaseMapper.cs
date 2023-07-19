using DataFormatter.Models;

namespace DataFormatter.Mappers
{
  public interface INoticeOfLeaseMapper
  {
    NoticeOfLease Map(List<string> entryText);
  }
}
