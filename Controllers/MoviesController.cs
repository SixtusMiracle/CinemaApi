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
  public class MoviesController : ControllerBase
  {
    private CinemaDbContext _dbContext;
    public MoviesController(CinemaDbContext dbContext) {
      _dbContext = dbContext;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post([FromForm] Movie movieObj)
    {
      var guid = Guid.NewGuid();
      var filePath = Path.Combine("wwwroot", guid + ".jpg");
      if (movieObj.Image != null)
      {
        var fileStream = new FileStream(filePath, FileMode.Create);
        movieObj.Image.CopyTo(fileStream);
      }
      movieObj.ImageUrl = filePath.Remove(0, 7);
      _dbContext.Movies.Add(movieObj);
      _dbContext.SaveChanges();

      return StatusCode(StatusCodes.Status201Created);
    }

  }
}