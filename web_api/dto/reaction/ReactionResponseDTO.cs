using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using System.Collections.Generic;
namespace entities_library.publishing.reactions;

    public class ReactionResponseDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string? Type { get; set; }
    }


