using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pz4
{
    public partial class MainWindow : Window
    {
        private List<User> users = new List<User>();
        private string currentFile = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            // Магические числа и строки
            users.Add(new User { Name = "Admin", Age = 30, Email = "admin@test.com", IsActive = true });
            users.Add(new User { Name = "User1", Age = 25, Email = "user1@test.com", IsActive = true });
            dataGridUsers.ItemsSource = users; 
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                currentFile = ofd.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(currentFile);
                    users.Clear();
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            User u = new User();
                            u.Name = parts[0];
                            u.Age = int.Parse(parts[1]);
                            u.Email = parts[2];
                            u.IsActive = bool.Parse(parts[3]);
                            users.Add(u);
                        }
                    }
                    dataGridUsers.ItemsSource = null; // Исправлено: dataGridUsers вместо dgUsers
                    dataGridUsers.ItemsSource = users; // Исправлено: dataGridUsers вместо dgUsers
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User { Name = "New User", Age = 0, Email = "email@test.com", IsActive = true };
            users.Add(newUser);
            RefreshGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem != null) // Исправлено: dataGridUsers вместо dgUsers
            {
                users.Remove((User)dataGridUsers.SelectedItem); // Исправлено: dataGridUsers вместо dgUsers
                RefreshGrid();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (sfd.ShowDialog() == true)
                {
                    currentFile = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                List<string> lines = new List<string>();
                foreach (User user in users)
                {
                    lines.Add($"{user.Name},{user.Age},{user.Email},{user.IsActive}");
                }
                File.WriteAllLines(currentFile, lines);
                MessageBox.Show("Saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save error: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            dataGridUsers.ItemsSource = null; // Исправлено: dataGridUsers вместо dgUsers
            dataGridUsers.ItemsSource = users; // Исправлено: dataGridUsers вместо dgUsers
        }
    }
}

