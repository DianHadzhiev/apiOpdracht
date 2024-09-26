using System.ComponentModel.DataAnnotations;

public class ReviewDto {
    
    [Range(1,10)]
    public int Rating{get; set;}
    [StringLength(1000)]
    public string Description{get; set;}
    [StringLength(50)]
    public string UserName{get; set;}
    public DateTime CreatedAt{get; set;}
}