using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review {
    [Key]
    public int id{get; set;}
    [Range(1,10)]
    public int Rating{get; set;}
    [StringLength(1000)]
    public string Description{get; set;}
    [StringLength(50)]
    public string UserName{get; set;}
    public DateTime CreatedAt{get; set;}
    
    [ForeignKey(nameof(movie.Id))]
    public int MovieId {get; set;}
    public Movie movie {get; set;}

    public User user {get; set;}

   [ForeignKey(nameof(User.Id))] 
   public string UserId{get;set;}

    
}