using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {
        private BL.Services.IDepartmentsService DepartmentsService;
        private ObservableCollection<DepartmentsDTO> departments;
        private bool isRefreshing;

        public ObservableCollection<DepartmentsDTO> Departments
        {
            get { return this.departments; }
            set { this.SetValue(ref this.departments, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public DepartmentsViewModel()
        {
            this.DepartmentsService = new DepartmentsService();
            this.RefreshCommand = new Command(async () => await GetDepartments());
            this.RefreshCommand.Execute(null);
        }

        public Command RefreshCommand { get; set; }

        async Task GetDepartments()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await DepartmentsService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }

                var listDepartments = await DepartmentsService.GetAll(Endpoints.GET_Departments);
                this.Departments = new ObservableCollection<DepartmentsDTO>(listDepartments);
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
