using StudentManager.Core.Entities;
using StudentManager.Infrastructure.Data;

namespace StudentManager.Infrastructure.Repositories.Impl
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
