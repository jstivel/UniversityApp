using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BL.DTOs;
using UniversityApp.BL.Services.Implements;
using UniversityApp.Helpers;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class InstructorItemViewModel : InstructorDTO
    {
        private BL.Services.IInstructorService instructorService;

        public InstructorItemViewModel()
        {
            this.instructorService = new InstructorService();

            EditInstructorCommand = new Command(async () => await EditInstructor());
            DeleteInstructorCommand = new Command(async () => await DeleteInstructor());
            
        }

        public Command EditInstructorCommand { get; set; }
        public Command DeleteInstructorCommand { get; set; }
        
        async Task EditInstructor()
        {
            MainViewModel.GetInstance().EditInstructor = new EditInstructorViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditInstructorPage());
        }

       
        async Task DeleteInstructor()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete Confirm", "Yes", "No");
            if (!answer)
                return;

            var connection = await instructorService.CheckConnection();
            if (!connection)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection", "Cancel");
                return;
            }

            await instructorService.Delete(Endpoints.DELETE_Instructor, this.ID);

            await Application.Current.MainPage.DisplayAlert("Message", "The process is successful", "Cancel");
            var instructorViewModel = InstructorsViewModel.GetInstance();
            var instructorDeleted = instructorViewModel.AllInstructors.FirstOrDefault(x => x.ID == this.ID);
            instructorViewModel.AllInstructors.Remove(instructorDeleted);
            instructorViewModel.GetInstructorsByFirstMidName();
        }
    }
}