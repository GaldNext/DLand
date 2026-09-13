using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLand.Model
{
    public class HeroAbilities
    {
        public int Id { get; set; }
        public int? Character_Id { get; set; }
        public int? TypeAbilitie { get; set; }
        public List<string>? PersonAbilitie { get; set; }

    }
}
