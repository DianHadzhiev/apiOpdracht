using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class MovieController:ControllerBase
{
    public Context myDB;
    public MovieController(Context myDB) {
        this.myDB = myDB;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() {
        var movies = await myDB.Movies.Select(mov => new MovieDto(mov.Title, mov.Year, mov.Director.Name)).ToListAsync();

        if (movies == null) {
            return NotFound();
        } else return Ok(movies);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>>GetMovie(int Id) {
        var movie = await myDB.Movies.FindAsync(Id);

        if (movie == null) {
            return NotFound();
        }
        var movieDto = new MovieDto(movie.Title, movie.Year, movie.Director.Name);
        return movieDto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>PutMovie(int id, Movie movie) {
        if (id != movie.Id) return BadRequest();
        myDB.Entry(movie).State = EntityState.Modified;
        try {
            await myDB.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException) {
            if (!await MovieExists(id)) { return NotFound();
            } else throw;
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Movie>>PostMovie(MovieDto movie1) {
        
        var director = await myDB.Directors.FirstOrDefaultAsync(d => d.Name == movie1.DirectorName);

        var movie = new Movie{
            Title = movie1.Title,
            Year = movie1.Year,
            Director = director
        };
        
        
        myDB.Add(movie);
        await myDB.SaveChangesAsync();
        return CreatedAtAction("GetMovie", new{Id = movie.Id}, movie);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMovie(string movieName) {
        var movie = await myDB.Movies.FirstOrDefaultAsync(m => m.Title == movieName);

        if (movie == null) return NotFound();
        myDB.Movies.Remove(movie);
        await myDB.SaveChangesAsync();
        
        return NoContent();
    }

    private async Task<bool> MovieExists(int id)
{
    return await myDB.Movies.AnyAsync(m => m.Id == id);
}

}