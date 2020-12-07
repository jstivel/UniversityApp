namespace UniversityApp.BL.DTOs
{
    public class EnrollmentsDTO
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
    }
}
