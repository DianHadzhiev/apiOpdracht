using System.ComponentModel.DataAnnotations;

public class DirectorDto {
  [StringLength(50)]
  public string? Name{get; set;}

  public List<Movie> Movies = new List<Movie>();

}
