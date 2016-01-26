using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TTSWindowsService
{
    public partial class TTSWinService : ServiceBase
    {

        private WebServiceHost host;

        public TTSWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            if (host == null || host.State != CommunicationState.Opened)
            {
                #region create temp directory if it doesn't exist
                if (!Directory.Exists(ConfigurationManager.AppSettings["tempWaveFileLocation"]))
                {
                    try
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["tempWaveFileLocation"]);
                    }
                    catch (Exception e)
                    {
                        EventLog.WriteEntry("exception creating temp folder: " + e.Message);
                    }
                }
                #endregion

                try
                {
                    var hostURI = new Uri("http://localhost:" + ConfigurationManager.AppSettings["wavServicePort"]);
                    EventLog.WriteEntry("starting webhost on " + hostURI);
                    host = new WebServiceHost(typeof(TTS_ServiceLibrary.TTSService), hostURI);
                    ServiceEndpoint ep = host.AddServiceEndpoint(typeof(TTS_ServiceLibrary.TTSIService), new WebHttpBinding(), "");
                    ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                    stp.HttpHelpPageEnabled = false;
                    stp.IncludeExceptionDetailInFaults = false;
                    host.Open();
                    EventLog.WriteEntry("web service host started on " + hostURI.AbsoluteUri);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("exception starting webserver: " + ex.Message);
                }
            }
            cleanupOldFiles();
        }

        private void startWinService()
        {
        }

        private void cleanupOldFiles()
        {
            var dirFiles = Directory.GetFiles(ConfigurationManager.AppSettings["tempWaveFileLocation"]);
            var filesRemoved = false;
            foreach (var fileItem in dirFiles)
            {
                if (File.GetCreationTime(fileItem) < DateTime.Now.AddMinutes(-1 * int.Parse(ConfigurationManager.AppSettings["minsToKeepWavFile"])))
                {
                    lock (fileItem)
                    {
                        if (File.Exists(fileItem))
                        {
                            try
                            {
                                File.Delete(fileItem);
                                filesRemoved = true;
                            }
                            catch (Exception ex)
                            {
                                EventLog.WriteEntry("exception deleting file" + ex.Message);
                            }
                        }
                    }
                }
            }
            if (filesRemoved)
            {
                EventLog.WriteEntry("files successfully removed");
            }
        }

        protected override void OnStop()
        {
            if (host != null && host.State == CommunicationState.Opened)
            {
                host.Close();
            }
        }

    }
}
