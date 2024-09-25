using System.ComponentModel.DataAnnotations;

public class Director {
  
  [Key]
  public int Id {get; set;}

  [StringLength(50)]
  public string? Name{get; set;}

  public List<Movie> Movies = new List<Movie>();

}
