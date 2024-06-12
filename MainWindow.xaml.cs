using JogoGourmet.Data;
using JogoGourmet.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace JogoGourmet;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly GuessEngine _guessEngine;
    private Dish currentGuess;
    private Description currentDescription;

    public MainWindow(GuessEngine guessEngine)
    {
        InitializeComponent();

        _guessEngine = guessEngine;
        currentGuess = guessEngine.GetInitialDish();
        currentDescription = currentGuess.DishDescriptions.FirstOrDefault().Description;
        ConcatMainTextWithDescription(currentDescription.Text);
    }

    private void OnGuessYesButtonClick(object sender, RoutedEventArgs e)
    {
        var result = _guessEngine.GuessDish(true, currentGuess, currentDescription);
        if (result.IsCorrect)
        {
            this.Close();
            ShowSuccessWindow();
        }
        else
        {
            if (result.Dish.DishDescriptions.Any())
            {
                currentDescription = result.Dish.DishDescriptions.First().Description;
                MainTextBlock.Text = $"The dish you're thinking {currentDescription.Text} ?";
            }
            else MainTextBlock.Text = $"The dish you're thinking is {result.Dish.Name} ?";
        }
    }
    private void OnGuessNoButtonClick(object sender, RoutedEventArgs e)
    {
        var result = _guessEngine.GuessDish(false, currentGuess, currentDescription);
        if (result.Dish == null)
        {
            this.Close();
            ShowNewDishWindow(_guessEngine, currentGuess.Name);
        }
        else
        {
            if (result.Dish.DishDescriptions.Any())
            {
                currentDescription = result.Dish.DishDescriptions.First().Description;
                MainTextBlock.Text = $"The dish you're thinking {currentDescription.Text} ?";
            }
            else MainTextBlock.Text = $"The dish you're thinking is {result.Dish.Name} ?";
        }
    }
    private void ConcatMainTextWithDescription(string text)
    {
        MainTextBlock.Text = $"The dish you're thinking {text} ?";
    }

    private static void ShowSuccessWindow()
    {
        var sucessWindow = new SuccessWindow();
        sucessWindow.Show();
    }
    private static void ShowNewDishWindow(GuessEngine guessEngine, string lastDishName)
    {
        var dishNameWindow = new NewDishNameWindow(guessEngine, lastDishName);
        dishNameWindow.Show();
    }
}
