using Pro.Mys.Application.Dto;
using Pro.Mys.Application.Utility;
using Pro.Mys.DataAccess.Dapper;
using System.Threading;

public class BackgroundWorkerService : BackgroundService
{
    public ILogger<BackgroundWorkerService> _logger { get; }

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //_logger.LogInformation("service started.");
        WriteToFile("StartAsync...");

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StopAsync...");
        return Task.CompletedTask;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // _logger.LogInformation("worker running at : {time}", DateTimeOffset.Now);
            // WriteToFile("worker running at : "+ DateTimeOffset.Now);
            SendEmail();
            await Task.Delay(10*60*1000, stoppingToken);
        }
    }

    private void WriteToFile(string Message)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        if (!File.Exists(filepath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(filepath))
            {
                sw.WriteLine(Message);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(Message);
            }
        }
    }
    private void SendEmail() {
        DapperService dapperService = new DapperService();
     var dateData=    DateTimeServices.Utl_Date_DayOfWeek();
        string query = @"
SELECT  [Id]
      ,[DateTaskIsExecute]
      ,[Rate]
      ,[Title]
      ,[MasterDataId]
      ,[Created]
      ,[CreatedBy]
      ,[LastModified]
      ,[LastModifiedBy]
      ,[IsDeleted]
      ,[Time]
      ,[IsExecuted]
      ,[Description]
      ,[CreatedShamsy]
      ,[LastModifiedShamsy]
  FROM [8719_manageyourself].[dbo].[Duties]
  where DateTaskIsExecute="+dateData.ShamsyDateFormatSlashLess+@"
";
      var res=  dapperService.ReturnData<DutyOutDto>(query);
        string html = "<table style='direction:rtl'>";
        
        foreach (var item in res)
        {
            html += "<tr>";
            html += "<td style='border:1px solid black'>"+item.Title+"</td>";
            html += "<td style='border:1px solid black'>" + (item.IsExecuted==true?"Yes":"No") + "</td>";
            html += "</tr>";
        }
        html += "</table>";
        Pro.Mys.DataAccess.Api.SendEmailServiceApi sendEmail=new Pro.Mys.DataAccess.Api.SendEmailServiceApi();
        sendEmail.SendEmail(html, dateData.ShamsyDateFormat, "امروز");



       
        string query2 = @"
SELECT  [Id]
      ,[DateTaskIsExecute]
      ,[Rate]
      ,[Title]
      ,[MasterDataId]
      ,[Created]
      ,[CreatedBy]
      ,[LastModified]
      ,[LastModifiedBy]
      ,[IsDeleted]
      ,[Time]
      ,[IsExecuted]
      ,[Description]
      ,[CreatedShamsy]
      ,[LastModifiedShamsy]
  FROM [8719_manageyourself].[dbo].[Duties]
  where DateTaskIsExecute=" + dateData.TommarowNoSlash + @"
";
        var res2 = dapperService.ReturnData<DutyOutDto>(query2);
        string html2 = "<table style='direction:rtl'>";

        foreach (var item in res2)
        {
            html2 += "<tr>";
            html2 += "<td style='border:1px solid black'>" + item.Title + "</td>";
            html2 += "<td style='border:1px solid black'>" + (item.IsExecuted == true ? "Yes" : "No") + "</td>";
            html2 += "</tr>";
        }
        html2 += "</table>";
        Pro.Mys.DataAccess.Api.SendEmailServiceApi sendEmail2 = new Pro.Mys.DataAccess.Api.SendEmailServiceApi();
        sendEmail2.SendEmail(html2, dateData.TommarowFormat, "فردا");
    }


}