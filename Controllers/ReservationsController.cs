using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace CinemaApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReservationsController : ControllerBase
  {
    private CinemaDbContext _dbContext;
    public ReservationsController(CinemaDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    // api/reservations
    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] Reservation reservationObj)
    {
      reservationObj.ReservationTime = DateTime.Now;
      _dbContext.Reservations.Add(reservationObj);
      _dbContext.SaveChanges();
      return StatusCode(StatusCodes.Status201Created);
    }

  }
}