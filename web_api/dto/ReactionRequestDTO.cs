using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using entities_library.publishing.reactions;
using System.Collections.Generic;

    public class ReactionRequestDTO
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Type { get; set; } = "love"; // Solo se permite "love"
    }

