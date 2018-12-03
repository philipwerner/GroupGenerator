using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupGenerator.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GroupGeneratorDbContext _context;
        public IndexModel(GroupGeneratorDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            //take in a list of names and produce groups of names with n members
            var Class = _context.Class.Select(p => p.FirstName).ToArray();
            string[] SClass = Shuffle(Class);

        }
        public string[,] ByGroupSize(int size, string[] sclass)
        {
            int groupSize = size;
            int groupNumber = sclass.Length / size;
            string[,] Groups = new string[groupNumber, size];
            for (var i = 0; i < groupNumber; i++)
            {
                for (var j = 0; j < sclass.Length; j++)
                {
                    Groups[i, j] = sclass[j];
                }
            }
            return Groups;
        }

        // this method takes in the floor and the ceiling | then uses the random methods .Next which returns a random number inside of the bounds that you define
        private static int GetRandom(int floor, int ceil)
        {
            Random _rand = new Random();

            return _rand.Next(floor, ceil + 1);
        }


        // shuffle method that uses the helper GetRandom method, which needs the lower and upper bounds
        private static string[] Shuffle(string[] array)
        {
            // base case if the array is less than one because it doesn't need to be shuffled
            if (array.Length <= 1)
            {

                return array;
            }

            // iterate through the array and call your random method on length of the array inserting the upper and lower bounds.  
            // take the value of GetRandom and if it doesn't equal this method, swap the index
            for (int i = 0; i < array.Length - 1; i++)
            {
                int randomIndex = GetRandom(i, array.Length - 1);

                if (randomIndex != i)
                {
                    string temp = array[i];
                    array[i] = array[randomIndex];
                    array[randomIndex] = temp;
                }
            }
            return array;
        }
    }
}
