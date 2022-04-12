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
using static PharmaDB.DataSourseClass;
using static PharmaDB.AddBrigade;

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для UserControlLists.xaml
    /// </summary>
    public partial class UserControlLists : UserControl
    {
        public UserControlLists()
        {
            DataContext = new ComboboxData();
            InitializeComponent();
            polBox.DisplayMemberPath = "short_title";
            UploadSquadList();
            setFieldData();

        }
        void UploadSquadList()
        {
            try
            {
                using (var dbSquad = new PharmaContext())
                {
                    var squad = (dbSquad.med_squad != null) ? dbSquad.med_squad : null;
                    if (squad != null)
                        squadGrid.ItemsSource = squad.ToList();
                }
            }
            catch (Exception exp) { MessageBox.Show("Описание ошибки: " + exp.Message, "Ошибка"); }
        }
        void setFieldData()
        {
            var data = ((MedSquad)squadGrid.SelectedItem != null) ? (MedSquad)squadGrid.SelectedItem : null;
            if (data == null)
            {
                squadGrid.SelectedIndex = 0;
                data = (App.MedSquad)squadGrid.SelectedItem;
            }
            if (data != null)
            {
                string polyclinic = "";
                var pol = (polBox.Items.Count > 0) ? polBox.Items : null;
                if (pol != null)
                {
                    foreach (Polyclinics str in pol)
                    {
                        if (str.id == data.id_pol)
                        {
                            polyclinic = str.short_title;
                        }
                    }
                    titleBox.Text = data.title.ToString();
                    commentBox.Document.Blocks.Clear();
                    commentBox.Document.Blocks.Add(new Paragraph(new Run(data.comment.ToString())));
                    polBox.Text = polyclinic;
                }
            }
        }
        void getSquadPerson(int idSquad)
        {
            using (var db = new PharmaContext())
            {
                if (idSquad != 0)
                {
                    var personalSquad = from s in db.add_to_brigade.Where(x => x.id_brigade == idSquad)
                                        join p in db.persons on s.id_person equals p.id
                                        select p;
                    SquadPersonList.ItemsSource = personalSquad.ToList();
                }
            }
        }
        private void squadGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Squad = squadGrid.SelectedItem as MedSquad;
            if (Squad != null)
            {
                var idSquad = Squad.id;
                getSquadPerson(idSquad);
            }
            setFieldData();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            if (squadGrid.SelectedItem != null)
            {
                var brigade = squadGrid.SelectedItem as MedSquad;
                int idBrigade = (brigade != null) ? brigade.id : 0;
                string titleBrigade = (brigade != null) ? brigade.title : "";
                List<Person> personList = new List<Person>();
                if (SquadPersonList.Items.Count > 0)
                {
                    foreach (Person person in SquadPersonList.Items)
                    {
                        personList.Add(person);
                    }
                }
                setPersonToBrigade(idBrigade, titleBrigade, personList);
            }
            brigadeList.Children.Clear();
            brigadeList.Children.Add(new AddBrigade());
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SquadPersonList.SelectedIndex != -1)
            {
                int idPerson = 0;
                MessageBoxResult ds = MessageBox.Show("Удалить выбранный объект?", "Подтверждение удаления", MessageBoxButton.YesNo);
                switch (ds)
                {
                    case MessageBoxResult.Yes:
                        using (var db = new App.PharmaContext())
                        {
                            idPerson = (int)SquadPersonList.SelectedItem.GetType().GetProperty("id").GetValue(SquadPersonList.SelectedItem);
                            var deletePerson = db.add_to_brigade.Where(x => x.id_person == idPerson).FirstOrDefault();
                            db.add_to_brigade.Remove(deletePerson);
                            db.SaveChanges();
                        }
                        var Squad = squadGrid.SelectedItem as MedSquad;
                        if (Squad != null)
                        {
                            var idSquad = Squad.id;
                            getSquadPerson(idSquad);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}