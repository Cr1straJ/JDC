using System;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Message entity.
    /// </summary>
    public class Message
    {
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
        public Chat ChatGroup { get; set; }

        /// <summary>
        /// Gets or sets the user id who sent the message.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who sent the message.
        /// </summary>
        public User User { get; set; }

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
    }
}
