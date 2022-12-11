using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Notes
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public DetailsModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

      public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            else 
            {
                Note = note;
            }
            return Page();
        }
    }
}
