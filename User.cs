using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz4
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public string GetInfo()
        {
            string status = IsActive ? "Active" : "Inactive";
            return $"{Name} ({Age}) - {Email} - {status}";
        }

        public string GetShortInfo()
        {
            string status = IsActive ? "A" : "I";
            return $"{Name} - {status}";
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            if (Age < 0 || Age > 150) return false;
            if (string.IsNullOrEmpty(Email) || !Email.Contains("@")) return false;
            return true;
        }
    }
}
