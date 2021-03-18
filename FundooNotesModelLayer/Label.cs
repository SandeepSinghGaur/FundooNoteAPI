using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooNotesModelLayer
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }
        public string Labels { get; set; }
        public long UserId { get; set; }
        [ForeignKey("NoteId")]
        public int NoteId { get; set; }
    }
}
