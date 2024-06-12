using JogoGourmet.Data;
using JogoGourmet.Guess_Engine;
using JogoGourmet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet
{
    public class GuessEngine
    {
        private readonly JogoGourmetContext _jogoGourmetContext;
        private List<Dish> avaiableGuessDishes;
        private List<Description> avaiableGuessDescriptions;
        private List<Description> correctDescriptions = new List<Description>();
        public Guess currentGuess { get; set; }

        public GuessEngine(JogoGourmetContext jogoGourmetContext)
        {
            _jogoGourmetContext = jogoGourmetContext;
        }

        public Dish GetInitialDish()
        {
            avaiableGuessDishes = _jogoGourmetContext.Dishes
                .Include(d => d.DishDescriptions)
                .ThenInclude(dd => dd.Description)
                .ToList();
            avaiableGuessDescriptions = _jogoGourmetContext.Descriptions
                .Include(d => d.DishDescriptions)
                .ThenInclude(dd => dd.Dish)
                .ToList();

            var firstGuess = avaiableGuessDishes.Where(d => d.DishDescriptions.Count() > 1);
            if (firstGuess.Any())
            {
                return firstGuess.First();
            }
            return avaiableGuessDishes.FirstOrDefault();
        }

        public Guess GuessDish(bool result, Dish dish, Description description)
        {
            currentGuess = new Guess(dish, false, correctDescriptions);
            if (result)
            {
                RemoveCurrentWithoutDescription(description);
                correctDescriptions.Add(description);
                avaiableGuessDescriptions.Remove(description);
            }
            else
            {
                RemoveCurrentWithDescription(description);
                avaiableGuessDescriptions.Remove(description);
            }

            if (avaiableGuessDishes.Any() && avaiableGuessDishes.Count() > 1)
            {
                currentGuess.Dish = avaiableGuessDishes.First();
                currentGuess.IsCorrect = false;
                currentGuess.CorrectDescriptions = correctDescriptions;
                return currentGuess;
            }
            if (!avaiableGuessDishes.Any() && result)
            {
                currentGuess.IsCorrect = true;
                currentGuess.CorrectDescriptions = correctDescriptions;
                return currentGuess;
            }
            if (avaiableGuessDishes.Any() && !result)
            {
                currentGuess.Dish = avaiableGuessDishes.First();
                currentGuess.IsCorrect = false;
                currentGuess.CorrectDescriptions = correctDescriptions;
                return currentGuess;
            }
            else
            {
                currentGuess.IsCorrect = false;
                currentGuess.CorrectDescriptions = correctDescriptions;
                return currentGuess;
            }
        }

        private void RemoveCurrentWithoutDescription(Description description)
        {
            var result = avaiableGuessDishes
            .Where(dish => dish.DishDescriptions.Any(dd => dd.DescriptionId == description.Id))
            .ToList();

            avaiableGuessDishes = result.Select(dish => new Dish
            {
                Id = dish.Id,
                Name = dish.Name,
                DishDescriptions = dish.DishDescriptions
                   .Where(dd => dd.DescriptionId != description.Id).ToList()
            })
              .ToList();
        }
        private void RemoveCurrentWithDescription(Description description)
        {
            var result = avaiableGuessDishes.Where(dish => !dish.DishDescriptions.Any(dd => dd.DescriptionId == description.Id))
                                                     .ToList();

            avaiableGuessDishes = result.Select(dish => new Dish
            {
                Id = dish.Id,
                Name = dish.Name,
                DishDescriptions = dish.DishDescriptions
                   .Where(dd => dd.DescriptionId != description.Id).ToList()
            })
              .ToList();
        }

        internal void SaveNewDish(string newDishName, string newDishDescriptiontext)
        {
            Dish dish = new Dish{Name = newDishName};
            Description newDescription = new Description { Text = newDishDescriptiontext };            
            DishDescription newDishDescription = new DishDescription
            {
                Dish = dish,
                Description = newDescription
            };
            
            dish.DishDescriptions.Add(newDishDescription);

            _jogoGourmetContext.Dishes.Add(dish);
            _jogoGourmetContext.Descriptions.Add(newDescription);            
            _jogoGourmetContext.SaveChanges();
        }
    }
}
