using StudentManager.SharedKernel;
using System.Collections.Generic;

namespace StudentManager.Core.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public List<StudentGroup> StudentGroups { get; set; }

        public Group()
        {
            StudentGroups = new List<StudentGroup>();
        }
    }
}
