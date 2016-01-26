using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Speech.Synthesis;
using System.ServiceModel.Web;
using System.Reflection;
using System.Configuration;

namespace TTS_ServiceLibrary
{
    public class TTSService : TTSIService
    {
        public Stream GetTTSAudio(string convertText)
        {
            if (convertText == null || convertText.Length > 10000)
                return null;

            var guid = Guid.NewGuid();
            var filePathStr = ConfigurationManager.AppSettings["tempWaveFileLocation"] +  "out" + guid.ToString() + ".wav";

            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SetOutputToWaveFile(filePathStr);
                synth.Speak(convertText);

                String headerInfo = "attachment; filename=out.wav";
                WebOperationContext.Current.OutgoingResponse.ContentType = "audio/x-wav";
                WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
            }

            return File.OpenRead(filePathStr);
        }

        
    }
}
