namespace Domain.Shared.Model.Entities;

public abstract class ModelBase
{
    public int Id { get; set; }
    public int CreatedContact { get; set; }
    public int? UpdatedContact { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;
}