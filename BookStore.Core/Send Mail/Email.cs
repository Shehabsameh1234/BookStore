using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Send_Mail
{
    public class Email
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string To { get; set; } = null!;
    }
}
