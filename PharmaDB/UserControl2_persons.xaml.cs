using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
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
using static PharmaDB.DataSourseClass;
using static PharmaDB.App;

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для UserControl2_persons.xaml
    /// </summary>
    public partial class UserControl2_persons : UserControl
    {
        App.PharmaContext db = new App.PharmaContext();
        public List<App.Person> persons;
        public static int id = 0, idPerson = 0;
        public UserControl2_persons()
        {
            DataContext = new ComboboxData();
            InitializeComponent();
            specialBox.DisplayMemberPath = "title";
            UploadList();
            setFieldData();
        }

        void UploadList()
        {
            try
            {
                using (var dbpers = new PharmaContext())
                {
                    var persons = (dbpers.persons != null) ? dbpers.persons : null;
                    if (persons != null)
                        usergrid1.ItemsSource = persons.ToList();
                }
            }
            catch (Exception exp) { MessageBox.Show("Описание ошибки: " + exp.Message, "Ошибка"); }
        }
        private void addPersonalButton_Click(object sender, RoutedEventArgs e)
        {
            personList.Children.Clear();
            personList.Children.Add(new addPersonControl());
        }

        private void expBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void salaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void usergrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (uploadPersonalButton.Content.ToString() == "Отмена")
            {
                uploadPersonalButton.Content = "Изменить";
                editPersonalButton.Visibility = Visibility.Hidden;
            }

            setEditingFieldDisable();
            setFieldData();
        }
        void setFieldData()
        {
            string specialization = "";
            var data = ((App.Person)usergrid1.SelectedItem != null) ? (App.Person)usergrid1.SelectedItem : null;
            if (data == null)
            {
                usergrid1.SelectedIndex = 0;
                data = (App.Person)usergrid1.SelectedItem;
            }
            if (data != null)
            {
                var spec = specialBox.Items;
                foreach (Specialization str in spec)
                {
                    if (str.id == data.specialization)
                    {
                        specialization = str.title;
                    }
                }
                nameBox.Text = data.name.ToString();
                famBox.Text = data.familia.ToString();
                otchBox.Text = data.otchestvo.ToString();
                genderBox.Text = data.gender.ToString();
                specialBox.Text = specialization;
                dateinBox.Text = data.data_in.ToString();
                dateendBox.Text = data.data_end.ToString();
                expBox.Text = data.experience.ToString();
                telBox.Text = data.telefon.ToString();
                salaryBox.Text = data.salary.ToString();
                numbBox.Text = data.tab_number.ToString();
            }
        }

        private void uploadPersonalButton_Click(object sender, RoutedEventArgs e)
        {
            if (uploadPersonalButton.Content.ToString() == "Отмена")
            {
                uploadPersonalButton.Content = "Изменить";
                editPersonalButton.Visibility = Visibility.Hidden;

            }
            else
            {
                uploadPersonalButton.Content = "Отмена";
                editPersonalButton.Visibility = Visibility.Visible;
            }
            setFieldData();
            setEditingField();
        }
        void setEditingFieldDisable()
        {
            bool ch = (bool)DataContext.GetType().GetProperty("isReadField").GetValue(DataContext);
            if (ch == false)
            {
                ComboboxData comboBox2 = new ComboboxData();
                comboBox2.isReadField = true;
                comboBox2.isEnabledField = false;
                DataContext = comboBox2;
            }
        }
        void setEditingField()
        {
            bool ch = (bool)DataContext.GetType().GetProperty("isReadField").GetValue(DataContext);
            if (ch == true)
            {
                ComboboxData comboBox2 = new ComboboxData();
                comboBox2.isReadField = false;
                comboBox2.isEnabledField = true;
                DataContext = comboBox2;
            }
            else
            {
                ComboboxData comboBox2 = new ComboboxData();
                comboBox2.isReadField = true;
                comboBox2.isEnabledField = false;
                DataContext = comboBox2;
            }
            setFieldData();
        }

        private void telBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,()+]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void editPersonalButton_Click(object sender, RoutedEventArgs e)
        {
            updatePersonal();
            editPersonalButton.Visibility = Visibility.Hidden;
            uploadPersonalButton.Content = "Изменить";
            setEditingFieldDisable();
            setFieldData();
        }
        void updatePersonal()
        {
            using (var db = new PharmaContext())
            {
                var data = ((App.Person)usergrid1.SelectedItem != null) ? (App.Person)usergrid1.SelectedItem : null;
                if (data != null)
                {
                    int index = 0;
                    index = usergrid1.SelectedIndex;
                    var spec = (Specialization)specialBox.SelectedItem;
                    var person = db.persons.Where(x => x.id == data.id).FirstOrDefault();

                    person.name = nameBox.Text;
                    person.familia = famBox.Text;
                    person.otchestvo = otchBox.Text;
                    person.gender = genderBox.Text;
                    person.specialization = spec.id;
                    person.data_in = (DateTime)dateinBox.SelectedDate;
                    person.data_end = (dateendBox.SelectedDate.HasValue == true) ? (dateendBox.SelectedDate.Value < dateinBox.SelectedDate.Value) ? null : dateendBox.SelectedDate : null;//dateendBox.SelectedDate.Value.Year < dateinBox.SelectedDate.Value.Year
                    person.experience = int.Parse(expBox.Text);
                    person.telefon = telBox.Text.ToString();
                    person.salary = int.Parse(salaryBox.Text);
                    person.tab_number = int.Parse(numbBox.Text);
                    db.SaveChanges();
                    UploadList();
                    usergrid1.SelectedIndex = index;
                }
            }
        }
        private void delPersonalButton_Click(object sender, RoutedEventArgs e)
        {
            if (usergrid1.SelectedIndex != -1)
            {
                MessageBoxResult ds = MessageBox.Show("Удалить выбранный объект?", "Подтверждение удаления", MessageBoxButton.YesNo);
                switch (ds)
                {
                    case MessageBoxResult.Yes:
                        using (var db = new App.PharmaContext())
                        {
                            idPerson = (int)usergrid1.SelectedItem.GetType().GetProperty("id").GetValue(usergrid1.SelectedItem);
                            var deletePerson = db.persons.Where(x => x.id == UserControl2_persons.idPerson).FirstOrDefault();
                            db.persons.Remove(deletePerson);
                            db.SaveChanges();
                        }
                        UploadList();
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