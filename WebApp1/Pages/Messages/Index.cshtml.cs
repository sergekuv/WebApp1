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
    public class IndexModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public IndexModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; } = default!;
        public int? FriendRecordId;

        public async Task OnGetAsync(int? id)
        {
            if (id == null || _context.Friend == null || _context.Message == null)
            {
                // how to react on this?
            }

            var friend = await _context.Friend.FirstOrDefaultAsync(f => f.Id == id);

            if (friend == null)
            {
                // how to react on this?
            }
            else
            {
                FriendRecordId = id;

                var currentUserId = friend.UserId;
                var currentUser = friend.User;
                var currentFriendId = friend.CurrentFriendId;
                var currentFriend = friend.CurrentFriend;

                Message = await _context.Message
                    .Include(m => m.Recipient)
                    .Include(m => m.Sender)
                    .Where(m => (m.SenderId == currentUserId && m.RecipientId == currentFriendId) 
                    || (m.SenderId == currentFriendId && m.RecipientId == currentUserId))
                    .ToListAsync();

            }


        }
    }
}
