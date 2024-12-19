using Microsoft.AspNetCore.Http;
using PingWebAPI.ApiModels.Dtos;
using PingWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace PingWebAPI.Services.Implementations
{
    /// <inheritdoc/>
    public class CheckActivityService : ICheckActivityService
    {

        public CheckActivityService()
        {

        }

        public async Task<SiteWithStatusDto> CheckSiteActivity(SiteToCheckDto siteWithStatus)
        {
            //bool result;
            //Uri uriResult;
            //if (result = Uri.TryCreate(siteWithStatus.URL, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            //    throw new Exception("Неправильная URL ссылка...");

            string resultUrl = siteWithStatus.URL;

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:126.0) Gecko/20100101 Firefox/126.0");

            try
            {
                var isActive = await httpClient.GetAsync(resultUrl);

                if (isActive.IsSuccessStatusCode)
                    return new SiteWithStatusDto() { URL = resultUrl, IsActive = true };
                else
                    return new SiteWithStatusDto() { URL = resultUrl, IsActive = false };
            }
            catch (Exception ex)
            {
                return new SiteWithStatusDto() { URL = resultUrl, IsActive = false };
            }
        }

        public async Task<List<SiteWithStatusDto>> CheckSiteActivityList(List<SiteToCheckDto> siteToCheckList)
        {
            if (siteToCheckList == null || siteToCheckList.Count == 0)
            {
                return new List<SiteWithStatusDto>();
            }
            else
            {
                List<SiteWithStatusDto> checkedSites = new List<SiteWithStatusDto>();

                foreach (var siteWithStatus in siteToCheckList)
                {
                    string resultUrl = siteWithStatus.URL;

                    using var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:126.0) Gecko/20100101 Firefox/126.0");

                    try
                    {
                        var isActive = await httpClient.GetAsync(resultUrl);

                        if (isActive.IsSuccessStatusCode)
                            checkedSites.Add(new SiteWithStatusDto() { URL = resultUrl, IsActive = true });
                        else
                            checkedSites.Add(new SiteWithStatusDto() { URL = resultUrl, IsActive = false });
                    }
                    catch (Exception ex)
                    {
                        checkedSites.Add(new SiteWithStatusDto() { URL = resultUrl, IsActive = false });
                    }
                }

                return checkedSites;
            }
        }
    }
}
