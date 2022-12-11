using WebApp1.Areas.Identity.Data;

namespace WebApp1.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string SenderId { get; set; }
        public WebApp1User Sender { get; set; }

        public string RecipientId { get; set; }
        public WebApp1User Recipient { get; set; }
    }
}
