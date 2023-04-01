using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
