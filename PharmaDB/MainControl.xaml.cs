﻿using System;
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

namespace PharmaDB
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        PharmaContext db = new App.PharmaContext();
        public MainControl()
        {
            InitializeComponent();
            //using (var db = new PharmaContext())
            //{
                personalCountBox.Text = db.persons.Count().ToString();
                squadCountBox.Text = db.med_squad.Count().ToString();
            //}
        }
    }
}