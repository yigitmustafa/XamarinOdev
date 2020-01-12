using FootBallStats.Data.CompetitionTeams;
using FootBallStats.ServiceProvider;
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
	public partial class TeamList : ContentPage
	{

        readonly ServiceManager manager = new ServiceManager();
        readonly IList<Team> _teams = new ObservableCollection<Team>();
        int _competitionId;

        public TeamList (int id, string caption)
		{
			InitializeComponent ();

            InitializeComponent();
            _competitionId = id;
            LoadData();
            this.BindingContext = _teams;
            this.Title = caption;
            lstCompetitionTeams.ItemsSource = _teams;
        }

        private async void LoadData()
        {
            this.IsBusy = true;
            TeamListIndicatorStack.IsVisible = true;
            TeamListStack.IsVisible = false;
            _teams.Clear();
            await Task.Delay(250);
            var restCompetitionTeams = await manager.CompetitionTeams(_competitionId);

            foreach (var item in restCompetitionTeams.teams)
            {
                _teams.Add(item);
            }

            TeamListIndicatorStack.IsVisible = false;
            TeamListStack.IsVisible = true;
            this.IsBusy = false;
        }
    }
}