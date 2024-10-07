using System.ComponentModel.DataAnnotations;

public class MovieDto {
    [StringLength(100)]
    public string Title{get; set;}
    [Range(1000,9999)]
    public int Year{get; set;}
    public List<Review>? Reviews = new List<Review>();
    public string DirectorName {get;set;}
    public MovieDto(string Title, int year, string DirectorName){
        this.Title = Title;
        this.Year = year;
        this.DirectorName = DirectorName;
    }
}