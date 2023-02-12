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
                Console.WriteLine("Неправильно! Попробуй еще раз.");
            }

            return correctValue;
        }

        public static string StringBuild(int N)
        {
            StringBuilder sb = new StringBuilder();

            int countGrup = 1;
            int start = 1;

            sb.Append($"Группа {countGrup}: ");

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
                sb.Append($"Группа {countGrup}: {i}  ");
            }
            return sb.ToString();
        }

        public static void CompressedFile(string file, string compressedFile)
        {
            using FileStream ss = new FileStream(file, FileMode.OpenOrCreate);                       ////////////// АРХИВАЦИЯ ФАЙЛА  ///////////////
            {
                using FileStream ts = File.Create(compressedFile);   // поток для записи сжатого файла   //////////////            ///////////////
                {
                    // поток архивации
                    using GZipStream cs = new GZipStream(ts, CompressionMode.Compress);
                    {
                        ss.CopyTo(cs); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла  завершено. Было: {0}  стало: {1}.", ss.Length, ts.Length);
                    }
                }
            }
        }

        public static int CountNewLine(string strFile)  // подсчет кол-ва групп
        {
            int lineCount = 1;
            for (int i = 0; i < strFile.Length; i++)
            {
                if (strFile[i] == '\n')
                {
                    lineCount++;                        // подсчет переходов на новую строку
                }
            }
            return lineCount;
        }

        public static class Validator                       // файл существует и первая строка не пустая
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

        public static string PathAndNameTXT(string path, string name)
        {
            StringBuilder sbPpath = new StringBuilder();

            sbPpath.Append($"{path}");
            sbPpath.Append($"{name}.txt");
            return sbPpath.ToString();
        }

        public static string PathAndName7Z(string path, string name)
        {
            StringBuilder sPpath = new StringBuilder();

            sPpath.Append(path);
            sPpath.Append($"{name}.7z");
            return sPpath.ToString();
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Starts()
        {
            Console.Write($"Введите путь к папке с файлом в которм записано  число, так -  E:\\С#\\SB\\Date\\ : ");
            string Path = Console.ReadLine();  // путь к папке с файлом в которм записано  число  E:\С#\SB\Date\N.txt

            Console.Write($"Введите имя файла для чтения исходного числа: ");
            string filePathName = Console.ReadLine();

            string filePath = PathAndNameTXT(Path, filePathName);

            if (Validator.NullOrEmpty(filePath) is false)
            {
                Console.WriteLine("Файл пустой");
                return;
            }

            string numbFileString = File.ReadAllText(filePath);  // получение числа N  из файла
            int numberFromFile = int.Parse(numbFileString);

            string allGroupsInLine = StringBuild(numberFromFile);  // метод заполнения групп

            Console.Write("\nНапиши '1' если хотите записать результат в файл или '2' для завершения: ");

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            int key = CheckValue();
            if (key == 1)
            {
                Console.Write($"Введите путь к папке в которую будет сохранен файл, так -  E:\\С#\\SB\\Date\\ : ");
                string pathSaveFile = Console.ReadLine();  // путь к файлу в котором сохранят результат E:\С#\SB\Date\res.txt
                Console.Write($"Введите имя для нового файла, в который запишем результат: ");
                string nameSaveFile = Console.ReadLine();

                string savedFile = PathAndNameTXT(pathSaveFile, nameSaveFile);

                File.WriteAllText(savedFile, allGroupsInLine);  // запись файла с значениями из allGroupsInLine

                stopWatch.Stop();

                Console.WriteLine($"На выполнение потраченно {stopWatch.ElapsedMilliseconds} миллисекунд");
                Console.Write($"Размер файла = {allGroupsInLine.Length}. Напиши '1' если хотите заархивировать файл, '2' для просмотра количества групп или '3' для завершения: ");

                key = CheckValue();
                switch (key)
                {
                    case 1:
                        if (key == 1)
                        {
                            Console.Write($"Введите путь к папке в которую будет сохранен архив, так -  E:\\С#\\SB\\Date\\res_txt.7z: ");
                            string pathCompressed = Console.ReadLine(); // путь куда сохранить архив E:\С#\SB\Date\res_txt.7z
                            Console.Write($"Введите имя для нового архива: ");
                            string nameCompressed = Console.ReadLine();

                            string compressed = PathAndName7Z(pathCompressed, nameCompressed);

                            CompressedFile(savedFile, compressed);
                        }
                        break;

                    case 2:
                        int lineCount = CountNewLine(allGroupsInLine);
                        //Console.ReadKey();
                        stopWatch.Stop();

                        Console.WriteLine($"На выполнение потраченно {stopWatch.ElapsedMilliseconds} миллисекунд");
                        Console.WriteLine($"Кол-во групп = {lineCount} при N = {numberFromFile}");
                        break;

                    case 3:
                        break;
                }
            }
        }
    }
}