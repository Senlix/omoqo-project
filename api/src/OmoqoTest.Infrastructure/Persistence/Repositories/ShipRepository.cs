using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Infrastructure.Persistence.Repositories
{
    public class ShipRepository(OmoqoTestDbContext dbContext) : Repository<Ship>(dbContext), IShipRepository
    { }
}