using System.Diagnostics.Metrics;
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

            public static void Start()
            {
                 string path = @"E:\�#\SB\Date\N.txt";  // ���� � ����� � ������ � ������ ��������  ����� N
                 string writeRes = @"E:\�#\SB\Date\res.txt"; // ���� � ����� � ������� �������� ��������� 
                 string compressed = @"E:\�#\SB\Date\res_txt.7z"; // ���� ���� ��������� �����
                 string numbN = File.ReadAllText(path);  // ��������� ����� N  �� �����

                

                 static void NullOrEmpty(string numbN)
                {
                    if (string.IsNullOrEmpty(numbN))
                    {
                    Console.WriteLine("���� ������");
                    return;
                    }
                 }

                NullOrEmpty(numbN);

                 int N = int.Parse(numbN);
    
                string result = StringBuild(N);  // ����� ���������� �����

                int lineCount = CountNewLine(result);
                int key;
            
                 Console.Write("������ '1' ���� ������ �������� ��������� � ���� ��� '2' ��� ��������� ���-�� �����: ");

                key = CheckValue();

                DateTime date = DateTime.Now;  // ������� ����� ������� �� ������� �������
                if (key == 1)
                {
                    File.WriteAllText(writeRes, result);  // ������ ����� � ���������� �� "result"  �  ����� "writeRes"
                    TimeSpan timeSpan = DateTime.Now.Subtract(date);  // ����������� ������� �� �������
                    Console.WriteLine($"�� ���������� ���������� {timeSpan.TotalSeconds} ������");

                    Console.Write($"������ ����� = {result.Length}. ������ '1' ���� ������ �������������� ���� ��� '2' ��� ����������: ");

                    key = CheckValue();

                    if (key == 1)
                    {
                        CompressedFile(writeRes, compressed);
                    }
                }
                else
                {
                    TimeSpan timeSpan = DateTime.Now.Subtract(date);
                    Console.WriteLine($"�� ���������� ���������� {timeSpan.TotalSeconds} ������");
                    Console.WriteLine($"���-�� ����� = {lineCount} ��� N = {N}");
                }
            }

        
    }

}