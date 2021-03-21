using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteManager noteManager;
        private readonly IConfiguration configuration;
        public NoteController(INoteManager noteManager, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.noteManager = noteManager;
        }
        [HttpPost]
        public ActionResult AddNote(Note note)
        {
            try
            {
                int userId = TokenUserId();
                note.UserId = userId;
                var result = this.noteManager.AddNote(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Added Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Note Added UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
        [HttpGet]
        [Route("{noteId}")]
        public ActionResult GetNote( int noteId)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.GetNote(userId, noteId);
                if (result != null)
                {
                   return this.Ok(new { Status = true, Message = "Note Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Note Get UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("PinNote/{noteId}")]
        public ActionResult PinNote(int noteId)
        {
            try
            {
                var result = this.noteManager.PinNote(noteId);
                if (result != 0)
                {
                   return this.Ok(new { Status = true, Message = "Note Pin Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Note Pin UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("ArchiveNote/{noteId}")]
        public ActionResult ArchiveNote(int noteId)
        {
            try
            {
                var result = this.noteManager.ArchiveNote(noteId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Archive Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Archive UnuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        public ActionResult UpdateNote( Note newNote)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.UpdateNote(userId, newNote);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Update Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Note Update SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [HttpDelete]
        [Route("DeleteNoteFromTrash/{noteId}")]
        public ActionResult DeleteNotePermanently(int noteId)
        {
            try
            {
                var result = this.noteManager.DeleteNotePermanently(noteId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Permanently Note Deleted Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Permanently Note Deleted SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [HttpDelete]
        [Route("{noteId}")]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.DeleteNote(userId, noteId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Note Deleted SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        //Retrives the first claim that is matched bt specified predicate
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("userId").Value);
        }
        [HttpGet]
        public ActionResult GetAllNotes()
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.GetAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Note Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "All Note Get SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [HttpGet]
        [Route("GetAllTrashNote")]
        public ActionResult GetAllTrashNote()
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.GetAllTrashNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Trash Note Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "All Trash Note Get SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [HttpGet]
        [Route("GetAllArchiveNote")]
        public ActionResult GetAllArchiveNote()
        {
            try
            {
                int userId = TokenUserId();
                var result = this.noteManager.GetAllArchiveNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Archive Note Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "All Archive Note Get SuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }
        [Route("Image")]
        [HttpPost]
        public IActionResult Image(IFormFile file, int id)
        {
            try
            {
                var result = this.noteManager.Image(file, id);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Image Upload Successful", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Image Upload Un-Successful" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
