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
        public static string MainAccountName { get; set; }
        App.PharmaContext db = new App.PharmaContext();
        public List<App.Person> persons;
        public MainWindow()
        {
            InitializeComponent();
            gridplace1.Children.Clear();
            gridplace1.Children.Add(new MainControl());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }*/
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void lw1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lw1.SelectedIndex;
            switch (index)
            {
                case 0:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new MainControl());
                    break;
                case 1:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControl2_persons());
                    break;
                case 2:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControlLists());
                    break;
                /*case 3:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControlService());
                    break;
                case 4:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new CategoryControl());
                    break;*/
                default:
                    break;
            }
        }
        public void cb(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AccountWIndow aw = new AccountWIndow();
            aw.Owner = this;
            //aw.AccountName = MainAccountName;
            aw.ShowDialog();
            //aw.ShowAccountName();
        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public string goname { get; set; }

    }
}
