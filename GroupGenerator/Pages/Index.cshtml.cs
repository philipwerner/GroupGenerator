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

        }

        [BindProperty]
        public int Number { get; set; }
        [BindProperty]
        public bool Radio { get; set; }
        public string[,] Groups { get; set; }
        public void OnPost()
        {
            MakeSomeGroups(Radio, Number);
        }

        public void MakeSomeGroups(bool radio, int num)
        {
            if (radio == true)
            {
                ByGroupSize(num);
            }
            else
            {
                ByGroupNumber(num);
            }
        }
        public void ByGroupSize(int size)
        {
            var Class = _context.Class.Select(p => p.FirstName).ToArray();
            string[] SClass = Shuffle(Class);
            int groupSize = size;
            int groupNumber = SClass.Length / size;
            string[,] groups = new string[groupNumber, size];
            int c = 0;
            while (c < SClass.Length)
            {
                for (var i = 0; i < groupNumber; i++)
                {
                    for (var j = 0; j < size; j++)
                    {
                        groups[i, j] = SClass[c];
                        c++;
                    }
                }
            }
            Groups = groups;
            ViewData["GroupSize"] = groupSize;
            ViewData["GroupNumber"] = groupNumber;


        }

        public void ByGroupNumber(int num)
        {
            var Class = _context.Class.Select(p => p.FirstName).ToArray();
            string[] SClass = Shuffle(Class);
            int groupSize = SClass.Length / num;
            string[,] groups = new string[num, groupSize];
            int c = 0;
            while (c < SClass.Length)
            {
                for (var i = 0; i < num; i++)
                {
                    for (var j = 0; j < groupSize; j++)
                    {
                        groups[i, j] = SClass[c];
                        c++;
                    }
                }
            }
             Groups = groups;
            ViewData["GroupSize"] = groupSize;
            ViewData["GroupNumber"] = num;

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
