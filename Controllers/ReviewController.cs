using System;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route ("api/[controller]")]
[ApiController]

public class ReviewController :ControllerBase {

    public Context myContext;

    public ReviewController(Context context) {
        this.myContext = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> getReviews() {
       var reviews = await myContext.Reviews.Select(rev => new ReviewDto(rev.Rating, rev.Description, rev.UserName, rev.CreatedAt)).ToListAsync();

       if (reviews == null) {
            return NotFound();
        } else return Ok(reviews);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> getReview(int Id) {
        var review = await myContext.Reviews.FindAsync(Id);

        if (review == null) {
            return NotFound();
        } else return new ReviewDto(review.Rating, review.Description, review.UserName, review.CreatedAt);

    }

    [HttpPost]
    public async Task<ActionResult<Review>>PostReview(ReviewDto review) {
        
        using (var user = await myContext.Users.FirstOrDefaultAsync(u => u.UserName == review.UserName)) {

            if (user == null ) {
                return BadRequest("User not Found.");
            }else {

                var review1 = new Review{
                    Description = review.Description,
                    Rating = review.Rating,
                    UserName = review.UserName,
                    CreatedAt = review.CreatedAt,
                    UserId = user.Id
                };


                myContext.Reviews.Add(review1);

                await myContext.SaveChangesAsync();

                return CreatedAtAction(nameof(getReview), new { id = review1.UserId}, review1);
            }

        }
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> PutReview (ReviewDto reviewDto) {
        var review = await myContext.Reviews.Include(r => r.user).FirstOrDefaultAsync(u => u.UserName == reviewDto.UserName);

        if (review == null ) {
            return NotFound();
        }

        var user = myContext.Users.FirstOrDefault(u => u.UserName == reviewDto.UserName);

        if (user == null) {
            return NotFound("User not found");
        }

        review.Rating = reviewDto.Rating;
        review.Description = reviewDto.Description;
        review.CreatedAt = DateTime.Now;
        review.UserId = user.Id;

        myContext.Entry(review).State = EntityState.Modified;

        try
        {
            await myContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!ReviewExists(review.id)){
                return NotFound();
            } else {
                throw;
            }
            
        }

        return NoContent();

    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteReview(int id) {
        var review = await myContext.Reviews.FirstOrDefaultAsync(r => r.id == id);

        if (review == null) return NotFound();
        
        try {
            myContext.Reviews.Remove(review);
            await myContext.SaveChangesAsync();
        }catch (DBConcurrencyException){
            throw;
        }

        return NoContent();
    }

    private bool ReviewExists(int id)
    {
        return myContext.Reviews.Any(e => e.id == id);
    }

}
