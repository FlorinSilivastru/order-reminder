namespace Packages.DataAccess.Repository.Contracts;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}
