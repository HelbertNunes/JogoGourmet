using JogoGourmet.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace JogoGourmet
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly JogoGourmetContext _jogoGourmetContext;
        private readonly GuessEngine _guessEngine;

        public StartWindow(JogoGourmetContext jogoGourmetContext, GuessEngine guessEngine)
        {
            InitializeComponent();
            _jogoGourmetContext = jogoGourmetContext;
            _guessEngine = guessEngine;
            _jogoGourmetContext.Database.EnsureCreated();
        }
        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {            
            MainWindow mainWindow = new MainWindow(_guessEngine);
            mainWindow.Show();            
        }
    }
}
