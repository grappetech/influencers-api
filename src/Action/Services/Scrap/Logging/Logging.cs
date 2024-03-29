﻿using System.IO;

namespace Action.Services.Scrap.Logging
{
    /// <summary>
    ///     Logging class for Log functionality
    /// </summary>
    public static class Logging
    {
        /// <summary>
        ///     Method to write reports to local machine.
        /// </summary>
        /// <param name="contents">string contents</param>
        public static void WriteReportToDisk(string contents)
        {
            var fileName = ""; // ConfigurationManager.AppSettings["logTextFileName"].ToString();
            FileStream fStream = null;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                fStream = File.Create(fileName);
            }
            else
            {
                fStream = File.OpenWrite(fileName);
            }

            using (TextWriter writer = new StreamWriter(fStream))
            {
                writer.WriteLine(contents);
                writer.Flush();
            }

            fStream.Dispose();
        }
    }
}