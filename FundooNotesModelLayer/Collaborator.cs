using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooNotesModelLayer
{
   public class Collaborator
    {
        [Key]
        public int CollaboratorId { get; set; }
        public long UserId { get; set; }
        public string ReceiverEmail { get; set; }
        [ForeignKey("NoteId")]
        public int NoteId { get; set; }

    }
}
