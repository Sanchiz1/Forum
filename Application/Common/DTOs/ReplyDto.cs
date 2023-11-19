using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs
{
    public class ReplyDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_Id { get; set; }
        public int User_Id { get; set; }
    }
}
