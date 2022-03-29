using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(UniversityModel universityModel) : base(universityModel)
        {

        }
    }
}
