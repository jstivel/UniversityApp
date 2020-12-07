using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class CourseItemViewModel : CourseDTO
    {
        private BL.Services.ICourseService courseService;

        public CourseItemViewModel()
        {
            this.courseService = new CourseService();

            EditCourseCommand = new Command(async () => await EditCourse());
            DeleteCourseCommand = new Command(async () => await DeleteCourse());
            //StudentCourseCommand = new Command(async () => await StudentsByCourse());
        }

        public Command EditCourseCommand { get; set; }
        public Command DeleteCourseCommand { get; set; }
        public Command StudentCourseCommand { get; set; }
        async Task EditCourse()
        {
            MainViewModel.GetInstance().EditCourse = new EditCourseViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditCoursePage());
        }

        //async Task StudentsByCourse()
        //{
        //    MainViewModel.GetInstance().Students = new StudentsViewModel(this);
        //    await Application.Current.MainPage.Navigation.PushAsync(new StudentsPage());
        //}

        async Task DeleteCourse()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete Confirm", "Yes", "No");
            if (!answer)
                return;

            var connection = await courseService.CheckConnection();
            if (!connection)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                return;
            }

            await courseService.Delete(Endpoints.DELETE_COURSES, this.CourseID);

            await Application.Current.MainPage.DisplayAlert("Message", "The process is successful", "Cancel");
            var courseViewModel = CoursesViewModel.GetInstance();
            var courseDeleted = courseViewModel.AllCourses.FirstOrDefault(x => x.CourseID == this.CourseID);
            courseViewModel.AllCourses.Remove(courseDeleted);
            courseViewModel.GetCoursesByTitle();
        }
    }
}