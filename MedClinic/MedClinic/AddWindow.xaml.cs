using MedClinic.classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MedClinic
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void IdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MedClinicList>));
            List<MedClinicList> medList = new List<MedClinicList>(); //створюється новий ліст із трофєєв
            if (!File.Exists("MedClinicList.xml"))//якщо не має файлу
            {
                using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.OpenOrCreate))//створюється файл
                {
                    medList = new List<MedClinicList>()//створюється пустий ліст
                    {

                    };
                    formatter.Serialize(fs, medList);//записує пустий ліст в файл 
                }
            }
            else
            {
                using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Open))//якщо файл є то відкриває його
                {
                    medList = (List<MedClinicList>)formatter.Deserialize(fs);//витягує з файла з ліста трофеїв айтемси
                }

            }
            MedClinicList item = new MedClinicList();//створюється новий елемент
            
            item.Id = int.Parse(IdTextBox.Text);
            item.Name = NameTextBox.Text;
            item.Surname = SurnameTextBox.Text;
            item.Diagnoz = DiagnozTextBox.Text;//в нього записується властивість
            item.DateOfCreate = dpDate.SelectedDate.Value;
            medList.Add(item);
            using (FileStream fs = new FileStream("MedClinicList.xml", FileMode.Create))//в файл пеезаписуються елементи
            {
                formatter.Serialize(fs, medList);
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
