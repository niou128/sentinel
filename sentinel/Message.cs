using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sentinel
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }

        public Message() { }
    }

    public class Message<T> : Message
    {
        public new T Tag { get; set; }
    }
}
