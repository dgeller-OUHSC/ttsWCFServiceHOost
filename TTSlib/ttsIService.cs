using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace TTSlib
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [ServiceContract]
    public interface TTSIService
        {
            [OperationContract]
            [WebGet]
            string GetMessage(string inputMessage);

             [OperationContract]
            [WebInvoke]
            string PostMessage(string inputMessage);

    }
}
