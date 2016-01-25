using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTSlib
{
    public class TTSService :TTSIService
    {
        public string GetMessage(string inputMessage)
        {
            return "Calling Get for you " + inputMessage;
        }
        public string PostMessage(string inputMessage)
        {
            return "Calling Post for you " + inputMessage;
        }

    }
}
