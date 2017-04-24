using JDP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtractFlvConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            extractFLV();
            Console.ReadLine();
            
        }

        private static void changeFileName()
        {
            string[] textContent = File.ReadAllLines(@"C:\Users\rockymay\Desktop\file.txt");
            int countContent = textContent.Count();
            Console.WriteLine(countContent);

            //string folderPath = Environment.CurrentDirectory;
            string folderPath = @"C:\Users\rockymay\Documents\Audio-MP3\晓松奇谈";
            DirectoryInfo Di = new DirectoryInfo(folderPath);
            
            var fileArray = Di.GetFiles("*.mp3");
            for (int i = 0; i< fileArray.Count(); i++)
            {
                Console.WriteLine("Previous Name: " + fileArray[i].Name);
                Console.WriteLine("New Name: " + textContent[(textContent.Count()-i-1)]);
                Console.WriteLine(Environment.NewLine);
                fileArray[i].MoveTo(fileArray[i].Directory + @"\" + textContent[(textContent.Count() - i - 1)] + ".mp3");
            }
        }
        private static void replaceFileName()
        {
            string[] textContent = File.ReadAllLines(@"C:\Users\rockymay\Desktop\file.txt");
            int countContent = textContent.Count();
            Console.WriteLine(countContent);
            string folderPath = @"C:\Users\rockymay\Documents\Audio-MP3\新建文件夹";
            DirectoryInfo Di = new DirectoryInfo(folderPath);

            var fileArray = Di.GetFiles("*.mp3");

            for (int i = 0; i < 14; i++)
            {
                //Delete file first few characters
               
                string heading = textContent[i].Substring(0, textContent[i].IndexOf(" "));
                Console.WriteLine(heading);
                string iDate = "10/24/2014";
                DateTime pDate = Convert.ToDateTime(iDate);
                string nDate = pDate.AddDays(7 + (7 * i)).ToString("yyyy-MM-dd");
                Console.WriteLine("New Heading: " + nDate + "期 ");
                string newName = (nDate + "期 " + fileArray[i].Name);
                Console.WriteLine(newName);
                fileArray[i].MoveTo(fileArray[i].Directory + "\\" + nDate + "期 " + fileArray[i].Name);
            }
        }
        private static void countdown()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Programm ended.  Console exit in: ");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(5-i);
                Thread.Sleep(1000);
            }
        }
        private static void extractFLV()
        {

            var newline = Environment.NewLine;
            Console.WriteLine("Welcome to FLV Extract" + newline + "##########################" + newline + "You have to put exe file in flv file folder" + newline + "##########################" + newline);


            Console.WriteLine("Do you want to save audio? Y/N");
            bool audio = (Console.ReadLine().ToUpper()) == "Y" ? true : false;
            Console.WriteLine("Do you want to save video? Y/N");
            bool video = (Console.ReadLine().ToUpper()) == "Y" ? true : false;
            Console.WriteLine("Do you want to save timecode? Y/N");
            bool timecode = (Console.ReadLine().ToUpper()) == "Y" ? true : false;
           
            string currentPath = Environment.CurrentDirectory;
            DirectoryInfo di = new DirectoryInfo(currentPath);
            if (di.GetFiles("*.flv").Count() == 0)
            { Console.WriteLine("Cannot find FLV file here"); }

            foreach (FileInfo file in di.GetFiles("*.flv"))
            {

                Console.WriteLine("Total Files:" + di.GetFiles("*.flv").Count() + " Selected File: " );
                Console.WriteLine(file.Name);
                FLVFile flvFile = new FLVFile(file.FullName);
                // first param is whether or not to extract audio streams (true)
                // second param is whether or not to extract video streams (false)
                // third param is whether or not to extract timecodes (false)
                // fourth param is the delegate that gets called in case of an overwrite prompt (leave null in case you want to overwrite automatically)
                flvFile.ExtractStreams(audio, video, timecode, null);
                Console.WriteLine("Completed" + Environment.NewLine);

            }

        }

    }
}
