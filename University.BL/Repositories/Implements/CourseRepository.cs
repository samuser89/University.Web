using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Models;
using University.BL.DTOs;
using System;

namespace University.BL.Repositories.Implements
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly UniversityModel universityModel;
        public CourseRepository(UniversityModel universityModel) : base(universityModel)
        {
            this.universityModel = universityModel;
        }

        public IEnumerable<Course> GetCoursesByInstructorId(int instructorID)
        {
            var courses = universityModel.CourseInstructor
                .Where(x => x.InstructorID == instructorID)
                .Select(x => x.Course);
            return courses;
        }

        public IEnumerable<DonutExampleDTO> GetReport()
        {
            var result = from _course in universityModel.Course
                         join _enroll in universityModel.Enrollment on _course.CourseID equals _enroll.CourseID
                         group _course by _course.Title into query
                         select query;
            var report = result.Select(x => new DonutExampleDTO 
            {
                Label = x.Key,
                Value = x.Count()
            });
            return report;
        }

        public IEnumerable<DonutExampleDTO> GetReport2()
        {
            var instructor = from _instructorId in universityModel.CourseInstructor
                             join _lastName in universityModel.Instructor on _instructorId.InstructorID equals _lastName.ID
                             group _lastName by _lastName.LastName  into query
                             select query;
            var report = instructor.Select(x => new DonutExampleDTO
            {
                Label = x.Key,
                Value = x.Count()
            });
            return report;
        }

        public IEnumerable<DonutExampleDTO> GetReport3()
        {
            var student = from _studentId in universityModel.Enrollment
                             join _lastName in universityModel.Student on _studentId.StudentID equals _lastName.ID
                             group _lastName by _lastName.LastName into query
                             select query;
            var report = student.Select(x => new DonutExampleDTO
            {
                Label = x.Key,
                Value = x.Count()
            });
            return report;
        }

        public new async Task Delete(int id)
        {
            var enrollments = universityModel.Enrollment.Where(x => x.CourseID == id);
            var courseIntructor = universityModel.CourseInstructor.Where(x => x.CourseID == id);

            if (!enrollments.Any() && !courseIntructor.Any())
            {
                var course = await GetById(id);
                universityModel.Course.Remove(course);
                await universityModel.SaveChangesAsync();
            }
            else
                throw new Exception("El Curso tiene dependencias");
        }
    }
}
