using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class HashedPassword
    {
        public string Password {  get; set; }
        public string Salt { get; set;}

        public HashedPassword() { }
    }
}
