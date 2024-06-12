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
    /// Interaction logic for NewDishDescriptionWindow.xaml
    /// </summary>
    public partial class NewDishDescriptionWindow : Window
    {
        private readonly GuessEngine _guessEngine;
        public string NewDishDescription { get; set; }
        public string NewDishName { get; set; }
        public string LastDishName { get; set; }
        public NewDishDescriptionWindow(GuessEngine guessEngine, string newDishName, string lastDish)
        {
            InitializeComponent();
            _guessEngine = guessEngine;
            NewDishName = newDishName;
            LastDishName = lastDish;
        }
        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewDishDescriptionTextBox.Text))
            {
                NewDishDescriptionTextBlock.Text = $"{NewDishName} is ______________ but {LastDishName} isn't";
                NewDishDescription = NewDishDescriptionTextBox.Text;
                DialogResult = true;                
                _guessEngine.SaveNewDish(NewDishName, NewDishDescription);
                Close();
            }
            else
            {
                MessageBox.Show("Please, insert a valid answer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
