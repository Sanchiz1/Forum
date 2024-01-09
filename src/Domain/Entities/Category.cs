using Domain.Common;
using Domain.Constants;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public Category() { }

        public bool CanEdit(string Account_Role)
        {
            return Account_Role == Roles.Admin;
        }
        public bool CanDelete(string Account_Role)
        {
            return Account_Role == Roles.Admin;
        }
    }
}
