namespace UniversityApp.BL.DTOs
{
    public class OfficeAssignmentsDTO
    {
        public int InstructorID { get; set; }
        public string Location { get; set; }
        public InstructorDTO Instructor { get; set; }
    }
}
