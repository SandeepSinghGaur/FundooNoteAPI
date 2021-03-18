using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
        }
        [HttpPost]
        public ActionResult AddCollaborator(Collaborator collaborator)
        {
            try
            {
                long userId = TokenUserId();
                var result = this.collaboratorManager.AddCollaborator(userId,collaborator);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborator Added Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Collaboratot Added UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
        private string EmailToken()
        {
            return Convert.ToString(User.FindFirst("email").Value);
        }
        private long TokenUserId()
        {
            return Convert.ToInt64(User.FindFirst("userId").Value);
        }
        [HttpDelete]
        public ActionResult RemoveCollaborator(int collaboratorId)
        {
            try
            {
                long userId = TokenUserId();
                var result = this.collaboratorManager.RemoveCollaborator(userId,collaboratorId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "collaborator Deleted Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "collaborator Deleted UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        public ActionResult GetCollaborator()
        {
            try
            {
                string email = EmailToken();
                long userId = TokenUserId();
                var result = this.collaboratorManager.GetCollaborator(email,userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaborator Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Collaborator Get UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }

    }
}
