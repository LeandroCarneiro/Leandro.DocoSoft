using System.Threading;
using System.Threading.Tasks;
using SertaoArch.UserMi.Application.Domain;
using SertaoArch.UserMi.Contracts.AppObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace SertaoArch.UserMi.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserApp _app;

        public UserController(UserApp appService)
        {
            _app = appService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username, CancellationToken cancellation)
        {
            var result = await _app.FindAsync(username, cancellation);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserContract user, CancellationToken cancellation)
        {
            var createdUser = await _app.Create(user, cancellation);
            return CreatedAtAction(nameof(Get), user, createdUser);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username, [FromBody] UserContract user, CancellationToken cancellation)
        {
            await _app.Update(username, user, cancellation);
            return Ok();
        }
    }
}
