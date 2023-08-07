using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public string? Answer { get; set; }
        public string? Question { get; set; }
        public int? Mark { get; set; }
        public DateTime? Date { get; set; }

        public virtual Test Test { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
