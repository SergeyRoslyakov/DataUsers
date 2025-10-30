using System;

namespace pz4
{
  
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }

            // Плохой метод с дублированием логики
            public string GetInfo()
            {
                string status = IsActive ? "Active" : "Inactive";
                return $"{Name} ({Age}) - {Email} - {status}";
            }

            // Еще один метод с похожей логикой
            public string GetShortInfo()
            {
                string status = IsActive ? "A" : "I";
                return $"{Name} - {status}";
            }

            // Валидация размазана по разным местам
            public bool IsValid()
            {
                if (string.IsNullOrEmpty(Name)) return false;
                if (Age < 0 || Age > 150) return false;
                if (string.IsNullOrEmpty(Email) || !Email.Contains("@")) return false;
                return true;
            }
        }
    }
