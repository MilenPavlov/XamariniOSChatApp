﻿using System;

namespace XamChat.Core.Models
{
    public class Message
    {
        public string Id { get; set; }

        public string ConversationId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string ToId { get; set; }
    }
}
