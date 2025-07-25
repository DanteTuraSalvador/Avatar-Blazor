using Microsoft.EntityFrameworkCore;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure.Data;

namespace Avatar.Infrastructure.Repositories;

public class AvatarRepository : Repository<Core.Entities.Avatar>, IAvatarRepository
{
    public AvatarRepository(AvatarDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Core.Entities.Avatar>> GetByCategory(string category)
    {
        return await _dbSet
            .Where(a => a.Category == category)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Core.Entities.Avatar>> GetActiveAvatars()
    {
        return await _dbSet
            .Where(a => a.IsActive)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Core.Entities.Avatar>> SearchByName(string name)
    {
        return await _dbSet
            .Where(a => a.Name.Contains(name))
            .OrderBy(a => a.Name)
            .ToListAsync();
    }
}
