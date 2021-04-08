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
    public class BlgsController : ControllerBase
    {
        private readonly BlgsService _bs;
        public BlgsController(BlgsService bs)
        {
            _bs = bs;
        }
    
    [HttpGet]
    public ActionResult<IEnumerable<Blg>> Get()
    {
      try
      {
        return Ok(_bs.GetAll());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Blg>> Get(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        {
        Blg blg = _bs.GetById(id);
        return Ok(blg);
        }
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Blg>> Post([FromBody] Blg newBlg)
        {
            try
            {
             Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
             newBlg.CreatorId = userInfo.Id;
             Blg created = _bs.Create(newBlg);
             created.Creator = userInfo; 
             return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Blg>> Edit([FromBody] Blg editBlg, int id)
        {
            try{
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editBlg.Id= id;
                editBlg.Creator = userInfo;
                return Ok(_bs.Edit(editBlg, userInfo.Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try{
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_bs.Delete(id,userInfo.Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}