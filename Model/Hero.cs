using DLand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DLand.backend.models
{
    public class Hero
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string? Name { get; set; }
        public string? Race { get; set; }
        public string? Class { get; set; }
        public string? Profession { get; set; }
        public int? OP { get; set; }
        public Properties? properties { get; set; }
        public List<HeroAbilities>? abilities { get; set; }
        public List<HeroItems>? items { get; set; }
        public byte[]? Image { get; set; }
    }
}
