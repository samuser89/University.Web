using Newtonsoft.Json;

namespace University.BL.DTOs
{
    public class CourseDTO
    {        
        public int CourseID { get; set; }
        
        public string Title { get; set; }
       
        public int Credits { get; set; }       
    }

    public class DonutCourseDTO
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
