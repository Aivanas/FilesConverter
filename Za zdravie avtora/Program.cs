using Newtonsoft.Json;
using Za_zdravie_avtora;
using System.Xml.Serialization;

Main_function();
//C:\Users\richb\Desktop\gchg.txt

static void Main_function()
{
    Console.WriteLine("Введите путь изначального файла: ");
    string path_start_file = Console.ReadLine();
    //string path_start_file = "C:\\Users\\richb\\Desktop\\gchg.txt";
    string format = (path_start_file[path_start_file.Length - 1]).ToString();


    while (true)
    {
        if (format == "t")
        {
            List<string> readed_txt = new List<string>(File.ReadAllLines(path_start_file));
            if (readed_txt.Count() >= 3)
            {
                List<Humans> humansList = new List<Humans>();
                for (int i = 0; i < (readed_txt.Count() / 3); i++)
                {
                    Humans human = new Humans();
                    int was = i * 3;

                    human.Name = readed_txt[was];
                    human.Age = readed_txt[was + 1];
                    human.ID = readed_txt[was + 2];
                    humansList.Add(human);
                }

                Chooser_and_editor chooser = new Chooser_and_editor();
                chooser.chooser(readed_txt, humansList);

                break;
            }


            else
            {
                Console.WriteLine("Выбранный файл пуст :(");
            }
        }

        if (format == "n")
        {
            string text = File.ReadAllText(path_start_file);
            List<Humans> humansList = JsonConvert.DeserializeObject<List<Humans>>(text);
            List<string> studentsList = new List<string>();
            foreach (Humans human in humansList)
            {
                studentsList.Add(human.Name);
                studentsList.Add(human.Age);
                studentsList.Add(human.ID);
            }

            Chooser_and_editor chooser = new Chooser_and_editor();
            chooser.chooser(studentsList, humansList);
            break;
        }

        if (format == "l")
        {
            List<string> studentsList = new List<string>();
            List<Humans> humansList = new List<Humans>();
            XmlSerializer xml = new XmlSerializer(typeof(Humans));
            using (FileStream fs = new FileStream(path_start_file, FileMode.Open))
            {
                Humans human = (Humans)xml.Deserialize(fs);
                humansList.Add(human);
                studentsList.Add(human.Name);
                studentsList.Add(human.Age);
                studentsList.Add(human.ID);
            }


            Chooser_and_editor chooser = new Chooser_and_editor();
            chooser.chooser(studentsList, humansList);
            break;
        }
        else
        {
            Console.WriteLine("Чет не то вы ввели. Не обнаружен ни один из поддерживаемых форматов.");
        }
    }
}