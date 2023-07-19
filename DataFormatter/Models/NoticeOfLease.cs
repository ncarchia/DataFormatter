namespace DataFormatter.Models
{
  public class NoticeOfLease
  {
    public string RegDateAndPlanRef { get; set; }
    public string PropertyDescription { get; set; }
    public string DateOfLeaseAndTerm { get; set; }
    public string LeaseTitle { get; set; }
    public List<string> Notes { get; set;}
    public string SupplementalNote { get; set; }
  }
}
