using System.Collections.Generic;

namespace JDC.Common.Entities
{
    public class ChatGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }

        public List<Message> Messages { get; set; }
    }
}
