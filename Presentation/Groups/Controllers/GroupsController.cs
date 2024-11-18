using Microsoft.AspNetCore.Mvc;
using Application.Groups.CommandServices;
using Application.Groups.QueryServices;
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
        
    }
}