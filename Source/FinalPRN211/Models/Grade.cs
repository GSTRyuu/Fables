using System;
using System.Collections.Generic;

namespace FinalPRN211.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public int? Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
