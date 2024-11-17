using System.ComponentModel.DataAnnotations;

namespace Presentation.FinanceGuard.Resources;

public record CreateContactResource(
    [Required]
    [MinLength(3)]
    [MaxLength(25)]
    string Name,
    [Required]
    [MinLength(5)]
    [MaxLength(35)]
    string Email);