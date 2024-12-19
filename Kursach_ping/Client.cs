using Kursach_ping.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_ping
{
    /// <summary>
    /// Файл запросов к API
    /// </summary>
    public static class Client
    {
        public async static Task<HttpResponseMessage> CheckSite(SiteToCheckDto siteToCheck)
        {
            string url = "https://localhost:7196/api/SiteCheck/CheckSiteStatus";

            using HttpClient client = new HttpClient();

            return await client.PostAsJsonAsync(url, siteToCheck);
        }

        public async static Task<HttpResponseMessage> CheckSiteList(List<SiteToCheckDto> employees)
        {
            string url = "https://localhost:7196/api/SiteCheck/CheckSiteStatusList";

            using HttpClient client = new HttpClient();

            var result = await client.PostAsJsonAsync(url, employees);

            return result;
        }

    }
}
