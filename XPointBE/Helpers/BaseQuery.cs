using System.ComponentModel.DataAnnotations;

namespace XPointBE.Helpers;

public abstract class BaseQuery
{
    public int Page { get; set; } = 1;
    
    [Range(1, 20, ErrorMessage = "Page size must be between 1 and 100.")]
    public int PageSize { get; set; } = 20;
}