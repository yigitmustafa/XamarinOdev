using FootBallStats.Data.Competitions;
using FootBallStats.Pages;
using FootBallStats.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FootBallStats
{
	public partial class MainPage : ContentPage
	{
        readonly ServiceManager manager = new ServiceManager();
        readonly IList<RootObject> _competition = new ObservableCollection<RootObject>();

        public MainPage()
		{
			InitializeComponent();

            InitializeComponent();
            LoadData();

            lstCompetition.ItemsSource = _competition;
            this.BindingContext = _competition;

            lstCompetition.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var action = await DisplayActionSheet("Navigation", "Cancel", null, "Teams", "League Table", "Fixtures");
                if (action == "Teams")
                {
                    var lv = (ListView)sender;
                    var competitionSelected = (RootObject)e.SelectedItem;
                    await Navigation.PushAsync(new TeamList(competitionSelected.id, competitionSelected.caption));
                    lv.SelectedItem = null;
                }
                else if (action == "League Table")
                {
                    var lv = (ListView)sender;
                    var competitionSelected = (RootObject)e.SelectedItem;
                    await Navigation.PushAsync(new LeagueTablePage(competitionSelected.id, competitionSelected.caption));
                    lv.SelectedItem = null;
                }

            };
        }

        private async void LoadData()
        {
            this.IsBusy = true;
            CompetitionIndicatorStack.IsVisible = true;
            CompetitionStack.IsVisible = false;
            _competition.Clear();
            await Task.Delay(250);
            var restCompetition = await manager.Competition();

            foreach (var item in restCompetition)
            {
                _competition.Add(item);
            }
            CompetitionIndicatorStack.IsVisible = false;
            CompetitionStack.IsVisible = true;
            this.IsBusy = false;
        }
    }
}
