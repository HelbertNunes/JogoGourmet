using JogoGourmet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Guess_Engine
{
    public class Guess
    {
        public Dish? Dish { get; set; }
        public bool IsCorrect { get; set; }
        public List<Description> CorrectDescriptions { get; set; }

        public Guess(Dish dish, bool isCorrect, List<Description> correctDescriptions)
        {
            Dish = dish;
            IsCorrect = isCorrect;
            CorrectDescriptions = correctDescriptions;
        }
    }
}
