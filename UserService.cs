using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace pz4
{
    public class UserService
    {
        private List<User> allUsers = new List<User>();

        private static List<User> searchCache = new List<User>();

        public void ProcessUsers(string filePath, bool backup, bool validate, bool sort)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath); 
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        User u = new User();
                        u.Name = parts[0];
                        u.Age = int.Parse(parts[1]);
                        u.Email = parts[2];
                        u.IsActive = bool.Parse(parts[3]);

                        if (validate)
                        {
                            if (u.IsValid())
                            {
                                allUsers.Add(u);
                            }
                        }
                        else
                        {
                            allUsers.Add(u);
                        }
                    }
                }

                if (sort)
                {
                    allUsers = allUsers.OrderBy(u => u.Name).ToList();
                }

                if (backup)
                {
                    string backupPath = Path.Combine(Path.GetDirectoryName(filePath),
                                                    "backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
                    File.WriteAllLines(backupPath, lines);
                }
            }
        }

        public List<User> FindUsers(string search, bool byName, bool byEmail, bool activeOnly)
        {
            List<User> result = new List<User>();

            foreach (User user in allUsers)
            {
                bool matches = false;

                if (byName && user.Name.Contains(search))
                {
                    matches = true;
                }

                if (byEmail && user.Email.Contains(search))
                {
                    matches = true;
                }

                if (activeOnly && !user.IsActive)
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(user);
                }
            }
            searchCache.AddRange(result); 

            return result;
        }

        public void ExportUsers(string filePath, bool activeOnly)
        {
            List<string> lines = new List<string>();
            foreach (User user in allUsers)
            {
                if (!activeOnly || user.IsActive)
                {
                    lines.Add($"{user.Name},{user.Age},{user.Email},{user.IsActive}");
                }
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
