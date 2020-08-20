using DemoTemplating.RendererLib;
using System;
using System.Diagnostics;
using System.IO;

namespace DemoTemplating.Cli {
    public class Program {
        static void Main(string[] args) {
            if (args.Length == 2) {
                string htmlText = "", jsonText = "";
                ReadFiles(args, ref htmlText, ref jsonText);
                string ResourcesDirectory = @"..\..\..\Resources";
                string htmlResultFile = @"htmlResult.html";
                string htmlResultPath = Path.Combine(ResourcesDirectory, htmlResultFile);
                CheckHtmlResultFile(htmlResultPath);
                DisplayHtmlResultInBrowser(htmlResultPath, htmlText, jsonText);
            } else {
                Console.WriteLine("Please enter parameter values.");
                Console.WriteLine("../../../Resources/hello.html ../../../Resources/name1.json");
            }
        }

        private static void CheckHtmlResultFile(string htmlResultPath) {
            if (!File.Exists(htmlResultPath)) {
                File.Create(htmlResultPath);
                Console.WriteLine($"File {htmlResultPath.Substring(htmlResultPath.LastIndexOf(@"\") + 1)} created");
            } else {
                string[] readText = File.ReadAllLines(htmlResultPath);
                if (readText.Length > 0) {
                    File.WriteAllText(htmlResultPath, "");
                    Console.WriteLine($"File {htmlResultPath.Substring(htmlResultPath.LastIndexOf(@"\") + 1)} found and cleaned");
                }
            }
        }

        private static void ReadFiles(string[] args, ref string htmlText, ref string jsonText) {
            try {
                htmlText = File.ReadAllText(args[0]);
                jsonText = File.ReadAllText(args[1]);
                Console.WriteLine("Files read successfully");

            } catch (FileNotFoundException e) {
                Console.WriteLine($"File not found {e.FileName}");
            }
        }

        private static void DisplayHtmlResultInBrowser(string htmlResultPath, string htmlText, string jsonText) {
            Console.WriteLine("Call the method to render the html");
            File.WriteAllText(htmlResultPath,
                TemplatingService.Render(htmlText, jsonText));
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(htmlResultPath) {
                UseShellExecute = true
            };
            p.Start();
            Console.WriteLine("Result is render in browser");
        }
    }
}