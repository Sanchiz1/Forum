﻿using System;

namespace Application.Common.DTOs
{
    public class ReplyDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Edited { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_User_Id { get; set; }
        public int User_Id { get; set; }
    }
}
