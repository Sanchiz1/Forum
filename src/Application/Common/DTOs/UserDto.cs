﻿using System;

namespace Application.Common.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime Registered_At { get; set; }
    }
}
