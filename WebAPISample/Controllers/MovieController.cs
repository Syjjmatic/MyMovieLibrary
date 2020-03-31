using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPISample.Data;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private ApplicationContext _context;
        public MovieController(ApplicationContext context)
        {
            _context = context;
        }
        
        // GET api/movie
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Movies);
        }

        // GET api/movie/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST api/movie
        [HttpPost]
        public IActionResult Post([FromBody]Movie movie)
        {
            // Create movie in db logic
            _context.Movies.Add(movie);
            _context.SaveChangesAsync();
            return Ok(movie);
        }

        // PUT api/movie
        [HttpPut]
        public IActionResult Put([FromBody] Movie movie)
        {
            // Update movie in db logic
            var updatedMovie = _context.Movies.Find(movie.MovieId);

            if (updatedMovie.MovieId != movie.MovieId)
            {
                return BadRequest();
            }

            updatedMovie.Title = movie.Title;
            updatedMovie.Genre = movie.Genre;
            updatedMovie.Director = movie.Director;
            _context.SaveChanges();
            return Ok(updatedMovie);
        }

        // DELETE api/movie/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Delete movie from db logic            
            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}