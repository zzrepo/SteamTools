using DynamicData;
using DynamicData.Binding;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using System.Application.Models;
using System.Application.Services;
using System.Application.Settings;
using System.Application.UI.Resx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Properties;
using System.Reactive.Linq;

namespace System.Application.UI.ViewModels
{
    public class SaveEditedAppInfoWindowViewModel : WindowViewModel
    {
        public static string DisplayName => AppResources.GameList_EditedAppsSaveManger;

        public SaveEditedAppInfoWindowViewModel()
        {
            Title = GetTitleByDisplayName(DisplayName);


            _SteamEditedAppsSourceList = new SourceCache<SteamApp, long>(t => t.AppId);

            _SteamEditedAppsSourceList
              .Connect()
              .ObserveOn(RxApp.MainThreadScheduler)
              .Sort(SortExpressionComparer<SteamApp>.Ascending(x => x.AppId))
              .Bind(out _SteamEditedApps)
              .Subscribe(_ => this.RaisePropertyChanged(nameof(IsSteamEditedAppsEmpty)));

            LoadSteamEditedApps();
        }

        readonly SourceCache<SteamApp, long> _SteamEditedAppsSourceList;
        readonly ReadOnlyObservableCollection<SteamApp>? _SteamEditedApps;
        public ReadOnlyObservableCollection<SteamApp>? SteamEditedApps => _SteamEditedApps;


        public bool IsSteamEditedAppsEmpty => !SteamEditedApps.Any_Nullable();


        public void LoadSteamEditedApps()
        {
            _SteamEditedAppsSourceList.Clear();

            _SteamEditedAppsSourceList.AddOrUpdate(SteamConnectService.Current.SteamApps.Items.Where(s => s.IsEdited));
        }

        public void SaveSteamEditedApps()
        {
            ISteamService.Instance.SaveAppInfosToSteam();
        }
        
        public async void ClearSteamEditedApps()
        {
            if (await MessageBox.ShowAsync("确定要清空所有的已修改数据吗？(该操作不可还原)", ThisAssembly.AssemblyTrademark, MessageBox.Button.OKCancel) == MessageBox.Result.OK)
            {
                _SteamEditedAppsSourceList.Clear();
            }
        }

        public void EditSteamApp(SteamApp app)
        {
            ISteamService.Instance.SaveAppInfosToSteam();
        }
    }
}