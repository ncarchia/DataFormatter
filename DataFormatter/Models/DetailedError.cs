using System.Text.Json;

namespace DataFormatter.Models
{
  public class DetailedError
  {
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}
