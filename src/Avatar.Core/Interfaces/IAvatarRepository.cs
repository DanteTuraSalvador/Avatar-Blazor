using Avatar.Core.Entities;

namespace Avatar.Core.Interfaces;

public interface IAvatarRepository : IRepository<Entities.Avatar>
{
    Task<IEnumerable<Entities.Avatar>> GetByCategory(string category);
    Task<IEnumerable<Entities.Avatar>> GetActiveAvatars();
    Task<IEnumerable<Entities.Avatar>> SearchByName(string name);
}
