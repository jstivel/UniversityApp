using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class CourseInstructorsViewModel : BaseViewModel
    {
        private BL.Services.ICourseInstructorService courseInstructorService;
        private ObservableCollection<CourseInstructorDTO> courseInstructor;
        private bool isRefreshing;

        public ObservableCollection<CourseInstructorDTO> CourseInstructor
        {
            get { return this.courseInstructor; }
            set { this.SetValue(ref this.courseInstructor, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public CourseInstructorsViewModel()
        {
            this.courseInstructorService = new CourseInstructorService();
            this.RefreshCommand = new Command(async () => await GetCourseInstructors());
            this.RefreshCommand.Execute(null);
        }

        public Command RefreshCommand { get; set; }

        async Task GetCourseInstructors()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await courseInstructorService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                    return;
                }

                var listCourseInstructors = await courseInstructorService.GetAll(Endpoints.GET_COURSE_INSTRUCTORS);
                this.CourseInstructor = new ObservableCollection<CourseInstructorDTO>(listCourseInstructors);
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