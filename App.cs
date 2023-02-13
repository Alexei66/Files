using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppFiles
{
    public static class App
    {
        public static int CheckValue()
        {
            int correctValue;
            while (!int.TryParse(Console.ReadLine(), out correctValue) || correctValue < 1 || correctValue > 3)
            {
                Console.WriteLine("�����������! �������� ��� ���.");
            }

            return correctValue;
        }

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

        public static void CompressedFile(string file, string compressedFile)
        {
            using FileStream ss = new FileStream(file, FileMode.OpenOrCreate);                       ////////////// ��������� �����  ///////////////
            {
                using FileStream ts = File.Create(compressedFile);   // ����� ��� ������ ������� �����   //////////////            ///////////////
                {
                    // ����� ���������
                    using GZipStream cs = new GZipStream(ts, CompressionMode.Compress);
                    {
                        ss.CopyTo(cs); // �������� ����� �� ������ ������ � ������
                        Console.WriteLine("������ �����  ���������. ����: {0}  �����: {1}.", ss.Length, ts.Length);
                    }
                }
            }
        }

        public static int CountNewLine(string strFile)  // ������� ���-�� �����
        {
            int lineCount = 1;
            for (int i = 0; i < strFile.Length; i++)
            {
                if (strFile[i] == '\n')
                {
                    lineCount++;                        // ������� ��������� �� ����� ������
                }
            }
            return lineCount;
        }

        public static class Validator                       // ���� ���������� � ������ ������ �� ������?
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
                return true;
            }
        }

        public static string PathAndNameTXT(string pathTXT, string nameTXT)
        {
            return $"{pathTXT}{nameTXT}.txt";
        }

        public static string PathAndName7Z(string path7Z, string name7Z)
        {
            return $"{path7Z}{name7Z}.7z";
        }

        public static string DirectoryCheck(string pathDirectory)
        {
            while (Directory.Exists(pathDirectory) is false || pathDirectory[pathDirectory.Length - 1] != '\\')
            {
                Console.Write("������ ���� ���! �������� ��� ���: ");
                pathDirectory = Console.ReadLine();
            }
            return pathDirectory;
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Starts()
        {
            Console.Write($"������� ���� � ����� � ������ � ������ ��������  �����, ��� -  E:\\�#\\SB\\Date\\ : ");
            string path = Console.ReadLine();  // ���� � ����� � ������ � ������ ��������  �����  E:\�#\SB\Date\N.txt
            string pathFile = DirectoryCheck(path);

            Console.Write($"������� ��� ����� ��� ������ ��������� �����: ");
            string filePathName = Console.ReadLine();

            string filePath = PathAndNameTXT(pathFile, filePathName);

            if (Validator.NullOrEmpty(filePath) is false)
            {
                Console.WriteLine("���� ������ ��� �� ������");
                return;
            }

            string numbFileString = File.ReadAllText(filePath);  // ��������� ����� N  �� �����
            int numberFromFile = int.Parse(numbFileString);

            string allGroupsInLine = StringBuild(numberFromFile);  // ����� ���������� �����

            Console.Write("\n������ '1' ���� ������ �������� ��������� � ���� ��� '2' ��� ����������: ");

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            int key = CheckValue();
            if (key == 1)
            {
                Console.Write($"\n������� ���� � ����� � ������� ����� �������� ����, ��� -  E:\\�#\\SB\\Date\\ : ");
                string pathSaveFileCheck = Console.ReadLine();  // ���� � ����� � ������� �������� ��������� E:\�#\SB\Date\res.txt
                string pathSaveFile = DirectoryCheck(pathSaveFileCheck);

                Console.Write($"������� ��� ��� ������ �����, � ������� ������� ���������: ");
                string nameSaveFile = Console.ReadLine();

                string savedFile = PathAndNameTXT(pathSaveFile, nameSaveFile);

                File.WriteAllText(savedFile, allGroupsInLine);  // ������ ����� � ���������� �� allGroupsInLine

                stopWatch.Stop();

                Console.WriteLine($"�� ���������� ���������� {stopWatch.ElapsedMilliseconds} �����������");
                Console.Write($"������ ����� = {allGroupsInLine.Length}. \n������ '1' ���� ������ �������������� ����, '2' ��� ��������� ���������� ����� ��� '3' ��� ����������: ");

                key = CheckValue();
                switch (key)
                {
                    case 1:
                        if (key == 1)
                        {
                            Console.Write($"\n������� ���� � ����� � ������� ����� �������� �����, ��� -  E:\\�#\\SB\\Date\\ : ");
                            string pathCompressedCheck = Console.ReadLine(); // ���� ���� ��������� ����� E:\�#\SB\Date\res_txt.7z
                            string pathCompressed = DirectoryCheck(pathCompressedCheck);

                            Console.Write($"������� ��� ��� ������ ������: ");
                            string nameCompressed = Console.ReadLine();

                            string compressed = PathAndName7Z(pathCompressed, nameCompressed);

                            CompressedFile(savedFile, compressed);
                        }
                        break;

                    case 2:
                        stopWatch.Restart();
                        int lineCount = CountNewLine(allGroupsInLine);
                        //Console.ReadKey();
                        stopWatch.Stop();

                        Console.WriteLine($"�� ���������� ���������� {stopWatch.ElapsedMilliseconds} �����������");
                        Console.WriteLine($"���-�� ����� = {lineCount} ��� N = {numberFromFile}");
                        break;

                    case 3:
                        break;
                }
            }
        }
    }
}