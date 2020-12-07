
using Newtonsoft.Json;

namespace UniversityApp.BL.DTOs
{
    public class CourseDTO
    {
        [JsonProperty("CourseID")]
        public int CourseID { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Credits")]
        public int Credits { get; set; }
    }
}