using DataFormatter.Models;

namespace DataFormatter.Mappers
{
  public class NoticeOfLeaseMapper : INoticeOfLeaseMapper
  {
    private const int Column1Length = 14;
    private const int Column2Length = 28;
    private const int Column3Length = 16;
    private const int Column4Length = 12;
    private const int Column1StartIndex = 0;
    private const int Column2StartIndex = 15;
    private const int Column3StartIndex = 44;
    private const int Column4StartIndex = 59;

    public NoticeOfLease Map(List<string> entryText)
    {

      if (entryText.Count == 1)
      {
        return new NoticeOfLease()
        {
          SupplementalNote = entryText[0]
        };
      }

      var extractedNotes = new List<string>();
      var textWithoutNotes = new List<string>();
      var regDateAndPlanRef = string.Empty;
      var propertyDescription = string.Empty;
      var dateOfLeaseAndTerm = string.Empty;
      var leaseTitle = string.Empty;
      int iteration = 0;

      if (entryText.Count > 1)
      {
        extractedNotes = entryText.Where(line => line != null && line.StartsWith("NOTE")).ToList();
        textWithoutNotes = entryText.Except(extractedNotes).Where(line => line != null).ToList();
        foreach (var line in textWithoutNotes)
        {
          var tempLine = line;
          var lineLength = line.Length;
          if (line.Length < 73)
          {
            for (int i = 0; i < 73; i++)
            {
              tempLine += " ";
            }
          }

          regDateAndPlanRef += tempLine.Substring(Column1StartIndex, Column1Length);
          propertyDescription += tempLine.Substring(Column2StartIndex, Column2Length);
          dateOfLeaseAndTerm += tempLine.Substring(Column3StartIndex, Column3Length);

          if (iteration == 0)
          {
            leaseTitle = tempLine.Substring(Column4StartIndex, Column4Length);
          }
          iteration++;
        }
      }

      return new NoticeOfLease()
      {
        RegDateAndPlanRef = regDateAndPlanRef.TrimStart().TrimEnd(),
        PropertyDescription = propertyDescription.TrimStart().TrimEnd(),
        DateOfLeaseAndTerm = dateOfLeaseAndTerm.TrimStart().TrimEnd(),
        LeaseTitle = leaseTitle.TrimStart().TrimEnd(),
        Notes = extractedNotes
      };
    }
  }
}

