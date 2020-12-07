using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        private BL.Services.IMovieService movieService;
        private ObservableCollection<SearchDTO> movies;
        private bool isRefreshing;
        private int count;

        public ObservableCollection<SearchDTO> Movies
        {
            get { return this.movies; }
            set { this.SetValue(ref this.movies, value); }
        }

        public int Count
        {
            get { return this.count; }
            set { this.SetValue(ref this.count, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public MoviesViewModel()
        {
            this.movieService = new MovieService();
            this.RefreshCommand = new Command(async () => await GetMovies());
            this.RefreshCommand.Execute(null);
        }

        public Command RefreshCommand { get; set; }

        async Task GetMovies()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await movieService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }

                string title = "avengers";
                var listMovies = await movieService.GetById("?apikey=e4078ac3&s=" + title, 0);
                this.Movies = new ObservableCollection<SearchDTO>(listMovies.Search);
                this.Count = Convert.ToInt32(listMovies.totalResults);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }
    }
}