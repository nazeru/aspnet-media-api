namespace MinimalApi.Core.Entities;

public interface ISoftDeleteEntity
{
    bool Deleted { get; set; }
}