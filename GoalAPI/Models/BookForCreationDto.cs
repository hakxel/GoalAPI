using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    public class BookForCreationDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
        public ICollection<NoteForCreationDto> Notes { get; set; } = new List<NoteForCreationDto>();
    }
}
