namespace DataFormatter.Models
{
  // Root myDeserializedClass = JsonConvert.DeserializeObject<List<LeasescheduleData>>(myJsonResponse);
  public class Leaseschedule
  {
    public string ScheduleType { get; set; }
    public List<ScheduleEntryIn> ScheduleEntry { get; set; }
  }

  public class LeasescheduleOut
  {
    public string ScheduleType { get; set; }
    public List<ScheduleEntryOut> ScheduleEntry { get; set; }
  }

  public class LeasescheduleData
  {
    public Leaseschedule Leaseschedule { get; set; }
  }

  public class LeasescheduleDataOut
  {
    public LeasescheduleOut Leaseschedule { get; set; }
  }

  public class ScheduleEntry
  {
    public string EntryNumber { get; set; }
    public string EntryDate { get; set; }
    public string EntryType { get; set; } 
  }

  public class ScheduleEntryIn : ScheduleEntry
  {
    public List<string> EntryText { get; set; }
  }

  public class ScheduleEntryOut : ScheduleEntry
  {
    public NoticeOfLease NoticeOfLease { get; set; }
  }
}
