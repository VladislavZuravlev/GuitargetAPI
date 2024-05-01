namespace Application.DTO;

public class EmployeeRoleDTO
{
    public int EmployeeId { get; set; }
    public byte RoleId { get; set; }
    public EmployeeDTO Employee { get; set; }
}