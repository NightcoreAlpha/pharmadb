using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PharmaDB.App;
using static PharmaDB.App.GenerateId;

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для AddBrigade.xaml
    /// </summary>
    public partial class AddBrigade : UserControl
    {
        static string title { get; set; }
        static int idSquad { get; set; }
        static List<Person> personL { get; set; }
        public AddBrigade()
        {
            InitializeComponent();
            setPersonToBrigade(idSquad, title, personL);
            nameLabel.Content = title;
            //SquadPersonList.ItemsSource =
            setPersonList();
            setSquadList();

        }
        static public void setPersonToBrigade(int idBrigade, string titleBrigade, List<Person> personList)
        {
            if (idBrigade != 0 && titleBrigade != "")
            {
                title = titleBrigade.ToString();
                idSquad = idBrigade;
                personL = personList;
            }
        }
        void setPersonList()
        {
            List<Person> personList = new List<Person>();
            using (var db = new PharmaContext())
            {
                var person = db.persons.ToList().Where(x => !personL.Any(y => y.id == x.id)).ToList();
                SquadPersonList.ItemsSource = person;
            }
        }
        void setSquadList()
        {
            List<Person> personList = new List<Person>();
            using (var db = new PharmaContext())
            {
                var person = db.persons.ToList().Where(x => personL.Any(y => y.id == x.id)).ToList();
                SquadList.ItemsSource = person;
            }
        }
        void setBrigadePerson(int id)
        {
            using (var db = new PharmaContext())
            {
                var person = SquadPersonList.SelectedItem as Person;
                if (person != null)
                {
                    int generatedId = getAddBrigadeId(db);
                    AddToBrigade addToBrigade = new AddToBrigade
                    {
                        id = generatedId,
                        id_brigade = id,
                        id_person = person.id
                    };
                    personL.Add(person);
                    db.add_to_brigade.Add(addToBrigade);
                    db.SaveChanges();
                }
            }
            //setPersonList(personL);
            //setSquadList(personL);
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            brigadeList.Children.Clear();
            brigadeList.Children.Add(new UserControlLists());
        }

        private void addBrigadeButton_Click(object sender, RoutedEventArgs e)
        {
            setBrigadePerson(idSquad);
            setPersonList();
            setSquadList();
            //brigadeList.Children.Clear();
            //brigadeList.Children.Add(new UserControlLists());
        }

    }
}
