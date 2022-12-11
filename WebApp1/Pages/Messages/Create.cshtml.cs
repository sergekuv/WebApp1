using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Messages
{
    public class CreateModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;
        //public Message msgInfo;

        public CreateModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null || _context.Friend == null || _context.Message == null)
            {
                // how to react on this?
            }



            //ViewData["RecipientId"] = msgInfo.RecipientId;
            //ViewData["SenderId"] = new SelectList(msgInfo.SenderId);
                return Page();
        }


        [BindProperty]
        public Message Message { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Friend == null || _context.Message == null)
            {
                // how to react on this?
            }

            var friend = _context.Friend.Include(f => f.User).Include(f => f.CurrentFriend).FirstOrDefault(f => f.Id == id);

            if (friend == null)
            {
                // how to react on this?
            }

            // Message = new Message();
            //FriendRecordId = id;

            //var currentUserId = friend.UserId;
            Message.SenderId = friend.UserId;
            Message.Sender = friend.User;
            Message.RecipientId = friend.CurrentFriendId;
            Message.Recipient = friend.CurrentFriend;


            //if (!ModelState.IsValid)
            //  {
            //      return Page();
            //  }
            ModelState.ClearValidationState(nameof(Message));
            if (!TryValidateModel(Message, nameof(Message)))
            {
                var e = ModelState.Values.SelectMany(v => v.Errors).ToList();
                return Page();
            }


            _context.Message.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = id.ToString() });
            //return RedirectToPage("./Index?id=" + id.ToString());
        }
    }
}
