using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password); // service Response 
        // this is a task that is a service response reutrns an int and registers the user and return id. 
        Task<ServiceResponse<string>> Login(string username, string password); // this is a login will loog the user. 
        Task<bool> UserExists(string username); //
    }
}