using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(UniversityModel universityModel) : base(universityModel)
        {

        }
    }
}
