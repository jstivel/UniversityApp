using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class StudentItemViewModel : StudentDTO
    {
        private BL.Services.IStudentService studentService;

        public StudentItemViewModel()
        {
            this.studentService = new StudentService();

            EditStudentCommand = new Command(async () => await EditStudent());
            DeleteStudentCommand = new Command(async () => await DeleteStudent());
     
        }

        public Command EditStudentCommand { get; set; }
        public Command DeleteStudentCommand { get; set; }

        async Task EditStudent()
        {
            MainViewModel.GetInstance().EditStudent = new EditStudentViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditStudentPage());
        }

        async Task StudentsByStudent()
        {
            MainViewModel.GetInstance().Students = new StudentsViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new StudentsPage());
        }

        async Task DeleteStudent()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete Confirm", "Yes", "No");
            if (!answer)
                return;

            var connection = await studentService.CheckConnection();
            if (!connection)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                return;
            }

            await studentService.Delete(Endpoints.DELETE_Students, this.ID);

            await Application.Current.MainPage.DisplayAlert("Message", "The process is successful", "Cancel");
            var studentViewModel = StudentsViewModel.GetInstance();
            var studentDeleted = studentViewModel.AllStudents.FirstOrDefault(x => x.ID == this.ID);
            studentViewModel.AllStudents.Remove(studentDeleted);
            studentViewModel.GetStudentsByFirstMidName();
        }
    }
}