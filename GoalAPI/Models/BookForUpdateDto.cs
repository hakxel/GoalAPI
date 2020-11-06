using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    public class BookForUpdateDto
    {
        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
    }
}
