# winTTSService
Text to Speech Windows Service for REDCap using the Speech.Synthesis library.

This application is designed to help reduce the lag involved in the transfer of audio data from vanderbilt by creating the audio file on the redcap server and avoiding the double hop.

To install, compile and use the windows service installation command:
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe "c:\path\to\executable\TTSWindowsService.exe"

To reference in REDCap, comment out the following line in file "speak.php" in the "Surveys" folder:
content = http_post('https://redcap.vanderbilt.edu/tts/index.php', $params);
and replace it with:
$content = http_get('http://localhost:8007/GetTTSAudio?convertText='.urlencode($q));
