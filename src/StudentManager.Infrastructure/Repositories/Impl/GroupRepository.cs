using StudentManager.Core.Entities;
using StudentManager.Infrastructure.Data;

namespace StudentManager.Infrastructure.Repositories.Impl
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
