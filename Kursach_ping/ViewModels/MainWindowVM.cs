using Kursach_ping.ApiModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach_ping.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private List<SiteToCheckDto> _apiCollection = new List<SiteToCheckDto>();

        public MainWindowVM()
        {
            
        }

        public string WebResourceURL { get; set; }

        public ObservableCollection<SiteWithStatusDto> SiteCollection { get; set; } = new ObservableCollection<SiteWithStatusDto>() 
        {
            
        };

        private RelayCommand _addNewCheckCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand AddNewCheckCommand => _addNewCheckCommand ??= new RelayCommand(AddNewCheck);

        private async void AddNewCheck()
        {
            _cancelTokenSource.Cancel();

            if (WebResourceURL == null || WebResourceURL == String.Empty)
                return;

            try
            {
                var responce = await Client.CheckSite(new SiteToCheckDto()
                {
                    URL = WebResourceURL,
                    CheckDomainOnly = false,
                });

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SiteCollection.Add(await responce.Content.ReadFromJsonAsync<SiteWithStatusDto>());
                    _apiCollection = CastToApiCollection(SiteCollection);
                }
                else
                {
                    MessageBox.Show(await responce.Content.ReadAsStringAsync());
                }

                _cancelTokenSource = new CancellationTokenSource();

                await CheckAllSites(_cancelTokenSource.Token);
            }
            catch
            {
                MessageBox.Show("Соединение с сервером не установлено...");
            }
        }

        private List<SiteToCheckDto> CastToApiCollection(ObservableCollection<SiteWithStatusDto> collection)
        {
            var result = new List<SiteToCheckDto>();

            foreach (var item in collection)
            {
                result.Add(new SiteToCheckDto
                {
                    URL = item.URL,
                    CheckDomainOnly = false
                });
            }

            return result;
        }

        private async Task CheckAllSites(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(10000, token);
                    var response = await Client.CheckSiteList(_apiCollection);

                    if (!token.IsCancellationRequested)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            SiteCollection = new ObservableCollection<SiteWithStatusDto>(await response.Content.ReadFromJsonAsync<List<SiteWithStatusDto>>());
                        }
                        else
                        {
                            MessageBox.Show(await response.Content.ReadAsStringAsync());
                        }
                    }
                    else
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Остановочка фоновой задачи!");
            }
        }

    }
}
