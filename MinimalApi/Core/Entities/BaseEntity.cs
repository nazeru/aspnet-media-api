namespace MinimalApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity
{
    [Required()]
    public int Id { get; set; }
    [Required()]
    public DateTime CreatedOn { get; set; }
    [Required()]
    public DateTime ModifiedOn { get; set; }

}