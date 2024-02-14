using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Mys.DataAccess.Api
{
    public class SendEmailServiceApi
    {
        public async Task SendEmail(string bodyHtml,string subject,string mailUserName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:9003/api/message/sendemail");

            var obj = new
            {
                body = bodyHtml,
                subject =subject,
                mailUserName = mailUserName
            };
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var content = new StringContent(body, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
