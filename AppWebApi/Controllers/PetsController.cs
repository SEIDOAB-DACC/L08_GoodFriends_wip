﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Configuration;
using Models;
using Models.DTO;

using Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PetsController : Controller
    {
        loginUserSessionDto _usr = null;

        IFriendsService _service = null;
        ILogger<PetsController> _logger = null;


        //GET: api/pets/read
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IFriend>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read(string seeded = "true", string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "1000")
        {
            //Convert and check parameters
            bool _seeded = true;
            if (!bool.TryParse(seeded, out _seeded))
            {
                return BadRequest("seeded format error");
            }

            bool _flat = true;
            if (!bool.TryParse(flat, out _flat))
            {
                return BadRequest("flat format error");
            }

            int _pageNr = 0;
            if (!int.TryParse(pageNr, out _pageNr))
            {
                return BadRequest("pageNr format error");
            }

            int _pageSize = 0;
            if (!int.TryParse(pageSize, out _pageSize))
            {
                return BadRequest("pageSize format error");
            }

            var items = await _service.ReadPetsAsync(_usr, _seeded, _flat, filter?.Trim()?.ToLower(), _pageNr, _pageSize);
            return Ok(items);
        }

        //GET: api/pets/readitem
        [HttpGet()]
        [ActionName("Readitem")]
        [ProducesResponseType(200, Type = typeof(IPet))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
          => BadRequest("Not implemented");

        //DELETE: api/pets/deleteitem/id
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
          => BadRequest("Not implemented");

        //GET: api/friends/readitemdto
        [HttpGet()]
        [ActionName("ReadItemDto")]
        [ProducesResponseType(200, Type = typeof(csPetCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
          => BadRequest("Not implemented");

        //PUT: api/pets/updateitem/id
        //Body: Friend in Json
        [HttpPut("{id}")]
        [ActionName("UpdateItem")]
        [ProducesResponseType(200, Type = typeof(csPetCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] csPetCUdto item)
          => BadRequest("Not implemented");

        //POST: api/pets/createitem
        //Body: Friend in Json
        [HttpPost()]
        [ActionName("CreateItem")]
        [ProducesResponseType(200, Type = typeof(csPetCUdto))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] csPetCUdto item)
          => BadRequest("Not implemented");

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        #region constructors
        public PetsController(IFriendsService service, ILogger<PetsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion
    }
}

