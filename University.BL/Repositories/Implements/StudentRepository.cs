using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityModel universityModel) : base(universityModel)
        {

        }
    }
}
