using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLand.backend.models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? password { get; set; }
    }
}
