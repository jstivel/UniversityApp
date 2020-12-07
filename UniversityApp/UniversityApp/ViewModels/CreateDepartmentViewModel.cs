using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class CreateDepartmentViewModel:BaseViewModel
    {
        private BL.Services.IDepartmentsService departmentsService;
        private int departmentID;
        private string name;
        private double budget;
        private DateTime startDate;
        private int instructorID;

        private bool isEnabled;
        private bool isRunning;

        public int DepartmentID
        {
            get { return this.departmentID; }
            set { this.SetValue(ref this.departmentID, value); }
        }

        public string Name
        {
            get { return this.name; }
            set { this.SetValue(ref this.name, value); }
        }

        public double Budget
        {
            get { return this.budget; }
            set { this.SetValue(ref this.budget, value); }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.SetValue(ref this.startDate, value); }
        }

        public int InstructorID
        {
            get { return this.instructorID; }
            set { this.SetValue(ref this.instructorID, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public CreateDepartmentViewModel()
        {
            this.departmentsService = new DepartmentsService();
            this.SaveCommand = new Command(async () => await CreateDepartment());

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        public Command SaveCommand { get; set; }

        async Task CreateDepartment()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Name))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter the field title", "Cancel");
                    return;
                }

                this.IsRunning = true;
                this.IsEnabled = false;

                var connection = await departmentsService.CheckConnection();
                if (!connection)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }

                var departmentDTO = new DepartmentsDTO { Name = this.Name, StartDate = this.StartDate, Budget = this.Budget, InstructorID = this.InstructorID };
                await departmentsService.Create(Endpoints.POST_Departments, departmentDTO);

                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert("Message", "The process is successful", "Cancel");

                this.DepartmentID = this.InstructorID = 0;
                this.Budget = 0;
                this.Name = string.Empty;
                //this.StartDate = DateTime.Now;

                //Application.Current.MainPage = new NavigationPage(new CoursesPage());
            }
            catch (Exception ex)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }
    }
}
