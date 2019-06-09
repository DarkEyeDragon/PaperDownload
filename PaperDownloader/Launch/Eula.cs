using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperDownload.Launch
{
    class Eula
    {
        public string WorkingDir { get; set; }
        public string Message { get; }

        public Eula(string workingDir, bool accept)
        {
            WorkingDir = workingDir;

            Message =
                "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula)."
                + "\r\n#You also agree that tacos are tasty, and the best food in the world."
                + $"\r\n#{string.Format(CultureInfo.InvariantCulture, "{0:ddd MMM dd HH:mm:ss z yyyy}", DateTime.Now)}"
                + $"\r\neula={accept.ToString().ToLower()}";
        }

        //EEE MMM dd HH:mm:ss Z yyyy
        public void Create()
        {
            var path = Path.Combine(WorkingDir, "eula.txt");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                // Create a new file     
                using (var fs = File.CreateText(path))
                {
                    // Add some text to file    
                    fs.WriteLine(Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}