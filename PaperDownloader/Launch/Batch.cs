using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PaperDownloader.Servers.Versions;

namespace PaperDownload.Launch
{
    class Batch
    {
        public string Arguments { get; }
        public string FileName { get; set; }
        public string WorkingDir { get; set; }

        public Batch(Project project, string version, string build, string workingDir, string fileName)
        {
            WorkingDir = workingDir;
            FileName = fileName;

            Arguments =
                $":start\r\necho off\r\ncls\r\njava -Xms3G -Xmx3G -XX:+UseG1GC -XX:+UnlockExperimentalVMOptions -XX:MaxGCPauseMillis=100 -XX:+DisableExplicitGC -XX:TargetSurvivorRatio=90 -XX:G1NewSizePercent=50 -XX:G1MaxNewSizePercent=80 -XX:G1MixedGCLiveThresholdPercent=35 -XX:+AlwaysPreTouch -XX:+ParallelRefProcEnabled -jar {project}-{version}_{build}.jar\r\npause\r\ngoto start";
        }

        public void Create()
        {
            var path = Path.Combine(WorkingDir, FileName);
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
                    fs.WriteLine(Arguments);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}