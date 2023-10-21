/******************************
 * ReservationController
 * Author: Jayasekara J.M.P.N.K
 * Date: [10/10/2023]
 * 
 * Description: This module oversees the reservation functionalities for travelers. 
 * It allows the creation, modification, and cancellation of reservations. 
 * A summary page is presented to the traveler before confirming any modifications.
 *
 * Features:
 * 1. Reservation Creation: Travelers can initiate and create new reservations.
 * 2. Reservation Modification: Allows travelers to edit existing reservations.
 * 3. Reservation Cancellation: Provides an option to cancel a reservation.
 * 4. Summary Page: Displays a summary of changes for review before confirmation.
 *
 ******************************/




using ead_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ead_backend.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger, IOptions<Database> database)
        {
            _logger = logger;
            var mongoClient = new MongoClient(database.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(database.Value.DatabaseName);
            _reservationCollection = mongoDatabase.GetCollection<Reservation>("Reservation");
        }

        [Route("add-reservation")]
        [HttpPost]
        public async Task<IActionResult> CreateReservation(Reservation reservation)
        {
            await _reservationCollection.InsertOneAsync(reservation);
            return CreatedAtAction(nameof(CreateReservation), new { id = reservation.Id }, reservation);
        }

        [Route("get-all-reservations")]
        [HttpGet]
        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _reservationCollection.Find(_ => true).ToListAsync();
        }

        [Route("get-reservation/{id?}")]
        [HttpGet]
        public async Task<Reservation?> GetReservationById(string id)
        {
            return await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        [Route("get-reservation-by-user/{userId}")]
        [HttpGet]
        public async Task<List<Reservation>> GetReservationByUser(string userId)
        {
            return await _reservationCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        [Route("update-reservation/{id?}")]
        [HttpPut]
        public async Task<IActionResult> UpdateReservation(string id, Reservation updatedReservation)
        {
            var reservation = await GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            updatedReservation.Id = reservation.Id;
            await _reservationCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation);

            return NoContent();
        }

        [Route("delete-reservation/{id?}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            var reservation = await GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationCollection.DeleteOneAsync(x => x.Id == id);
            return NoContent();
        }
    }

}
