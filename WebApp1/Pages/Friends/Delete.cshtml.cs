using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Friends
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public DeleteModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Friend Friend { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FirstOrDefaultAsync(m => m.Id == id);

            if (friend == null)
            {
                return NotFound();
            }
            else 
            {
                Friend = friend;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }
            var friend = await _context.Friend.FindAsync(id);

            if (friend != null)
            {
                Friend = friend;
                _context.Friend.Remove(Friend);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
