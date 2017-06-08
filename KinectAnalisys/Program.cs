using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectWPF;
using Newtonsoft.Json;
using System.IO;

namespace KinectAnalysis
{
    class Program
    {
        private static string jsonString;
        static void Main(string[] args)
        {
            Console.WriteLine("ファイル名を入力してください(同一ディレクトリに配置されてたら)");
            var fileName = Console.ReadLine();
            using(var sr = new StreamReader(fileName))
            {
                jsonString = sr.ReadLine();
            }

            var faceInfo = JsonConvert.DeserializeObject<FaceInfo>(jsonString);

            Console.ReadLine();
        }
    }
}
