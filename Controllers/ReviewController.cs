using System;
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
        
        var user = await myContext.Users.FirstOrDefaultAsync(u => u.UserName == review.UserName);
        var review1 = new Review{
            Description = review.Description,
            Rating = review.Rating,
            UserName = review.UserName,
            CreatedAt = review.CreatedAt,
            UserId = user.Id
        };





    }



}

    

