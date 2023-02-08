using System.Diagnostics.Metrics;
using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppFiles
{
    internal class Program
    {
        static int CheckValue()
        {
            int correctValue;
            while (!int.TryParse(Console.ReadLine(), out correctValue) || correctValue < 1|| correctValue>2)
            {
                Console.WriteLine("Неправильно! Попробуй еще раз.");
            }            

                return correctValue;
        }
        /// <summary>
        /// создает строку из чисел
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static string StringBuild(int N)
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
        /// <summary>
        /// архивирует файл
        /// </summary>
        /// <param name="file"></param>
        /// <param name="compressedFile"></param>
        static void CompressedFile(string file, string compressedFile)
        {
            using (FileStream ss = new FileStream(file, FileMode.OpenOrCreate))             ////////////// АРХИВАЦИЯ ФАЙЛА  ////////////////////////////
            {
                using (FileStream ts = File.Create(compressedFile))   // поток для записи сжатого файла   //////////////            ////////////////////////////
                {
                    // поток архивации
                    using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                    {
                        ss.CopyTo(cs); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
                                          file,
                                          ss.Length,
                                          ts.Length);
                    }
                }
            }
        }
        /// <summary>
        /// считает кол-во переходов на новую строку
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        static int CountNewLine (string strFile) 
        {
            int lineCount = 1;
            for (int i = 0; i < strFile.Length; i++)  // подсчет кол-ва групп
            {
                if (strFile[i] == '\n')
                {
                    lineCount++;
                }
            }
            return lineCount;
        }
        static void Main(string[] args)
        {
            var path = @"E:\С#\SB\Date\N.txt";  // путь к папке с файлом в которм записано  число N
            var writeRes = @"E:\С#\SB\Date\res.txt"; // путь к файлу в котором сохранят результат 
            string compressed = @"E:\С#\SB\Date\res_txt.7z"; // путь куда сохранить архив

            string numbN = File.ReadAllText(path);  // получение числа N  из файла  

            
            if (string.IsNullOrEmpty(numbN))
            {
                Console.WriteLine("Файл пустой");
                return;
            }

            int N = int.Parse(numbN);

            string result = StringBuild(N);  // метод заполнения групп

            int lineCount = CountNewLine(result);
           
            //var lineCount = File.ReadAllLines(writeRes).Length; // считает сколько строк в файле
                        
            Console.Write("Напиши '1' если хотите записать результат в файл или '2' для просмотра кол-ва групп: ");
            int key = CheckValue();

            DateTime date = DateTime.Now;  // создает точку отсчета из текущго времени

            if (key==1)
            {
                File.WriteAllText(writeRes, result);  // запись файла с значениями из "result"  в  папку "writeRes"
                TimeSpan timeSpan = DateTime.Now.Subtract(date);  // определение разницы во времени
                Console.WriteLine($"На выполнение потраченно {timeSpan.TotalSeconds} секунд");

                Console.Write($"Размер файла = {result.Length}. Напиши '1' если хотите заархивировать файл или '2' для завершения: ");

                int key1 = CheckValue();

                if (key1 == 1)
                {
                    CompressedFile(writeRes, compressed);                    
                }
            }
            else
            {                
                TimeSpan timeSpan = DateTime.Now.Subtract(date);
                Console.WriteLine($"На выполнение потраченно {timeSpan.TotalSeconds} секунд");
                Console.WriteLine($"Кол-во групп = {lineCount} при N = {N}");
            }

            




        }
    }
}