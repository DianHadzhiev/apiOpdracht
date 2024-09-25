
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MovieController:ControllerBase
{
    public MovieContext myDB;
    public MovieController(MovieContext myDB) {
        this.myDB = myDB;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies() {
        return await myDB.Movies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>>GetMovie(int Id) {
        var movie = await myDB.Movies.FindAsync(Id);

        if (movie == null) {
            return NotFound();
        }

        return movie;
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
    public async Task<ActionResult<Movie>>PostMovie(Movie movie) {
        myDB.Add(movie);
        await myDB.SaveChangesAsync();
        return CreatedAtAction("GetMovie", new {Id = movie.Id}, movie);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMovie(int Id) {
        var movie = await myDB.Movies.FindAsync(Id);
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