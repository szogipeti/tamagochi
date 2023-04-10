using System;
using System.Collections.Generic;

namespace Tamagochi.Model
{
    public partial class Animal
    {
        public Animal()
        {
            AnimalStats = new HashSet<AnimalStat>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AnimalStat> AnimalStats { get; set; }
    }
}
