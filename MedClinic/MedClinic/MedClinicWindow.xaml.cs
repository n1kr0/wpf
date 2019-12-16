using MedClinic.classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace MedClinic
{
    /// <summary>
    /// Логика взаимодействия для MedClinicWindow.xaml
    /// </summary>
    public partial class MedClinicWindow : Window
    {
        public MedClinicWindow()
        {
            InitializeComponent();
            if (!File.Exists("MedClinicList.xml"))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<classes.MedClinicList>));
                using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.OpenOrCreate))
                {
                    List<classes.MedClinicList> films = new List<classes.MedClinicList> {
                         new classes.MedClinicList
                        {
                            Id=1,
                            Name="Name1",
                            Surname="Surname1",
                            Diagnoz="Diagnoz1",
                            DateOfCreate=DateTime.Parse("1.11.2019")
                        },
                         new classes.MedClinicList
                        {
                            Id=2,
                            Name="Name2",
                            Surname="Surname2",
                            Diagnoz="Diagnoz2",
                            DateOfCreate=DateTime.Parse("1.12.2019")
                        },

                         new classes.MedClinicList
                        {
                            Id=3,
                            Name="Name3",
                            Surname="Surname3",
                            Diagnoz="Diagnoz3",
                            DateOfCreate=DateTime.Parse("3.12.2019")
                        },
                         new classes.MedClinicList
                        {
                            Id=4,
                            Name="Name4",
                            Surname="Surname4",
                            Diagnoz="Diagnoz4",
                            DateOfCreate=DateTime.Parse("4.12.2019")
                        },
                    };
                    formatter.Serialize(fs, films);
                }
            }
        }

        private void Work()
        {
            dgClinic.Items.Clear();
            XmlSerializer formatter = new XmlSerializer(typeof(List<classes.MedClinicList>));


            List<classes.MedClinicList> items = new List<classes.MedClinicList>();
            if (!File.Exists("MedClinicList.xml"))
            {
                using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.OpenOrCreate))
                {
                    items = new List<classes.MedClinicList>()
                    {
                    };
                    formatter.Serialize(fs, items);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Open))
                {
                    items = (List<classes.MedClinicList>)formatter.Deserialize(fs);
                }

            }

            foreach (var item in items)
            {

                dgClinic.Items.Add(new { Id = item.Id, Name = item.Name, Surname = item.Surname, Diagnoz = item.Diagnoz, DateOfCreate = item.DateOfCreate });
            }
        }


        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Work();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow dlg = new AddWindow();
            if (dlg.ShowDialog().Value == true)
            {
                Work();
            };
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            int tmp = int.Parse((dgClinic.SelectedCells[0].Column.GetCellContent(dgClinic.SelectedItem) as TextBlock).Text);


            XmlSerializer formatter = new XmlSerializer(typeof(List<MedClinicList>));
            List<MedClinicList> items = new List<MedClinicList>(); ;

            using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Open))
            {
                items = (List<MedClinicList>)formatter.Deserialize(fs);
            }
            MedClinicList film = new MedClinicList();
            foreach (var item in items)
            {
                if (item.Id == tmp)
                {
                    film = item;
                }
            }
            items.Remove(film);



            using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Create))
            {

                formatter.Serialize(fs, items);
            }
            Work();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int tmp = int.Parse((dgClinic.SelectedCells[0].Column.GetCellContent(dgClinic.SelectedItem) as TextBlock).Text);
            XmlSerializer formatter = new XmlSerializer(typeof(List<MedClinicList>));
            List<MedClinicList> items = new List<MedClinicList>(); ;

            using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Open))
            {
                items = (List<MedClinicList>)formatter.Deserialize(fs);
            }
            MedClinicList medList = new MedClinicList();
            foreach (var item in items)
            {
                if (item.Id == tmp)
                {
                    medList = item;

                    AddWindow dlg = new AddWindow();
                    {
                        dlg.IdTextBox.Text = medList.Id.ToString();
                        dlg.NameTextBox.Text = medList.Name.ToString();
                        dlg.SurnameTextBox.Text = medList.Surname.ToString();
                        dlg.DiagnozTextBox.Text = medList.Diagnoz.ToString();
                        dlg.dpDate.Text = medList.DateOfCreate.ToString();

                    }
                    if (dlg.ShowDialog().Value == true)
                    {
                        medList.Id = int.Parse(dlg.IdTextBox.Text);
                        medList.Name = dlg.NameTextBox.Text;
                        medList.Surname = dlg.SurnameTextBox.Text;
                        medList.Diagnoz = dlg.DiagnozTextBox.Text;
                
                        using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Create))
                        {
                            formatter.Serialize(fs, items);
                        }

                        Work();
                    };
                }
            }
        }
    }
}
