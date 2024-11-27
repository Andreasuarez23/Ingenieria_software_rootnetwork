using entities_library.publishing.reactions;
using System;

namespace dao_library.Interfaces.reaction;

public interface IDAOReaction
{
    Task<Reaction> AddReaction(Reaction reaction);
    Task<Reaction> GetById(long id);
}

