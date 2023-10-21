/******************************
 * User Management Controller
 * Author: UdanaIsiwari
 * Date: [10/10/2023]
 * 
 * Description: This module manages the details and operations related to trains. 
 * It encompasses functionalities for creating new train schedules, updating them, 
 * and canceling trains provided they have no existing reservations.
 *
 * Features:
 * 1. Train Creation: Enables creation of new trains with schedules. Trains 
 *    can be reserved only once they are active and published.
 * 2. Train Schedule Update: Provides the capability to modify existing 
 *    train schedules.
 * 3. Train Cancellation: Allows trains to be canceled for reservations. 
 *    Trains with existing reservations cannot be canceled.
 *
 ******************************/


using ead_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ead_backend.Controllers
{
    [Route("api/train")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly IMongoCollection<Train> _trainCollection;
        private readonly ILogger<TrainController> _logger;

        public TrainController(ILogger<TrainController> logger, IOptions<Database> database)
        {
            _logger = logger;
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _trainCollection = mongoDatabase.GetCollection<Train>("Train");
        }

        [Route("add-train")]
        [HttpPost]

        public async Task<IActionResult> CreateTrain(Train train)
        {
            await _trainCollection.InsertOneAsync(train);
            return CreatedAtAction(nameof(CreateTrain), new { id = train.Id }, train);
        }

        [Route("get-all-trains")]
        [HttpGet]

        public async Task<List<Train>> GetAllTrains()
        {
            return await _trainCollection.Find(_ => true).ToListAsync();
        }

        [Route("get-train/{id?}")]
        [HttpGet]

        public async Task<Train?> GetTrainById(string id)
        {
            return await _trainCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        [Route("update-train/{id?}")]
        [HttpPut]

        public async Task<IActionResult> UpdateTrain(string id, Train updateTrain)
        {
            var train = await GetTrainById(id);
            if (train == null)
            {
                return NotFound();
            }

            updateTrain.Id = train.Id;
            await _trainCollection.ReplaceOneAsync(x => x.Id == id, updateTrain);

            return NoContent();
        }

        [Route("delete-train/{id?}")]
        [HttpDelete]

        public async Task<IActionResult> DeleteTrain(string id)
        {
            var train = await GetTrainById(id);
            if (train == null)
            {
                return NotFound();
            }

            await _trainCollection.DeleteOneAsync(x => x.Id == id);
            return NoContent();
        }
    }
}
