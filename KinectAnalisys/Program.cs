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
        private static int page = 0;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("ようこそ 利用したい機能の数字を入力してください");
                Console.WriteLine("[1]:単一ファイルの読み込み[0]:終了");
                page = Int32.Parse(Console.ReadLine());
                switch (page)
                {
                    case 1:
                        Console.WriteLine("ファイル名を入力してください(.jsonは不要)");
                        var fileName = Console.ReadLine();
                        fileName += ".json";
                        using (var sr = new StreamReader(fileName))
                        {
                            jsonString = sr.ReadLine();
                        }

                        var faceInfo = JsonConvert.DeserializeObject<FaceInfo>(jsonString);

                        GetAverageRotationScore(faceInfo);

                        break;
                    case 0:
                        return;
                }
                Console.WriteLine();
            }
        }

        private static void GetAverageRotationScore(FaceInfo faceInfo)
        {
            foreach (var face in faceInfo.faceInfos)
            {
                PYR[0] += Math.Abs(face.pitch);
                PYR[1] += Math.Abs(face.yaw);
                PYR[2] += Math.Abs(face.roll);
            }
            for (int i = 0; i < PYR.Length; i++)
                PYR[i] /= faceInfo.faceInfos.Count;

            for (int i = 0; i < PYR.Length; i++)
                Console.WriteLine($"[{i}] {PYR[i]}");
        }
    }
}
