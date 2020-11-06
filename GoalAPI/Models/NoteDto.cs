using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid BookId { get; set; }
    }
}
