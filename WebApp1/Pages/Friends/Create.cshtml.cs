using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp1.Areas.Identity.Data;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Friends
{
    public class CreateModel : PageModel
    {
        readonly UserManager<WebApp1User> _userManager;
        private readonly WebApp1.Data.WebApp1Context _context;

        public CreateModel(UserManager<WebApp1User> userManager, WebApp1.Data.WebApp1Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            var listToDisplay = (from friend in _context.Friend
                                where (friend.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier) )
                                select friend.CurrentFriendId).ToList();

            var listToDisplay1 = from user in _context.Users
                                 where
                                 (user.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
                                 && !(from friend in _context.Friend
                                      where (friend.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                                      select friend.CurrentFriendId).Contains(user.Id)
                                 select user;

            //var curFriends = from friend in _context.Friend
            //                 where (Friend.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            //                 select Friend.CurrentFriendId;

            //var listToDisplay = _context.Users.Where(u => (u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList().Except(curFriends);

            //var notFriends = _context.Users.Where(u => (u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier) && Пользователь не является другом данного пользователя, т.е., отсутствует в таблице Friends в паре с пользователем) ))
            //ViewData["CurrentFriendId"] = new SelectList(_context.Users.Where(u => u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier)), "Name");
            ViewData["CurrentFriendId"] = new SelectList(listToDisplay1, "Id", "Name");


            //ViewData["CurrentFriendId"] = new SelectList(_context.Users, "Id", "Email", "Name");
            //var curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Friend Friend { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Friend.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var u = await _userManager.GetUserAsync(User);
            //var u = _context.Users.Where(u => u.Id == Friend.UserId).FirstOrDefault();
            Friend.User = u;
            var f = _context.Users.Where(u => u.Id == Friend.CurrentFriendId).FirstOrDefault();
            Friend.CurrentFriend = f;

            //var u = _context.WebApp1User.First(User => User.UserId == Friend.UserId);
            //var all = await _context.WebApp1User.ToListAsync();
            //if (!ModelState.IsValid)
            //{
            //    var e = ModelState.Values.SelectMany(v => v.Errors).ToList();
            //    return Page();
            //}

            

            ModelState.ClearValidationState(nameof(Friend));
            if (!TryValidateModel(Friend, nameof(Friend)))
            {
                return Page();
            }

            _context.Friend.Add(Friend);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
