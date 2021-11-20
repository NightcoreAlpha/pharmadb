using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public class PharmaContext : DbContext
        {
            public DbSet<Person> persons { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
            {
                var Username = "postgres";
                var Userpassword = "postgres";
                try
                {
                    optionsbuilder.UseNpgsql("Server=localhost;Port=5432;Username=" + Username + ";Password=" + Userpassword + ";Database=pharmadb;");
                }
                catch (Exception exp) { MessageBox.Show("Ошибка подключения: " + exp.Message, "Ошибка"); }
            }
        }
        public class Person
        {
            public int id { get; set; }
            public string familia { get; set; }
            public string name { get; set; }
            public string otchestvo { get; set; }
            public string gender { get; set; }
            public int specialization { get; set; }
            public DateTime data_in { get; set; }
            public DateTime? data_end { get; set; }
            public int? experience { get; set; }
            public string telefon { get; set; }
            public double salary { get; set; }
            public int? tab_number { get; set; }
        }
    }
}
