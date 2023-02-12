using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppFiles
{
    public static class App
    {
        public static int CheckValue()
        {
            int correctValue;
            while (!int.TryParse(Console.ReadLine(), out correctValue) || correctValue < 1 || correctValue > 2)
            {
                Console.WriteLine("�����������! �������� ��� ���.");
            }

            return correctValue;
        }

        /// <summary>
        /// ������� ������ �� �����
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static string StringBuild(int N)
        {
            StringBuilder sb = new StringBuilder();

            int countGrup = 1;
            int start = 1;

            sb.Append($"������ {countGrup}: ");

            for (int i = 1; i <= N; i++)
            {
                if (i != start * 2)
                {
                    sb.Append($"{i}  ");
                    continue;
                }
                start *= 2;
                countGrup++;

                sb.AppendLine();
                sb.Append($"������ {countGrup}: {i}  ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// ���������� ����
        /// </summary>
        /// <param name="file"></param>
        /// <param name="compressedFile"></param>
        public static void CompressedFile(string file, string compressedFile)
        {
            using FileStream ss = new FileStream(file, FileMode.OpenOrCreate);             ////////////// ��������� �����  ////////////////////////////
            {
                using FileStream ts = File.Create(compressedFile);   // ����� ��� ������ ������� �����   //////////////            ////////////////////////////
                {
                    // ����� ���������
                    using GZipStream cs = new GZipStream(ts, CompressionMode.Compress);
                    {
                        ss.CopyTo(cs); // �������� ����� �� ������ ������ � ������
                        Console.WriteLine("������ ����� {0} ���������. ����: {1}  �����: {2}.",
                                          file,
                                          ss.Length,
                                          ts.Length);
                    }
                }
            }
        }

        /// <summary>
        /// ������� ���-�� ��������� �� ����� ������
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static int CountNewLine(string strFile)
        {
            int lineCount = 1;
            for (int i = 0; i < strFile.Length; i++)  // ������� ���-�� �����
            {
                if (strFile[i] == '\n')
                {
                    lineCount++;
                }
            }
            return lineCount;
        }

        public static class Validator
        {
            public static bool NullOrEmpty(string path)
            {
                if (File.Exists(path) is false)
                {
                    return false;
                }
                if (File.ReadAllLines(path).Any() is false)
                {
                    return false;
                }
                //string numbN = File.ReadAllText(path);
                //bool res = string.IsNullOrEmpty(numbN);
                return true;
            }
        }

        public static void Start()
        {
            Console.Write($"������� ���� � ����� � ������ � ������ ��������  �����, ��� -  E:\\�#\\SB\\Date\\N.txt  ");
            string path = Console.ReadLine();  // ���� � ����� � ������ � ������ ��������  �����  E:\�#\SB\Date\N.txt

            string writeRes = @"E:\�#\SB\Date\res.txt"; // ���� � ����� � ������� �������� ���������
            string compressed = @"E:\�#\SB\Date\res_txt.7z"; // ���� ���� ��������� �����

            if (Validator.NullOrEmpty(path) is false)
            {
                Console.WriteLine("���� ������");
                return;
            }

            string numbN = File.ReadAllText(path);  // ��������� ����� N  �� �����
            int numberFromFile = int.Parse(numbN);

            string result = StringBuild(numberFromFile);  // ����� ���������� �����

            int key;

            Console.Write("������ '1' ���� ������ �������� ��������� � ���� ��� '2' ��� ��������� ���-�� �����: ");

            key = CheckValue();

            //DateTime date = DateTime.Now;  // ������� ����� ������� �� ������� �������
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            if (key == 1)
            {
                File.WriteAllText(writeRes, result);  // ������ ����� � ���������� �� "result"  �  ����� "writeRes"

                stopWatch.Stop();

                //TimeSpan timeSpan = DateTime.Now.Subtract(date);  // ����������� ������� �� �������
                Console.WriteLine($"�� ���������� ���������� {stopWatch.ElapsedMilliseconds} �����������");

                Console.Write($"������ ����� = {result.Length}. ������ '1' ���� ������ �������������� ���� ��� '2' ��� ����������: ");

                key = CheckValue();

                if (key == 1)
                {
                    CompressedFile(writeRes, compressed);
                }
            }
            else
            {
                int lineCount = CountNewLine(result);
                //Console.ReadKey();
                stopWatch.Stop();
                //TimeSpan timeSpan = DateTime.Now.Subtract(date);
                Console.WriteLine($"�� ���������� ���������� {stopWatch.ElapsedMilliseconds} �����������");
                Console.WriteLine($"���-�� ����� = {lineCount} ��� N = {numberFromFile}");
            }
        }
    }
}