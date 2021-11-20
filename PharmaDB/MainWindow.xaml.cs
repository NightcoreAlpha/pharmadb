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

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("it is work!1");
            using (App.PharmaContext db = new App.PharmaContext())
            {
                try
                {
                    var persons = from p in db.persons select p;
                    datagrid1.ItemsSource = persons.ToList();
                }
                catch (Exception exp) { MessageBox.Show("Не робит: " + exp.Message, "Ошибка"); }
            }
        }
    }
}
