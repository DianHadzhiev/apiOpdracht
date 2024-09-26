using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

public class Movie {
    [Key]
    public int Id{get; set;}
    [StringLength(100)]
    public string? Title{get; set;}
    [Range(1000,9999)]
    public int Year{get; set;}

    public List<Review>? Reviews = new List<Review>();
    [ForeignKey(nameof(Director))]
    public int DirectorId {get;set;}
    public Director Director{get; set;}

    private Movie(){}
    public Movie(string title, int year, Director Director){
        this.Title = title;
        this.Year = year;
        this.Director = Director;
    }

}