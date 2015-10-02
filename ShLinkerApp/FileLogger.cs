using System.IO;

namespace ShLinkerApp
{
    public class FileLogger:LoggerBase
    {
        private string _filePath = @".\ShLinkerLog.txt";

        private StreamWriter _streamWriter;

        public FileLogger(){}

        public FileLogger(string filePath)
        {
            _filePath = filePath;
            _streamWriter = new StreamWriter(_filePath);
        }

        public override void Log(string message)
        {
            //_streamWriter.Flush();
            using (var streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            //_streamWriter.Close();          
        }

        public void DisposeLogger()
        {
            _streamWriter.Close();
            _streamWriter.Dispose();
        }
    }
}
