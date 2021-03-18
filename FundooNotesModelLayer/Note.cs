using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooNotesModelLayer
{
    public class Note
    {

        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public bool IsPin { get; set; } = false;
        public string Description { get; set; }
        public bool IsArchive { get; set; } = false;
        public bool IsTrash { get; set; } = false;
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime NoteCreatedDate { get; set; }
        public DateTime NoteModifiedDate { get; set; }
        public DateTime? Remainder { get; set; }
        [ForeignKey("UserRegistration")]
        public long UserId { get; set; }
        


    }
}
