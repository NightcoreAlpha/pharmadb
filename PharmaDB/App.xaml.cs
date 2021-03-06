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
            public DbSet<Polyclinics> polyclinics { get; set; }
            public DbSet<AddToBrigade> add_to_brigade { get; set; }
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
                int id = 1;
                List<int> idPerson = new List<int>();
                var allPersons = from p in context.persons.ToList()
                                 select new
                                 {
                                     id = p.id
                                 };
                foreach (var person in allPersons)
                {
                    idPerson.Add(person.id);
                }
                if (idPerson.Count > 0)
                {
                    while (id < 1000)
                    {
                        if (idPerson.Contains(id))
                        {
                            id++;
                        }
                        else
                        {
                            return id;
                        }
                    }
                }
                else return 1;
                return 1;
            }
            public static int getAddBrigadeId(PharmaContext context)
            {
                int id = 1;
                List<int> idBrigade = new List<int>();
                var allBrigades = from p in context.add_to_brigade.ToList()
                                  select new
                                  {
                                      p.id
                                  };
                foreach (var brigade in allBrigades)
                {
                    idBrigade.Add(brigade.id);
                }
                if (idBrigade.Count > 0)
                {
                    while (id < 1000)
                    {
                        if (idBrigade.Contains(id))
                        {
                            id++;
                        }
                        else
                        {
                            return id;
                        }
                    }
                }
                else return 1;
                return 1;
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
        public class Polyclinics
        {
            public int id { get; set; }
            public string title { get; set; }
            public string short_title { get; set; }
            public string address { get; set; }
            public int? id_tel { get; set; }
        }
        public class AddToBrigade
        {
            public int id { get; set; }
            public int id_person { get; set; }
            public int id_brigade { get; set; }

        }
    }
}
