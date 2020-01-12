using FootBallStats.Data.CompetitionLeagueTable;
using FootBallStats.ServiceProvider;
using FootBallStats.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FootBallStats.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeagueTablePage : ContentPage
	{
        readonly ServiceManager manager = new ServiceManager();
        readonly IList<Standing> _leagueTable = new ObservableCollection<Standing>();
        readonly int _competitionId;

        public LeagueTablePage (int id, string caption)
		{
            InitializeComponent();
            LoadData();
            _competitionId = id;
            lstCompetitionLeagueTable.IsGroupingEnabled = false;
            lstCompetitionLeagueTable.BindingContext = _leagueTable;

            this.Title = caption;
        }

        public ObservableCollection<Grouping<string, Standing>>
        BindingWithGrouping(string searchText = "")
        {

            var result = _leagueTable;

            if (!String.IsNullOrEmpty(searchText) && searchText.Length >= 1)
            {
                result = result.Where(x => x.teamName.ToLower().StartsWith(
                    searchText.ToLower())).ToList();
            }

            var list = new ObservableCollection<Grouping<string, Standing>>
                (result.
                GroupBy(c => c.teamName[0].ToString()
                ).Select(k => new Grouping<string, Standing>(k.Key, k)));

            return list;
        }

        private void onTextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                lstCompetitionLeagueTable.IsGroupingEnabled = false;
                lstCompetitionLeagueTable.BindingContext = _leagueTable;

            }
            else
            {
                lstCompetitionLeagueTable.IsGroupingEnabled = true;
                lstCompetitionLeagueTable.BindingContext = BindingWithGrouping(e.NewTextValue);
            }

        }

        private void onRefresh(object sender, EventArgs e)
        {
            LoadData();
            lstCompetitionLeagueTable.IsGroupingEnabled = false;
            lstCompetitionLeagueTable.BindingContext = _leagueTable;
            lstCompetitionLeagueTable.IsRefreshing = false;
        }

        private async void LoadData()
        {
            this.IsBusy = true;
            LeagueTableIndicatorStack.IsVisible = true;
            LeagueTableStack.IsVisible = false;
            txtSearch.IsVisible = false;
            _leagueTable.Clear();

            await Task.Delay(250);
            var restCompetitionLeague = await manager.CompetitionLeagueTable(_competitionId);
            foreach (var item in restCompetitionLeague.standing)
            {
                _leagueTable.Add(item);
            }

            LeagueTableIndicatorStack.IsVisible = false;
            txtSearch.IsVisible = true;
            LeagueTableStack.IsVisible = true;

            this.IsBusy = false;
        }
    }
}