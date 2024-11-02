using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Groups.CommandServices;
using Project.Application.Groups.QueryServices;
using Project.Domain.Groups.Model.Entities;

namespace Project.Presentation.Groups.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly GroupCommandService _commandService;
        private readonly GroupQueryService _queryService;

        public GroupsController(GroupCommandService commandService, GroupQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            var groups = await _queryService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            var group = await _queryService.GetGroupByIdAsync(id);
            return group == null ? NotFound() : Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(Group group)
        {
            var createdGroup = await _commandService.CreateGroupAsync(group);
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id }, createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, Group group)
        {
            if (id != group.Id) return BadRequest();
            await _commandService.UpdateGroupAsync(group);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await _commandService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}