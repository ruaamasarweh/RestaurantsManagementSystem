using System.ComponentModel.DataAnnotations;

public class DishDto
{
    [Key]
    public int DishID { get; set; }
    public string? DishName { get; set; }
    public string? DishImageUrl { get; set; }
    public double Price { get; set; }
    public string? Details { get; set; }
}

