﻿using Newtonsoft.Json;

namespace University.BL.DTOs
{
    public class EnrollmentDTO
    {        

        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }
        
        public int StudentID { get; set; }
       
        public int Grade { get; set; }       
    }

}
