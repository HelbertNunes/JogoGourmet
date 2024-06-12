using JogoGourmet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace JogoGourmet
{
    /// <summary>
    /// Interaction logic for NewDishNameWindow.xaml
    /// </summary>
    public partial class NewDishNameWindow : Window
    {
        private readonly GuessEngine _guessEngine;
        public string NewDishName { get; set; }
        public string LastDishName { get; set; }
        public NewDishNameWindow(GuessEngine guessEngine, string lastDishName)
        {
            InitializeComponent();
            _guessEngine = guessEngine;
            LastDishName = lastDishName; 
        }
        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewDishTextBox.Text))
            {
                NewDishName = NewDishTextBox.Text;
                DialogResult = true;                
                var sucessWindow = new NewDishDescriptionWindow(_guessEngine, NewDishName, LastDishName);
                sucessWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please, insert a valid answer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
