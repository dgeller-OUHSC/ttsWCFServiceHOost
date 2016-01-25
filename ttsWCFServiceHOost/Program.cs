using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace ttsWCFServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost host = new WebServiceHost(typeof(TTS_ServiceLibrary.TTSService), new Uri("http://localhost:8000"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(TTS_ServiceLibrary.TTSIService), new WebHttpBinding(), "");
            ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            host.Open();
            Console.WriteLine("Service is up and running");
            Console.WriteLine("Press enter to quit ");
            Console.ReadLine();
            host.Close();



        }
    }
}
