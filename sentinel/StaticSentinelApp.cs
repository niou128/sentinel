using sentinel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sentinel
{
    public static class StaticSentinelApp
    {
        public static IMessenger Messenger { get { return SentinelApp.Current.Messenger; } }
    }

    public class SentinelApp
    {
        public static SentinelApp Current { get { return _Current ?? (_Current = new SentinelApp()); } }
        protected static SentinelApp _Current;

        public IMessenger Messenger
        {
            get
            {
                if (_Messenger == null)
                    _Messenger = new uselessMessenger();

                return _Messenger;
            }
            set { _Messenger = value; }
        }
        private IMessenger _Messenger;
    }
}
