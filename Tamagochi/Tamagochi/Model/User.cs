using System;
using System.Collections.Generic;

namespace Tamagochi.Model
{
    public partial class User
    {
        public User()
        {
            AnimalStats = new HashSet<AnimalStat>();
        }

        public ulong Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? EmailVerifiedAt { get; set; }
        public string Password { get; set; } = null!;
        public string? RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AnimalStat> AnimalStats { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
