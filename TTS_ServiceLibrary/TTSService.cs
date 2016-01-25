using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Speech.Synthesis;
using System.ServiceModel.Web;
using System.Reflection;

namespace TTS_ServiceLibrary
{
    public class TTSService : TTSIService
    {
        public string GetMessage(string inputMessage)
        {
            return "Calling Get for you " + inputMessage;
        }
        public string PostMessage(string inputMessage)
        {
            return "Calling Post for you " + inputMessage;
        }


        public Stream GetText(string convertText)
        {
            if (convertText == null)
                return null;



            //var guid = Guid.NewGuid();
            //var filePathStr = "tmpWav/out" + guid.ToString() + ".wav";
            //using (SpeechSynthesizer synth = new SpeechSynthesizer())
            //{
            //    synth.SetOutputToWaveFile(filePathStr);
            //    synth.Speak(convertText);

            //    String headerInfo = "attachment; filename=out.wav";
            //    WebOperationContext.Current.OutgoingResponse.ContentType = "audio/x-wav";
            //    WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
            //    //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
            //}
            //return File.OpenRead(filePathStr);


            //SpeechAudioFormatInfo audioOutputFormat = new SpeechAudioFormatInfo(11025, AudioBitsPerSample.Eight, AudioChannel.Mono);
            using (MemoryStream audioOutputStream = new MemoryStream())
            {

                //var guid = Guid.NewGuid();
                //var filePathStr = "tmpWav/out" + guid.ToString() + ".wav";
                //synth.SetOutputToWaveFile(filePathStr);
                //synth.Speak(convertText);


                //String headerInfo = "attachment; filename=out.wav";

                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    var audioOutputFormat = synth.Voice.SupportedAudioFormats.First();
                    //var mi = synth.GetType().GetMethod("SetOutputStream", BindingFlags.Instance | BindingFlags.NonPublic);
                    //mi.Invoke(synth, new object[] { audioOutputStream, audioOutputFormat, true, true });
                    //synth.SelectVoice(synth.GetInstalledVoices().First().VoiceInfo.Name);
                    synth.Speak(convertText);
                }

                //WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;

                //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                //WebOperationContext.Current.OutgoingResponse.ContentType = "";

                //var responseArr = audioOutputStream.ToArray();
                //Response.ContentType = "audio/wav";
                //Response.Write(responseArr);
                //Response.End();

                //Response.Clear();
                //Response.ContentType = "audio/x-wav";
                //Response.AddHeader("Content-Disposition", "attachment; filename=output.wav");
                //Response.Write(audioOutputStream.ToArray());
                //audioOutputStream.WriteTo(Response.OutputStream); //works too
                //Response.Flush();
                //Response.Close();
                //Response.End();
                //String headerInfo = "attachment; filename=out.wav";
                //WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
                String headerInfo = "attachment; filename=out.wav";
                WebOperationContext.Current.OutgoingResponse.ContentType = "audio/x-wav";
                WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
                //WebOperationContext.Current.OutgoingResponse.ContentType = "audio/x-wav";
                audioOutputStream.Seek(0, SeekOrigin.Begin);
                return audioOutputStream;
            }

        }

    }
}
