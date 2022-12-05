using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Za_zdravie_avtora
{
    public class Humans
    {
        public string Name;
        public string Age;
        public string ID;
    }

    public class Chooser_and_editor
    {
        public void chooser(List<string> students_list, List<Humans> humansList)
        {
            int line_num = 0;

            while (true)
            {
                Console.Clear();

                for (int i = 0; i < students_list.Count(); i++)
                {
                    if (i == line_num)
                    {
                        Console.WriteLine(students_list[i] + " <-- ");
                    }

                    else
                    {
                        Console.WriteLine(students_list[i]);
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow & line_num > 0)
                {
                    line_num--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow & line_num < students_list.Count - 1)
                {
                    line_num++;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.Write(students_list[line_num] + " --> ");
                    students_list[line_num] = Convert.ToString(Console.ReadLine());
                }
                else if (keyInfo.Key == ConsoleKey.F1)
                {
                    saver(students_list, humansList);
                    break;
                }
            }
        }

        private static void saver(List<string> students_list, List<Humans> humansList)
        {
            Console.Clear();
            Console.WriteLine("Введите путь по которому сохранить файл: ");
            string save_path = Console.ReadLine();
            string format = (save_path[save_path.Length - 1]).ToString();
            if (format == "t")
            {
                File.WriteAllLines(save_path, students_list);
            }
            else if (format == "n")
            {
                string serilized = JsonConvert.SerializeObject(humansList);
                File.WriteAllText(save_path, serilized);
            }
            else if (format == "l")
            {
                XmlSerializer serialized = new XmlSerializer(typeof(Humans));
                using (FileStream fs = new FileStream(save_path, FileMode.OpenOrCreate))
                {
                    foreach (var human in humansList)
                    {
                        serialized.Serialize(fs, human);
                    }
                }
            }
        }
    }
}