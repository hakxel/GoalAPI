using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    /// <summary>
    /// A note for update with a content field
    /// </summary>
    public class NoteForUpdateDto
    {
        /// <summary>
        /// The content of the note
        /// </summary>
        public string Content { get; set; }
    }
}
