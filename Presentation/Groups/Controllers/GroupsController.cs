using Microsoft.AspNetCore.Mvc;
using Application.Groups.CommandServices;
using Application.Groups.QueryServices;
using Presentation.Groups.Resources;
using Presentation.Groups.Transform;
using Domain.Groups.Model.Entities;

namespace Presentation.Groups.Controllers
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
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _queryService.GetAllGroupsAsync();
            var result = groups.Select(g => g.ToResource()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _queryService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            var result = group.ToResource();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            groupDto.Id = null;
            foreach (var participant in groupDto.Participants ?? new List<ParticipantDto>())
            {
                participant.Id = null;
            }

            var group = groupDto.ToDomainModel();
            await _commandService.CreateGroupAsync(group);
            var result = group.ToResource();
            return CreatedAtAction(nameof(GetGroupById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] GroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groupDto.Id)
            {
                return BadRequest("The group ID doesn't match.");
            }

            var group = groupDto.ToDomainModel();
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