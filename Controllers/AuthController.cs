using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.DTOS.User;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(new User { UserName = request.Username }, 
                                                               request.Password);
            
            
            return !response.Success ? BadRequest(response) : Ok(response);
        }
    }
}