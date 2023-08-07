using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string? Content { get; set; }
        public bool IsMore { get; set; }

        public virtual Test Test { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
