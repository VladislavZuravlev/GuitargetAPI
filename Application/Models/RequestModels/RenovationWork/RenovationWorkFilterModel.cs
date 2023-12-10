namespace Application.Models.RequestModels.RenovationWork;

public class RenovationWorkFilterModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}