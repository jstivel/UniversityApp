using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class EnrollmentsViewModel : BaseViewModel
    {
        private BL.Services.IEnrollmentsService EnrollmentsService;
        private ObservableCollection<EnrollmentsDTO> enrollments;
        private bool isRefreshing;

        public ObservableCollection<EnrollmentsDTO> Enrollments
        {
            get { return this.enrollments; }
            set { this.SetValue(ref this.enrollments, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public EnrollmentsViewModel()
        {
            this.EnrollmentsService = new EnrollmentsService();
            this.RefreshCommand = new Command(async () => await GetEnrollments());
            this.RefreshCommand.Execute(null);
        }

        public Command RefreshCommand { get; set; }

        async Task GetEnrollments()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await EnrollmentsService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }

                var listEnrollments = await EnrollmentsService.GetAll(Endpoints.GET_Enrollments);
                this.Enrollments = new ObservableCollection<EnrollmentsDTO>(listEnrollments);
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
