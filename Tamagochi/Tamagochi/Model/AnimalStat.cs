using System;
using System.Collections.Generic;

namespace Tamagochi.Model
{
    public partial class AnimalStat
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong AnimalId { get; set; }
        public string Name { get; set; } = null!;
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public int Happiness { get; set; }
        public int Activity { get; set; }
        public int Health { get; set; }
        public int Dexterity { get; set; }
        public DateTime LastHunger { get; set; }
        public DateTime LastThirst { get; set; }
        public DateTime LastHappiness { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime LastHealth { get; set; }
        public DateTime LastDexterity { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Animal Animal { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
