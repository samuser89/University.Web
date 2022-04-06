﻿using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The field Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field First MidName is required")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "The field Enrollment Date is required")]
        public DateTime EnrollmentDate { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstMidName);
            }
        }

        public string EnrollmentDateFormat
        {
            get
            {
                return EnrollmentDate.ToString("yyyy-MM-dd");
            }
        }



    }
}
