using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class Test
    {
        public Test()
        {
            Histories = new HashSet<History>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; } = null!;
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
