using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.IO;

namespace TTS_ServiceLibrary
{
    [ServiceContract]
    public interface TTSIService
    {
        [OperationContract]
        [WebGet]
        string GetMessage(string inputMessage);

        [OperationContract]
        [WebInvoke]
        string PostMessage(string inputMessage);

        [OperationContract]
        [WebGet]
        Stream GetText(string convertText);

    }
}
