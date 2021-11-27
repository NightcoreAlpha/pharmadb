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
using System.Windows.Shapes;

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для AccountWIndow.xaml
    /// </summary>
    public partial class AccountWIndow : Window
    {
        public string AccountName { get; set; }
        public AccountWIndow()
        {
            InitializeComponent();
            namebox.Text = MainWindow.MainAccountName;
        }
        public void ShowAccountName()
        {
            MessageBox.Show(AccountName);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new App.PharmaContext())
                {
                    var newname = db.accounts.Where(x => x.id == 1).FirstOrDefault();
                    if (newname != null)
                    {
                        newname.name_account = namebox.Text;
                        db.SaveChanges();
                    }
                    else
                        try
                        {
                            using (var dbaccount = new App.PharmaContext())
                            {
                                App.Account accounts = new App.Account()
                                {
                                    id = 1,
                                    name_account = namebox.Text
                                };
                                dbaccount.accounts.Add(accounts);
                                dbaccount.SaveChanges();
                                //MainWindow.;
                                Close();
                            }
                        }
                        catch (Exception exp) { MessageBox.Show("Не удалось добавить пользователя: " + exp.Message, "Ошибка"); }
                }
                Close();
            }
            //catch (Exception) { MessageBox.Show("Некорректный ввод", "Ошибка"); }
            catch (Exception exp) { MessageBox.Show("Ошибка подключения: " + exp.Message, "Ошибка"); }

        }
    }
}