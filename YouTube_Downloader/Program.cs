using System;
using System.IO;
using System.Diagnostics;
using VideoLibrary;
using static System.Console;


namespace YouTube_Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (isYouTubeVideoLink(args[0]))
                {
                    WriteLine("Downloading...");
                    DownloadVideo(args[0], args[1], true);

                    System.Console.WriteLine("Video is successfully downloaded!");
                }
                else
                {
                    WriteLine("An error occurred while providing the video link!");
                }
            }
            else
            {
                WriteLine("An error occurred while entering the argument!");
            }
        }
        static void DownloadVideo(string link, string output, bool openAfterDownload = false)
        {
            YouTube youtube = YouTube.Default;
            YouTubeVideo video = youtube.GetVideo(link);

            byte[] result = video.GetBytes();
            string fileName = Path.Combine(output, $"{video.Title}.{video.FileExtension}");
            File.WriteAllBytes(fileName, result);

            if (openAfterDownload == true)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = fileName;
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }
        static bool isYouTubeVideoLink(string link)
        {
            Uri uri = new Uri(link);
            return uri.Host == "www.youtube.com" || uri.Host == "wwww.youtu.be" ||
                uri.Host == "youtube.com" || uri.Host == "youtu.be";
        }
    }
}
