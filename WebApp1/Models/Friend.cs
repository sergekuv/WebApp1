using WebApp1.Areas.Identity.Data;

namespace WebApp1.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public WebApp1User User { get; set; }

        public string CurrentFriendId { get; set; }

        public WebApp1User CurrentFriend { get; set; }
    }
}
