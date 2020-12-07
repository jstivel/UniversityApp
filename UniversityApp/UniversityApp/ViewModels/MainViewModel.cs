using System.Threading.Tasks;
using UniversityApp.Views;
using Xamarin.Forms;

namespace UniversityApp.ViewModels
{
    public class MainViewModel
    {
        
        public CourseInstructorsViewModel CourseInstructors { get; set; }
        public MoviesViewModel Movies { get; set; }
        public CreateStudentViewModel CreateStudent { get; set; }
        public CreateInstructorViewModel CreateInstructor { get; set; }
        public CreateCourseViewModel CreateCourse { get; set; }
        public EditCourseViewModel EditCourse { get; set; }
        public EditInstructorViewModel EditInstructor { get; set; }
        public EditStudentViewModel EditStudent { get; set; }
        public CoursesViewModel Courses { get; set; }
        public StudentsViewModel Students { get; set; }
        public LoginViewModel Login { get; set; }
        public HomeViewModel Home { get; set; }
        public InstructorsViewModel Instructors { get; set; }
     
        public MainViewModel()
        {
            instance = this;

            Students = new StudentsViewModel();
            Instructors = new InstructorsViewModel();
            Courses = new CoursesViewModel();
            CourseInstructors = new CourseInstructorsViewModel();
            
            Login = new LoginViewModel();
            Home = new HomeViewModel();
            
            CreateCourse = new CreateCourseViewModel();
            CreateStudent = new CreateStudentViewModel();
            CreateInstructor = new CreateInstructorViewModel();

            //Movies = new MoviesViewModel();

            CreateCourseCommand = new Command(async () => await GoToCreateCourse());
            CreateInstructorCommand = new Command(async () => await GoToCreateInstructor());
            CreateStudentCommand = new Command(async () => await GoToCreateStudent());
        }

        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }

        public Command CreateCourseCommand { get; set; }
        async Task GoToCreateCourse()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateCoursePage());
        }
        public Command CreateInstructorCommand { get; set; }
        async Task GoToCreateInstructor()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateInstructorPage());
        }

        public Command CreateStudentCommand { get; set; }
        async Task GoToCreateStudent()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateStudentPage());
        }
    }
}
