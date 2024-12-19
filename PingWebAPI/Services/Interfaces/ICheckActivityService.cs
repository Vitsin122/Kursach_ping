using PingWebAPI.ApiModels.Dtos;
using System.Text.RegularExpressions;

namespace PingWebAPI.Services.Interfaces
{
    /// <summary>
    /// Проверка активности интернет ресурса
    /// </summary>
    public interface ICheckActivityService
    {
        /// <summary>
        /// Проверка активности одного сайта
        /// </summary>
        /// <param name="siteWithStatus">Сайт для проверки</param>
        /// <returns></returns>
        public Task<SiteWithStatusDto> CheckSiteActivity (SiteToCheckDto siteWithStatus);

        /// <summary>
        /// Проверка активности сразу нескольких сайтов
        /// </summary>
        /// <param name="siteToCheck">Список сайтов для проверки</param>
        /// <returns></returns>
        public Task<List<SiteWithStatusDto>> CheckSiteActivityList (List<SiteToCheckDto> siteToCheckList);
    }
}
