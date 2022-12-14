using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Messages
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public DetailsModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

      public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Message == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            else 
            {
                Message = message;
            }
            return Page();
        }
    }
}
