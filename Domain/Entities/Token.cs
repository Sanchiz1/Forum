using System;

namespace Domain.Entities
{
    public class Token
    {
        public string Value { get; set; } = null;
        public DateTime Issued_at { get; set; }
        public DateTime Expires_at { get; set; }

        public Token() { }

        public Token(string value, DateTime issued_at, DateTime expires_at)
        {
            Value = value;
            Issued_at = issued_at;
            Expires_at = expires_at;
        }
    }
}
