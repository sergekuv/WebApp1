using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Friends
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public IndexModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

        public IList<Friend> Friend { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Friend != null)
            {
                Friend = await _context.Friend
                    .Where(f => f.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Include(f => f.CurrentFriend)
                    .Include(f => f.User).ToListAsync();
            }
        }
    }
}
