using System.Diagnostics.Metrics;
using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppFiles
{
    internal class Program
    {
        //static int CheckValue()
        //{
        //    int correctValue;
        //    while (!int.TryParse(Console.ReadLine(), out correctValue) || correctValue < 1|| correctValue>2)
        //    {
        //        Console.WriteLine("Неправильно! Попробуй еще раз.");
        //    }

        //        return correctValue;
        //}
        ///// <summary>
        ///// создает строку из чисел
        ///// </summary>
        ///// <param name="N"></param>
        ///// <returns></returns>
        //static string StringBuild(int N)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    int countGrup = 1;
        //    int start = 1;

        //    sb.Append($"Группа {countGrup}: ");

        //    for (int i = 1; i <= N; i++)
        //    {
        //        if (i != start * 2)
        //        {
        //            sb.Append($"{i}  ");
        //            continue;
        //        }
        //        start *= 2;
        //        countGrup++;

        //        sb.AppendLine();
        //        sb.Append($"Группа {countGrup}: {i}  ");
        //    }
        //    return sb.ToString();
        //}
        ///// <summary>
        ///// архивирует файл
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="compressedFile"></param>
        //static void CompressedFile(string file, string compressedFile)
        //{
        //    using (FileStream ss = new FileStream(file, FileMode.OpenOrCreate))             ////////////// АРХИВАЦИЯ ФАЙЛА  ////////////////////////////
        //    {
        //        using (FileStream ts = File.Create(compressedFile))   // поток для записи сжатого файла   //////////////            ////////////////////////////
        //        {
        //            // поток архивации
        //            using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
        //            {
        //                ss.CopyTo(cs); // копируем байты из одного потока в другой
        //                Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
        //                                  file,
        //                                  ss.Length,
        //                                  ts.Length);
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// считает кол-во переходов на новую строку
        ///// </summary>
        ///// <param name="strFile"></param>
        ///// <returns></returns>
        //static int CountNewLine (string strFile)
        //{
        //    int lineCount = 1;
        //    for (int i = 0; i < strFile.Length; i++)  // подсчет кол-ва групп
        //    {
        //        if (strFile[i] == '\n')
        //        {
        //            lineCount++;
        //        }
        //    }
        //    return lineCount;
        //}
        private static void Main(string[] args)
        {
            //App.Starts();
            int start = 1_000;
            int orel = 1;
            int count = 0;
            Random rand = new Random();

            for (int i = 0; i < start; i++)
            {
                int monetka = rand.Next(1, 3);
                if (monetka == orel)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}