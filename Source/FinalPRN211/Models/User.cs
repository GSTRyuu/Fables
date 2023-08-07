using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class User
    {
        public User()
        {
            Histories = new HashSet<History>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<History> Histories { get; set; }
    }
}
