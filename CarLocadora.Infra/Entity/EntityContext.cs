using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Infra.Entity
{
    public class EntityContext : DbContext
    {

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }
    }
}
