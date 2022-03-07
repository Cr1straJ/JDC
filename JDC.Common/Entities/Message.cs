using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Message entity.
    /// </summary>
    public class Message
    {
        public Message(int chatGroupId, string userId, RecipientType recipent, string content, DateTime sendingDate)
        {
            ChatGroupId = chatGroupId;
            UserId = userId;
            Recipent = recipent;
            Content = content;
            SendingDate = sendingDate;
        }

        /// <summary>
        /// Gets or sets a message id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a chat group id.
        /// </summary>
        public int ChatGroupId { get; set; }

        /// <summary>
        /// Gets or sets a chat group which includes a message.
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        public Chat ChatGroup { get; set; }

        /// <summary>
        /// Gets or sets the user id who sent the message.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who sent the message.
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }

        [NotMapped]
        public string UserName { get { return this.User.Email; } }

        [NotMapped]
        public string Name { get { return this.User.FirstName; } }

        /// <summary>
        /// Gets or sets the message recipient type.
        /// </summary>
        public RecipientType Recipent { get; set; } = RecipientType.Deafult;

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date the message was sent.
        /// </summary>
        public DateTime SendingDate { get; set; }

        [NotMapped]
        public string Date { get { return SendingDate.ToString("hhh,mm"); } }
    }
}
