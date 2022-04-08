using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для addPersonControl.xaml
    /// </summary>
    public partial class addPersonControl : UserControl
    {
        List<App.Specialization> specializations = new List<App.Specialization>();
        public addPersonControl()
        {
            InitializeComponent();
            genderhBox.Items.Add("Муж");
            genderhBox.Items.Add("Жен");

            using (var db = new App.PharmaContext())
            {
                var specials = db.specializations.ToList();
                //specialBox.Items.Add(specials);

                foreach (var special in specials)
                {
                    //specialBox.Items.Add(special.title);
                    /*specialBox.Items.Add(new App.Specialization()
                    {
                        id = special.id,
                        title = special.title,
                        description = special.description
                    }.title);*/
                    specializations.Add(
                        new App.Specialization()
                        {
                            id = special.id,
                            title = special.title,
                            description = special.description
                        }
                        );
                }
                specialBox.ItemsSource = specializations;
                specialBox.DisplayMemberPath = "title";
                //specialBox.SelectedValue = "id";
            }
        }

        List<App.Specialization> specload()
        {
            using (var db = new App.PharmaContext())
            {
                var specials = db.specializations.ToList();
                //specialBox.Items.Add(specials);

                foreach (var special in specials)
                {
                    //specialBox.Items.Add(special.title);
                    specialBox.Items.Add(new App.Specialization()
                    {
                        id = special.id,
                        title = special.title,
                        description = special.description
                    }.title);
                }
                var mess = "";

            }
            return specializations;
        }

        private void expBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            personGrid.Children.Clear();
            personGrid.Children.Add(new UserControl2_persons());
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new App.PharmaContext())
            {
                //var sp = new GenerateId();

                int ids = GenerateId.getPersonId(db);
                //var spec = db.specializations.Where(x => x.title == specialBox.SelectedItem.ToString()).FirstOrDefault();
                var spec = specialBox.SelectedItem as App.Specialization;
                /*DateTime? dateEnd = null;
                if (dateendBox.SelectedDate.HasValue == true)
                {
                    dateEnd = dateendBox.SelectedDate.Value;
                }
                else
                {
                    dateEnd = null;
                }*/
                var newPerson = new App.Person
                {
                    id = ids,
                    name = nameBox.Text,
                    familia = famBox.Text,
                    otchestvo = otchBox.Text,
                    gender = genderhBox.Text,
                    specialization = spec.id,
                    data_in = dateinBox.SelectedDate.Value,
                    data_end = (dateendBox.SelectedDate.HasValue == true) ? dateendBox.SelectedDate.Value : dateendBox.SelectedDate.GetValueOrDefault(),
                    experience = int.Parse(expBox.Text),
                    telefon = expBox.Text,
                    salary = double.Parse(salaryBox.Text),
                    tab_number = int.Parse(numbBox.Text)
                };
                db.persons.Add(newPerson);
                db.SaveChanges();
                personGrid.Children.Clear();
                personGrid.Children.Add(new UserControl2_persons());

            }
        }

        private void salaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}