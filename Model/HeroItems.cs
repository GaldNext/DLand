using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLand.Model
{
    public class HeroItems
    {
        public int Id { get; set; }
        public int? Character_Id { get; set; }
        public decimal? ItemWeight { get; set; }
        public List<string>? Item { get; set; }
    }
}
