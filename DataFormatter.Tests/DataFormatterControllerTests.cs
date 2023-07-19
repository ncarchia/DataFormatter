using DataFormatter.Controllers;
using DataFormatter.Models;
using DataFormatter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DataFormatter.Tests
{
  public class DataFormatterControllerTests
  {
    private readonly DataFormatterController _controller;
    private readonly Mock<IDataService> _dataService = new();
    private readonly Mock<ILogger<DataFormatterController>> _mockLogger = new();

    public DataFormatterControllerTests()
    {
      _controller = new DataFormatterController(_dataService.Object, _mockLogger.Object);
    }

    [Fact]
    public async void FormatScheduleOfNoticesOfLeases_GivenScheduleOfNoticesOfLeasesFile_ShouldReturnFileWithFormattedData()
    {
      var data = GetData();

      _dataService
        .Setup(ds => ds.GetMapLeaseScheduleData(data))
        .Returns(GetMappedData())
        .Verifiable();

      var result = await _controller.FormatScheduleOfNoticesOfLeases(data);

      Assert.NotNull(result);
      var okObject = result.Result as OkObjectResult;
      var list = okObject?.Value as List<LeasescheduleDataOut>;
      Assert.Equal(200, okObject?.StatusCode);
      Assert.Equal(3, list?.First().Leaseschedule.ScheduleEntry.Count);
      Assert.Equal("SCHEDULE OF NOTICES OF LEASE", list?.First().Leaseschedule.ScheduleType);
      _dataService
        .Verify(ds => ds.GetMapLeaseScheduleData(data), Times.Once);
    }

    private static List<LeasescheduleData> GetData()
      => new()
      {
        new LeasescheduleData()
        {
          Leaseschedule = new Leaseschedule
          {
            ScheduleType = "SCHEDULE OF NOTICES OF LEASE",
            ScheduleEntry = new List<ScheduleEntryIn>
            {
              new ScheduleEntryIn
              {
                EntryNumber = "1",
                EntryDate = "",
                EntryType = "Schedule of Notices of Leases",
                EntryText = new List<string>
                {
                  "28.01.2009      Transformer Chamber (Ground   23.01.2009      EGL551039  ",
                  "tinted blue     Floor)                        99 years from              ",
                  "(part of)                                     23.1.2009"
                }
              },
              new ScheduleEntryIn
              {
                EntryNumber = "2",
                EntryDate = "",
                EntryType = "Schedule of Notices of Leases",
                EntryText = new List<string>
                {
                  "16.04.1993      High Voltage Cables,          25.03.1993      WSX178084  ",
                  "Edged Blue      Gatwick Airport               140 years from             ",
                  "25.3.1993                  ",
                  "NOTE 1: Only the airspace within the pits and ducts or soil and the High Voltage Cable or Conductors is included in the Lease.",
                  "NOTE 2: The lease comprises also other land."
                }
              },
              new ScheduleEntryIn
              {
                EntryNumber = "3",
                EntryDate = "",
                EntryType = "Cancelled Item - Schedule of Notices of Leases",
                EntryText = new List<string>
                {
                  "ITEM CANCELLED on 22 March 2018."
                }
              },
            }
          }
        }
      };

    private static List<LeasescheduleDataOut> GetMappedData()
      => new()
      {
        new LeasescheduleDataOut()
        {
          Leaseschedule = new LeasescheduleOut
          {
            ScheduleType = "SCHEDULE OF NOTICES OF LEASE",
            ScheduleEntry = new List<ScheduleEntryOut>
            {
              new ScheduleEntryOut
              {
                EntryNumber = "1",
                EntryDate = "",
                EntryType = "Schedule of Notices of Leases",
                NoticeOfLease = new NoticeOfLease
                {
                  RegDateAndPlanRef = "28.01.2009    tinted blue   (part of)",
                  PropertyDescription = "Transformer Chamber (Ground Floor)",
                  DateOfLeaseAndTerm = "23.01.2009      99 years from   23.1.2009",
                  LeaseTitle = "EGL551039",
                  Notes = new List<string>(),
                  SupplementalNote = null
                }
              },
              new ScheduleEntryOut
              {
                EntryNumber = "2",
                EntryDate = "",
                EntryType = "Schedule of Notices of Leases",
                NoticeOfLease = new NoticeOfLease
                {
                  RegDateAndPlanRef = "16.04.1993    hatched       yellow",
                  PropertyDescription = "High Voltage Cables GatwickAirport",
                  DateOfLeaseAndTerm = "25.03.1993     140 years from   25.3.1993",
                  LeaseTitle = "WSX178084",
                  Notes = new List<string>()
                  {
                    "NOTE 1: Only the airspace within the pits and ducts or soil and the high voltage cable or conductors is included in the lease.",
                    "NOTE 2: The Lease comprises also other land"
                  },
                  SupplementalNote = null
                }
              },
              new ScheduleEntryOut
              {
                EntryNumber = "3",
                EntryDate = "",
                EntryType = "Schedule of Notices of Leases",
                NoticeOfLease = new NoticeOfLease
                {
                  RegDateAndPlanRef = null,
                  PropertyDescription = null,
                  DateOfLeaseAndTerm = null,
                  LeaseTitle = null,
                  Notes = null,
                  SupplementalNote = "ITEM CANCELLED on 22 March 2018."
                }
              }
            }
          }
        }
      };
  }
}