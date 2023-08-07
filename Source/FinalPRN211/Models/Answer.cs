using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? Content { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Question Question { get; set; } = null!;
    }
}
