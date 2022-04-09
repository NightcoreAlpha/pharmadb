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
            public DbSet<Account> accounts { get; set; }
            public DbSet<Person> persons { get; set; }
            public DbSet<Specialization> specializations { get; set; }
            public DbSet<MedSquad> med_squad { get; set; }
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
        public static class GenerateId
        {
            public static int getPersonId(PharmaContext context)
            {
                int id = 0;
                var allPersons = from p in context.persons.ToList()
                                 select new
                                 {
                                     id = p.id
                                 };
                var idPersons = allPersons.ToList();
                if (idPersons.Count > 0)
                {
                    for (int i = 0; i < idPersons.Count - 1; i++)
                    {
                        id = idPersons[i].id;
                        if (idPersons[i + 1].id != id + 1)
                        {
                            return id + 1;
                        }
                    }
                    return id += 2;
                }
                else
                {
                    return 1;
                }
            }
        }
        public class Account
        {
            public int? id { get; set; }
            public string name_account { get; set; }
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
        public class Specialization
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
        public class MedSquad
        {
            public int id { get; set; }
            public string title { get; set; }
            public string comment { get; set; }
            public int id_pol { get; set; }
        }
    }
}
