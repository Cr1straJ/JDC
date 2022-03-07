﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Chat entity.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Gets or sets a chat id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a chat name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a chat users.
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        public List<User> Users { get; set; }

        /// <summary>
        /// Gets or sets a chat messages.
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}
