using System;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser {

    public string? favoriteMovieCharacter;

    public ICollection<Review>? reviews {get; set;}



}