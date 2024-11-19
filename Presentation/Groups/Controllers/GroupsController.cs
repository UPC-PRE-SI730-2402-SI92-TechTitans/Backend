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
        
        private Guid GetUserIdFromToken()
        {
            // Simulación de decodificación del token JWT.
            // Aquí puedes implementar la lógica para obtener el `userId` real del token.
            // Por ahora, retornaremos un valor de prueba.
            return Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var userId = GetUserIdFromToken();
            var groups = await _queryService.GetAllGroupsAsync();

            // Filtrar los grupos donde el usuario está presente como participante
            var filteredGroups = groups
                .Where(g => g.Participants.Any(p => p.Id == userId))
                .Select(g => g.ToResource())
                .ToList();

            return Ok(filteredGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var userId = GetUserIdFromToken();
            var group = await _queryService.GetGroupByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            // Verificar si el usuario está presente en el grupo
            if (!group.Participants.Any(p => p.Id == userId))
            {
                return Forbid("El usuario no es miembro del grupo.");
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

            var userId = GetUserIdFromToken();
            groupDto.Id = null;
            groupDto.CreationDate = DateTime.UtcNow;

            // Añadir al usuario autenticado como participante
            var userParticipant = new ParticipantDto
            {
                Id = userId,
                Name = "Usuario Autenticado", // Puedes obtener el nombre real del usuario del token si está disponible
                Amount = 0,
                PendingPayment = 0,
                Date = DateTime.UtcNow
            };

            groupDto.Participants ??= new List<ParticipantDto>();
            groupDto.Participants.Add(userParticipant);

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
                return BadRequest("El ID del grupo no coincide.");
            }

            var userId = GetUserIdFromToken();
            var group = await _queryService.GetGroupByIdAsync(id);

            if (group == null || !group.Participants.Any(p => p.Id == userId))
            {
                return Forbid("El usuario no tiene permisos para modificar este grupo.");
            }

            var updatedGroup = groupDto.ToDomainModel();
            await _commandService.UpdateGroupAsync(updatedGroup);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var userId = GetUserIdFromToken();
            var group = await _queryService.GetGroupByIdAsync(id);

            if (group == null || !group.Participants.Any(p => p.Id == userId))
            {
                return Forbid("El usuario no tiene permisos para eliminar este grupo.");
            }

            await _commandService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}