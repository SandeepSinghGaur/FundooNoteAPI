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
    public class LabelController : Controller
    {
        private readonly ILabelManager labelManager;

        private readonly IConfiguration configuration;

        public LabelController(ILabelManager labelManager, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.labelManager = labelManager;
        }
        [HttpPost]
        public ActionResult AddLabel(Label label)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.labelManager.AddLabel(userId,label);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Added Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Label Added UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
        private int TokenUserId()
        {
            return Convert.ToInt32(User.FindFirst("userId").Value);
        }
        [HttpGet]
        [Route("{labelId}")]
        public ActionResult GetLabel(int labelId)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.labelManager.GetLabel(userId, labelId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Label Get UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        public ActionResult GetAllLabel()
        {
            try
            {
                int userId = TokenUserId();
                var result = this.labelManager.GetAllLabel(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Label Get Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "All Label Get UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        public ActionResult DeleteLabel(int labelId)
        {
            try
            {
                int userId = TokenUserId();
                var result = this.labelManager.DeleteLabel(userId, labelId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Label Deleted Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Label Deleted UnSuccessFully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }

        }

    }
}
