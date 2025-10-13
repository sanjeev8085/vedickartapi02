using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VadicKart.Entity.Presentation.Dto.ContactMessage
{
    public record ContactMassagecsCreationDto
    {
       // public int Id { get; init; }
        public required string? Name { get; init; }
        public required string? Email { get; init; }
        public required string? Subject { get; init; }
        public required string? Message { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
