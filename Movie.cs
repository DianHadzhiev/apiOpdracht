using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

public class Movie {
    [Key]
    public int Id{get; set;}
    [StringLength(100)]
    public string? Title{get; set;}
    [Range(1000,9999)]
    public int Year{get; set;}

    public List<Review>? Reviews = new List<Review>();
    public Director Director{get; set;}

}