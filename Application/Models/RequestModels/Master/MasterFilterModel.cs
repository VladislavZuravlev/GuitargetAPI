namespace Application.Models.RequestModels.Master;

public class MasterFilterModel
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
}