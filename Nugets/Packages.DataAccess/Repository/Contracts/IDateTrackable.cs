namespace Packages.DataAccess.Repository.Contracts;

public interface IDateTrackable
{
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
