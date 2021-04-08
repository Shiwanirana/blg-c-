using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blg_server.Models;
using blg_server.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace blg_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly BlgsService _bs;
        public AccountController(ProfilesService ps, BlgsService bs)
        {
            _ps = ps;
            _bs = bs;
        }
        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
              Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
              return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("blgs")]
        public async Task<ActionResult<IEnumerable<Blg>>> GetBlgsByProfileIdAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                IEnumerable<Blg> blgs = _bs.GetByAccountId(userInfo.Id);
                return Ok(blgs); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}