using Packages.DataAccess.Repository.Contracts;

namespace CustomerInventoryService.Domain.Models;

public class Product 
    : ISoftDelete, IDateTrackable
{
    public int Id { get; set; } 

    public required string Name { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get ; set; }
}
