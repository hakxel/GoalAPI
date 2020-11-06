using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Author { get; set; }

        public ICollection<Note> Notes { get; set; }
            = new List<Note>();
    }
}
