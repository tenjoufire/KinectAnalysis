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
        private static FaceInfo faceInfo;
        private static int page = 0;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("ようこそ 利用したい機能の数字を入力してください");
                Console.WriteLine("[1]:単一ファイルの読み込み[2]:(TrackingID別)単一ファイルの読み込み[0]:終了");
                page = Int32.Parse(Console.ReadLine());
                string fileName;
                switch (page)
                {
                    
                    case 1:
                        Console.WriteLine("ファイル名を入力してください(.jsonは不要)");
                        fileName = Console.ReadLine();
                        fileName += ".json";

                        faceInfo = GetJsonObject(fileName);

                        GetAverageRotationScore(faceInfo.faceInfos);

                        break;

                    case 2:
                        Console.WriteLine("ファイル名を入力してください(.jsonは不要)");
                        fileName = Console.ReadLine();
                        fileName += ".json";

                        faceInfo = GetJsonObject(fileName);

                        GetAverageRotationScores();


                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("数字がちゃうで");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void GetAverageRotationScore(List<Face> faceInfos)
        {
            float[] PYR = new float[3];
            foreach (var face in faceInfos)
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

        private static FaceInfo GetJsonObject(string filePath)
        {
            string json;
            using (var sr = new StreamReader(filePath))
            {
                json = sr.ReadLine();
            }
            return JsonConvert.DeserializeObject<FaceInfo>(json);
        }

        private static void GetAverageRotationScores()
        {
            var faceInfos = faceInfo.faceInfos;
            List<Face>[] faceDividedID = new List<Face>[6];
            for(int i = 0; i < faceDividedID.Length; i++)
            {
                if(faceInfos.Where(x => x.trackingID == i) != null)
                {
                    faceDividedID[i] = faceInfos.Where(x => x.trackingID == i).ToList();
                }
            }

            Console.WriteLine("[0]:pitch [1]:yaw [2]:roll");

            for(int i = 0; i < faceDividedID.Length; i++)
            {
                Console.WriteLine($"\nTrackingID {i}");
                GetAverageRotationScore(faceDividedID[i]);
            }
            Console.ReadLine();
        }
    }
}
