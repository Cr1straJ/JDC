using System;
using System.Collections.Generic;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    public class Message
    {
        public Message(int chatGroupId, int userId, RecipientType recipent, string content, DateTime sendingDate)
        {
            this.ChatGroupId = chatGroupId;
            this.UserId = userId;
            this.Recipent = recipent;
            this.Content = content;
            this.SendingDate = sendingDate;
        }

        public int Id { get; set; }

        public int ChatGroupId { get; set; }

        public ChatGroup ChatGroup { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public RecipientType Recipent { get; set; } = RecipientType.Deafult;

        public string Content { get; set; }

        public DateTime SendingDate { get; set; }

        public List<User> Writed { get; set; }
    }
}
