
namespace UniversityApp.Helpers
{
    public class Endpoints
    {
        #region Courses
        public static string GET_COURSES = "api/Courses/GetCourses/";
        public static string GET_COURSE = "api/Courses/GetCourse/";
        public static string GET_COURSE_BY_STUDENT = "api/Courses/GetCoursesByStudentId/";
        public static string GET_COURSE_BY_INSTRUCTOR = "api/Courses/GetCoursesByInstructorId/";
        public static string POST_COURSES = "api/Courses/";
        public static string PUT_COURSES = "api/Courses/";
        public static string DELETE_COURSES = "api/Courses/";
        #endregion

        #region Courses_instructor
        public static string GET_COURSE_INSTRUCTORS = "/api/CourseInstructors/";
        #endregion

        #region Departments
        public static string GET_Departments = "/api/Departments";
        public static string GET_Department = "/api/Departments";
        public static string POST_Departments = "/api/Departments";
        public static string PUT_Departments = "/api/Departments";
        public static string DELETE_Departments = "/api/Departments";
        #endregion

        #region Enrollments
        public static string GET_Enrollments = "/api/Enrollments";
        public static string GET_Enrollment = "/api/Enrollments";
        public static string POST_Enrollment = "/api/Enrollments";
        public static string PUT_Enrollment = "/api/Enrollments";
        public static string DELETE_Enrollment = "/api/Enrollments";
        #endregion

        #region Instructors
        public static string GET_Instructors = "/api/Instructors/GetInstructors";
        public static string GET_Instructor = "/api/Instructors/GetInstructors";
        public static string GET_Instructor_BY_COURSE = "/api/Instructors/GetInstructorsByCourseId";
        public static string PUT_Instructor = "/api/Instructors/";
        public static string DELETE_Instructor = "/api/Instructors/";
        public static string POST_Instructor = "/api/Instructors";
        #endregion

        #region OfficeAssignments
        public static string GET_OfficeAssignments = "/api/OfficeAssignments";
        public static string GET_OfficeAssignment = "/api/OfficeAssignments";
        public static string POST_OfficeAssignment = "/api/OfficeAssignments";
        public static string PUT_OfficeAssignment = "/api/OfficeAssignments";
        public static string DELETE_OfficeAssignment = "/api/OfficeAssignments";
        #endregion

        #region Students
        public static string GET_Students = "/api/Students/GetStudents";
        public static string GET_Student = "/api/Students/GetStudent";
        public static string GET_Students_BY_COURSES = "/api/Students/GetStudentsByCourseId";
        public static string POST_Students = "/api/Students";
        public static string PUT_Students = "/api/Students/";
        public static string DELETE_Students = "/api/Students/";
        #endregion
    }
}