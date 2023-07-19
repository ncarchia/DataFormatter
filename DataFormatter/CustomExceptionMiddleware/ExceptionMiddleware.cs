using DataFormatter.Controllers;
using DataFormatter.Models;
using System.Net;

namespace DataFormatter.CustomExceptionMiddleware
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<DataFormatterController> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<DataFormatterController> logger)
    {
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong: {ex}");
        await HandleExceptionAsync(httpContext, ex);
      }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      var message = exception switch
      {
        Exception => "An error has occured while mapping the data.",
        _ => "Internal Server Error from the custom middleware."
      };

      await context.Response.WriteAsync(new DetailedError()
      {
        StatusCode = context.Response.StatusCode,
        Message = message
      }.ToString());
    }
  }
}
