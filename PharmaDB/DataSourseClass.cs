using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PharmaDB.App;

namespace PharmaDB
{
    internal class DataSourseClass
    {
        public class ComboboxData
        {
            public string[] genderList { get; set; }
            public bool isReadField { get; set; }
            public bool isEnabledField { get; set; }
            public List<Specialization> specializationList { get; set; }
            public List<Polyclinics> medSquad { get; set; }
            public ComboboxData()
            {
                genderList = new string[] { "Муж", "Жен" };
                isReadField = true;
                isEnabledField = false;
                using (var db = new App.PharmaContext())
                {
                    specializationList = db.specializations.ToList();
                    medSquad = db.polyclinics.ToList();
                }
            }
        }
    }
}