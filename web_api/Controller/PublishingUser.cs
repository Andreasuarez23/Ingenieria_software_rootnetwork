using Microsoft.AspNetCore.Mvc;
using entity_framework.publishing;
using entities_library.publishing;
using System.Collections.Generic;
using System.Threading.Tasks;
using dao_library.Interfaces.publishing;

namespace web_api.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    
    public class PublishingUserController : ControllerBase
    {
        private readonly IDAOPublishingUser _daoPublishingUser;

        // Inyección de dependencias para DAO
        public PublishingUserController(IDAOPublishingUser daoPublishingUser)
        {
            _daoPublishingUser = daoPublishingUser ?? throw new ArgumentNullException(nameof(daoPublishingUser));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublishingUser>>> GetAll()
        {
            var publishingUsers = await _daoPublishingUser.GetAll();
            return Ok(publishingUsers);
        }

        // GET: api/PublishingUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PublishingUser>> GetById(long id)
        {
            var publishingUser = await _daoPublishingUser.GetById(id);

            if (publishingUser == null)
            {
                return NotFound();
            }

            return Ok(publishingUser);
        }

        // POST: api/PublishingUser
        [HttpPost]
        public async Task<ActionResult<PublishingUser>> Save(PublishingUser publishingUser)
        {
            await _daoPublishingUser.Save(publishingUser);

            // Si la publicación tiene ID, significa que se ha actualizado, de lo contrario, es nueva
            return CreatedAtAction(nameof(GetById), new { id = publishingUser.Id }, publishingUser);
        }

        // PUT: api/PublishingUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, PublishingUser publishingUser)
        {
            if (id != publishingUser.Id)
            {
                return BadRequest();
            }

            await _daoPublishingUser.Save(publishingUser);

            return NoContent();
        }

        
}
}
