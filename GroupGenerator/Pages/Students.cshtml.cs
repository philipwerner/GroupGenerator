using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupGenerator.Data;
using GroupGenerator.Models;

namespace GroupGenerator.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly GroupGenerator.Data.GroupGeneratorDbContext _context;

        public StudentsModel(GroupGenerator.Data.GroupGeneratorDbContext context)
        {
            _context = context;
        }

        public IList<Class> Class { get;set; }

        public async Task OnGetAsync()
        {
            Class = await _context.Class.ToListAsync();
        }
    }
}
