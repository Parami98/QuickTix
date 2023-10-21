/******************************
 * ADMIN Login Module
 * Author: Amaraweera O.G
 * Date:[10/10/2023]
 * 
 * Description: This module manages the login functionalities for administrative users. 
 * It ensures that only authorized administrators can access certain restricted 
 * areas or functionalities of the application.
 *
 ******************************/


using ead_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ead_backend.Controllers
{
    [Route("api/admin-login")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IMongoCollection<Admin> _adminCollection;
        private readonly ILogger<AdminLoginController> _logger;

        public AdminLoginController(ILogger<AdminLoginController> logger, IOptions<Database> database)
        {
            _logger = logger;
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _adminCollection = mongoDatabase.GetCollection<Admin>("Admin");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLogin request)
        {
            var admin = await _adminCollection.Find(x => x.Email == request.Email && x.Password == request.Password).FirstOrDefaultAsync();

            if (admin == null)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }

}
