using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinimalApi.Core.Entities;

public class UserEntity : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Username { get; set; }
    private string Password { get; set; }
    public ICollection<MediaEntity>? Medias { get; set; }
}

public class UserModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Username { get; set; }
}