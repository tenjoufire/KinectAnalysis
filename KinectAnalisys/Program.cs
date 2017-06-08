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
        private static int[] PYR = new int[3];
        static void Main(string[] args)
        {
            Console.WriteLine("ファイル名を入力してください(同一ディレクトリに配置されてたら)");
            var fileName = Console.ReadLine();
            using(var sr = new StreamReader(fileName))
            {
                jsonString = sr.ReadLine();
            }

            var faceInfo = JsonConvert.DeserializeObject<FaceInfo>(jsonString);

            foreach(var face in faceInfo.faceInfos)
            {
                PYR[0] += face.pitch;
                PYR[1] += face.yaw;
                PYR[2] += face.roll;
            }
            for (int i = 0; i < PYR.Length; i++)
                PYR[i] /= faceInfo.faceInfos.Count;

            for (int i = 0; i < PYR.Length; i++)
                Console.WriteLine($"[{i}] {PYR[i]}");

            Console.ReadLine();
        }
    }
}
