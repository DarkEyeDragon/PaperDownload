using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PaperDownloader.Servers.Versions
{
    public enum Project
    {
        Paper,
        Waterfall,
        Travertine
    };

    class Projects
    {
        private static readonly string _baseUrl = "https://papermc.io/api/v1/";

        public static string DownloadPath { get; set; }


        private static readonly string UserAgent = $"{Assembly.GetEntryAssembly().GetName().Name}/{Assembly.GetEntryAssembly().GetName().Version} ({AssemblyContentType.WindowsRuntime})";

        public static string[] GetVersions(Project project)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
                var json = wc.DownloadString($"{_baseUrl}{project.ToString().ToLower()}");
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                foreach (var keyValuePair in result)
                {
                    if (keyValuePair.Key.Contains("version"))
                    {
                        JArray array = (JArray) keyValuePair.Value;
                        return array.Select(jv => (string) jv).ToArray();
                    }
                }
            }

            return null;
        }

        public static string[] GetBuild(Project project, string version)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
                var json = wc.DownloadString($"{_baseUrl}{project.ToString().ToLower()}/{version}");
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                foreach (var keyValuePair in result)
                {
                    if (keyValuePair.Key.Contains("builds"))
                    {
                        var buildNum =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(keyValuePair.Value.ToString());
                        foreach (var valuePair in buildNum)
                        {
                            if (valuePair.Key.Equals("all"))
                            {
                                JArray array = (JArray) valuePair.Value;
                                return array.Select(jv => (string) jv).ToArray();
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Download the external Paper jar trough their API
        /// </summary>
        /// <param name="project">Projects of the the type <see cref="Project"/></param>
        /// <param name="version">Minecrafts target version</param>
        /// <param name="build">The build number of the jar</param>
        public static void DownloadJar(Project project, string version, string build)
        {
            using (WebClient wc = new WebClient())
            {
                string localPath = $"{DownloadPath}/{project}-{version}_{build}.jar";
                wc.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
                wc.DownloadFileAsync(new Uri(
                        $"{_baseUrl}{project.ToString().ToLower()}/{version}/{build}/download"),
                    localPath);

                wc.DownloadProgressChanged += DownloadProgressChanged;
                wc.DownloadFileCompleted += DownloadFileCompleted;
            }
        }


        public delegate void DownloadChangeEventHandler(int progress);

        public static event DownloadChangeEventHandler DownloadChangeEvent;

        public delegate void DownloadCompletedHandler();

        public static event DownloadCompletedHandler DownloadCompleted;

        private static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadChangeEvent?.Invoke(e.ProgressPercentage);
            Debug.WriteLine($"{e.ProgressPercentage}%");
        }

        private static void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadCompleted?.Invoke();
        }
    }
}