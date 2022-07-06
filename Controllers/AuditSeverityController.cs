using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AuditSeverityModule.Models;
using AuditSeverityModule.Providers;
using AuditSeverityModule.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditSeverityModule.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuditSeverityController : ControllerBase
    {
        private readonly ISeverityProvider obj;
        public AuditSeverityController(ISeverityProvider _obj)
        {
            obj = _obj;
        }
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));


        // POST: api/AuditSeverity
        
        [HttpPost]
        public IActionResult Post([FromBody]AuditRequest req)    //Change Here
        {
            _log4net.Info(" Http POST request from AuditSeverity");
            if (req == null)
                return BadRequest();
            try
            {
                var response = obj.SeverityResponse(req);
                if (response == null)
                    return BadRequest("Check Your Data");
                return Ok(response);
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured "+e.Message);
                return StatusCode(500);
            }
            
        }
        
    }
}
