using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Areas.Identity.Data;

namespace WebApp1.Models
{
    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }

        public WebApp1User webApp1User { get; set; }
    }
}
