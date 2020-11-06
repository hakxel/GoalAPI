using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public Guid BookId { get; set; }
    }
}
