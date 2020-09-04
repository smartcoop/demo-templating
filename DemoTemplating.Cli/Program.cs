using DemoTemplating.RendererLib;
using System;
using System.Diagnostics;
using System.IO;

namespace DemoTemplating.Cli {
    public class Program {
        public const string WorkDirectory = "./";
        static void Main(string[] args) {
            if (args.Length == 3) {
                string htmlText = "", jsonText = "";
                ReadFiles(args, ref htmlText, ref jsonText);
                string outputFile = Path.Combine(WorkDirectory, args[2]);
                CheckHtmlResultFile(outputFile);
                RenderHtml(outputFile, htmlText, jsonText);
                RunHtmlInBrowser(outputFile);
            } else {
                Console.WriteLine("Please enter parameter values.");
                Console.WriteLine("hello.html name1.json outputFile.html");
            }
        }

        private static void ReadFiles(string[] args, ref string htmlText, ref string jsonText) {
            try {
                htmlText = File.ReadAllText(args[0]);
                jsonText = File.ReadAllText(args[1]);
                Console.WriteLine("Files read successfully");
            } catch (FileNotFoundException e) {
                Console.WriteLine($"File not found {e.FileName}");
                Environment.Exit(1);
            }
        }

        private static void CheckHtmlResultFile(string outputFile) {
            if (!File.Exists(outputFile)) {
                File.Create(outputFile);
                Console.WriteLine($"File {outputFile} created");
            }
        }

        private static void RenderHtml(string htmlResultPath, string htmlText, string jsonText) {
            Console.WriteLine("Call the method to render the html");
            File.WriteAllText(htmlResultPath,
                TemplatingService.Render(htmlText, jsonText));
        }

        private static void RunHtmlInBrowser(string outputFile) {
            new Process {
                StartInfo = new ProcessStartInfo(Path.GetFullPath(outputFile)) {
                    UseShellExecute = true
                }
            }.Start();
            Console.WriteLine("Result is render in browser");
        }
    }
}