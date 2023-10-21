/******************************
 * Traveler Login Module
 * Author: Jayasekara J.M.P.N.K
 * Date: [10/10/2023]
 * 
 * Description: This module oversees the login functionalities tailored for travelers. 
 * It facilitates the secure access of traveler profiles and ensures data privacy.
 *
 ******************************/


using ead_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ead_backend.Controllers
{
    [Route("api/user-login")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly ILogger<UserLoginController> _logger;

        public UserLoginController(ILogger<UserLoginController> logger, IOptions<Database> database)
        {
            _logger = logger;
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<User>("User");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin request)
        {
            var user = await _userCollection.Find(x => x.NIC == request.NIC && x.Password == request.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(); 
        }
    }

}
