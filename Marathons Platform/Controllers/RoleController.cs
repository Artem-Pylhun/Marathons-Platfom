using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Marathons_Platform.Domain.Repositories;
using Marathons_Platform.API.Implementation;
using Marathons_Platform.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
      
        public RoleController(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = _mapper.Map<IEnumerable<Role>>(await _roleRepository.GetAllAsync());
            return Ok(roles);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Role>> AddRole( RoleCreateDto role)
        {
            if (role == null)
            {
                return BadRequest("Role wasn't found");
            }
            await _roleRepository.Add(_mapper.Map<Role>(role));
            return Ok(role);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Role>>> GetRoleById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return NotFound("Role not found");
            return Ok(role);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return NotFound("Role not found");
            await _roleRepository.Delete(role);
            return Ok($"Badge {role.Id} deleted");
        }
        [HttpPut("update")]
        public async Task<ActionResult<Role>> UpdateBadge([FromForm] RoleDto roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            if (role.Id == null)
            {
                return BadRequest("Role not found");
            }

            // Retrieve the existing role from the database
            var existingRole = await _roleRepository.GetByIdAsync(role.Id);
            if (existingRole == null)
            {
                return NotFound("Role not found");
            }

            // Update the title 
            existingRole.Title = roleDTO.Title;

            // Save the updated role to the database
            await _roleRepository.Update(existingRole);

            return Ok(existingRole);
        }
    }
}
