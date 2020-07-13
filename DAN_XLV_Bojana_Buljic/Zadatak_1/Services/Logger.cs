using System;
using System.IO;


namespace Zadatak_1.Services
{
    /// <summary>
    /// Class for logging actions into file
    /// </summary>
    class Logger
    {
        private readonly string loggerFile = @"..\..\Services\Log.txt";

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="message">Message to be written in the file</param>
        public void WriteToFile(string message)
        {
            // Save all the routes to file
            using (StreamWriter streamWriter = new StreamWriter(loggerFile, append: true))
            {
                string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                streamWriter.WriteLine(logMessage.ToString());
            }
        }
    }
}
