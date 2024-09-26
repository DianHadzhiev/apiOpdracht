using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

public class MovieDto {
    [StringLength(100)]
    public string? Title{get; set;}
    [Range(1000,9999)]
    public int Year{get; set;}
    public List<Review>? Reviews = new List<Review>();
    public string DirectorName {get;set;}
    public MovieDto(string title, int year, string DirectorName){
        this.Title = title;
        this.Year = year;
        this.DirectorName = DirectorName;
    }
}