using System;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser, IDisposable {

    public string? favoriteMovieCharacter;

    public ICollection<Review>? reviews {get; set;}

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}