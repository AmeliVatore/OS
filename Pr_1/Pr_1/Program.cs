using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Pr_1
{
    class Program
    {
        static void Zadaniye_1_Info()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("\nНазвание: " + drive.Name);
                Console.WriteLine("Тип: " + drive.DriveType);
                if (drive.IsReady)
                {
                    Console.WriteLine("Объём диска: " + drive.TotalSize);
                    Console.WriteLine("Свободное пространство: " + drive.TotalFreeSpace);
                    Console.WriteLine("Метка: " + drive.VolumeLabel);
                }
            }
        }

        static void Zadaniye_2_Files()
        {
            string path = @"d:\";
            int choice = 0;
            for (; ; )
            {
                do
                {
                    Console.WriteLine("\nВыберите действие:\n 1. Создать файл\n 2. Записать в файл строку\n 3. Прочитать файл в консоль\n 4. Удалить файл\n 5. Выход");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 5);
                switch (choice)
                {
                    case 1:
                        z_2_CreateFile(path);
                        break;
                    case 2:
                        z_2_WriteToFile(path);
                        break;
                    case 3:
                        z_2_ReadFile(path);
                        break;
                    case 4:
                        z_2_DeleteFile(path);
                        goto FilesExit;
                    case 5:
                        goto FilesExit;
                }
            }
            FilesExit:;
        }

        static void z_2_CreateFile(string path)
        {
            Console.WriteLine("\nВведите имя файла");
            string filename = Console.ReadLine();
            FileInfo fileInfo = new FileInfo($"{path}\\{filename}.txt");
            if (fileInfo.Exists)
            {
                Console.WriteLine("\nФайл с таким именем уже существует");
            }
            else
                using (FileStream fstream = new FileStream($"{path}\\{filename}.txt", FileMode.OpenOrCreate))
                {
                    Console.WriteLine("\nФайл создан");
                }
        }

        static void z_2_WriteToFile(string path)
        {
            Console.WriteLine("\nВведите имя файла");
            string filename = Console.ReadLine();
            FileInfo fileInfo = new FileInfo($"{path}\\{filename}.txt");
            if (fileInfo.Exists)
            {
                Console.WriteLine("\nВведите строку для записи в файл");
                string text = Console.ReadLine();

                StreamWriter writer = new StreamWriter($"{path}\\{filename}.txt", true, System.Text.Encoding.Default);
                writer.WriteLine(text);
                writer.Close();
            }
            else
                Console.WriteLine("\nФайл с таким именем не существует");
        }

        static void z_2_ReadFile(string path)
        {
            Console.WriteLine("\nВведите имя файла");
            string filename = Console.ReadLine();
            FileInfo fileInfo = new FileInfo($"{path}\\{filename}.txt");
            if (fileInfo.Exists)
            {
                StreamReader reader = new StreamReader($"{path}\\{filename}.txt", System.Text.Encoding.Default);
                Console.WriteLine("\nТекст из файла:\n" + reader.ReadToEnd());
                reader.Close();
            }
            else
                Console.WriteLine("\nФайл с таким именем не существует");
        }

        static void z_2_DeleteFile(string path)
        {
            Console.WriteLine("\nВведите имя файла");
            string filename = Console.ReadLine();
            FileInfo fileInfo = new FileInfo($"{path}\\{filename}.txt");
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            else
                Console.WriteLine("\nФайл с таким именем не существует");
        }

        static async Task Zadaniye_3_JSON()
        {
            string path = @"D:\VSrep\OS\Pr_1\Pr_1\user.json";
            for (; ; )
            {
                int choice = 0;
                do
                {
                    Console.WriteLine("\nВыберите действие:\n 1. Выполнить сериализацию объекта в формате JSON и записать в файл\n 2. Прочитать файл в консоль\n 3. Удалить файл\n 4. Выход\n");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 4);
                switch (choice)
                {
                    case 1:
                        await z_3_WriteToJSON(path);
                        break;
                    case 2:
                        await z_3_ReadJSON(path);
                        break;
                    case 3:
                        z_3_DeleteJSON(path);
                        goto JSON_Exit;
                    case 4:
                        goto JSON_Exit;
                }
            }
            JSON_Exit:;
        }

        static async Task z_3_WriteToJSON(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Person person = new Person();
                Console.WriteLine("\nВведите имя");
                person.Name = Console.ReadLine();
                Console.WriteLine("Введите возраст");
                person.Age = Convert.ToInt32(Console.ReadLine());

                await JsonSerializer.SerializeAsync<Person>(fs, person);
            }
        }

        static async Task z_3_ReadJSON(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Person person = await JsonSerializer.DeserializeAsync<Person>(fs);
                    Console.WriteLine($"\nДанные из файла: \nName: {person.Name}  Age: {person.Age}");
                }
            }
            else
                Console.WriteLine("\nФайл не существует");
        }

        static void z_3_DeleteJSON(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                File.Delete(path);
            }
            else
                Console.WriteLine("\nФайл не существует");
        }

        static void Zadaniye_4_XML()
        {
            string path = @"D:/VSrep\OS\Pr_1\Pr_1/users.xml";

            for (; ; )
            {
                int choice = 0;
                do
                {
                    Console.WriteLine("\nВыберите действие:\n 1. Записать в файл новые данные из консоли\n 2. Прочитать файл в консоль\n 3. Удалить файл\n 4. Выход");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 4);
                switch (choice)
                {
                    case 1:
                        z_4_WriteToXML(path);
                        break;
                    case 2:
                        z_4_ReadXML(path);
                        break;
                    case 3:
                        z_4_DeleteXML(path);
                        goto XML_Exit;
                    case 4:
                        goto XML_Exit;
                }
            }
            XML_Exit:;
        }

        static void z_4_WriteToXML(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement userElem = xDoc.CreateElement("user");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlElement companyElem = xDoc.CreateElement("company");
            XmlElement ageElem = xDoc.CreateElement("age");


            Console.WriteLine("\nВведите имя основателя компании");
            string username = Console.ReadLine();
            Console.WriteLine("Введите название компании");
            string userCompany = Console.ReadLine();
            Console.WriteLine("Введите возраст основателя компании");
            string userAge = (Console.ReadLine());
            XmlText nameText = xDoc.CreateTextNode(username);
            XmlText companyText = xDoc.CreateTextNode(userCompany);
            XmlText ageText = xDoc.CreateTextNode(userAge);

            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            ageElem.AppendChild(ageText);
            userElem.Attributes.Append(nameAttr);
            userElem.AppendChild(companyElem);
            userElem.AppendChild(ageElem);
            xRoot.AppendChild(userElem);

            xRoot.AppendChild(userElem);
            xDoc.Save(path);
            Console.WriteLine("\nЗапись добавлена");
        }

        static void z_4_ReadXML(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);

                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode xnode in xRoot)
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("name");
                        if (attr != null)
                            Console.WriteLine("\n" + attr.Value);
                    }
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "company")
                        {
                            Console.WriteLine($"Компания: {childnode.InnerText}");
                        }
                        if (childnode.Name == "age")
                        {
                            Console.WriteLine($"Возраст: {childnode.InnerText}");
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("\nФайл не существует");
        }

        static void z_4_DeleteXML(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                File.Delete(path);
            }
            else
                Console.WriteLine("\nФайл не существует");
        }

        static void Zadaniye_5_zip()
        {
            int choice = 0;
            for (; ; )
            {
                do
                {
                    Console.WriteLine("\nВыберите действие:\n 1. Создать архив в формате zip\n 2. Добавить файл в архив\n 3. Разархивировать файл и вывести данные о нем\n 4. Удалить архив\n 5. Выход");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 5);
                switch (choice)
                {
                    case 1:
                        z_5_CreateZip();
                        break;
                    case 2:
                        z_5_AddToZip();
                        break;
                    case 3:
                        z_5_OpenZip();
                        break;
                    case 4:
                        z_5_DeleteZip();
                        goto ZipExit;
                    case 5:
                        goto ZipExit;
                }
            }
            ZipExit:;
        }

        static void z_5_CreateZip()
        {
            Console.WriteLine("\nВведите имя папки, которую нужно заархивировать");
            string foldername = Console.ReadLine();
            string folderPath = $"D://{foldername}/";

            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            Console.WriteLine("\nВведите название архива");
            string zipname = Console.ReadLine();
            string zipPath = $"D://{ zipname}.zip";

            FileInfo zipFileInfo = new FileInfo(zipPath);
            if (zipFileInfo.Exists)
            {
                Console.WriteLine("\nАрхив с таким именем уже существует");
            }
            else
            {
                ZipFile.CreateFromDirectory(folderPath, zipPath);
                Console.WriteLine($"\nПапка {folderPath} архивирована в файл {zipPath}");
            }
        }

        static void z_5_AddToZip()
        {
            Console.WriteLine("\nВведите имя файла");
            string filename = Console.ReadLine();
            string filePath = $"D://{filename}.txt";

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }

            Console.WriteLine("\nВведите название архива");
            string zipname = Console.ReadLine();
            string zipPath = $"D://{ zipname}.zip";
            FileInfo zipInfo = new FileInfo(zipPath);
            if (zipInfo.Exists)
            {
                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        archive.CreateEntry($"{filename}");
                        Console.WriteLine($"\nФайл {filePath} добавлен в архив {zipPath}");
                    }
                }
            }
            else
                Console.WriteLine("\nАрхив с таким именем не найден");
        }

        static void z_5_OpenZip()
        {
            Console.WriteLine("\nВведите имя папки для разархивации");
            string foldername = Console.ReadLine();
            string folderPath = $"D://{foldername}/";

            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            Console.WriteLine("\nВведите название архива");
            string zipname = Console.ReadLine();
            string zipPath = $"D://{ zipname}.zip";

            FileInfo zipFileInfo = new FileInfo(zipPath);
            if (zipFileInfo.Exists)
            {
                Console.WriteLine("\nИмя файла: {0}", zipFileInfo.Name);
                Console.WriteLine("Время создания: {0}", zipFileInfo.CreationTime);
                Console.WriteLine("Тип файла: {0}", zipFileInfo.Extension);
                Console.WriteLine("Размер: {0}", zipFileInfo.Length);

                ZipFile.ExtractToDirectory(zipPath, folderPath);
                Console.WriteLine($"\nФайл {zipPath} распакован в папку {folderPath}");
            }
            else
                Console.WriteLine("\nАрхив с таким именем не найден");
        }

        static void z_5_DeleteZip()
        {
            Console.WriteLine("\nВведите название архива");
            string zipname = Console.ReadLine();
            string zipPath = $"D://{ zipname}.zip";
            FileInfo fileInfo = new FileInfo(zipPath);
            if (fileInfo.Exists)
            {
                File.Delete(zipPath);
            }
            else
                Console.WriteLine("\nФайл не существует");
        }

        static async Task Main(string[] args)
        {
            for (; ; )
            {
                int choice = 0;
                do
                {
                    Console.WriteLine("\nВыберите действие:\n 1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.\n 2. Работа с файлами\n 3. Работа с форматом JSON\n 4. Работа с форматом XML\n 5. Создание zip архива, добавление туда файла, определение размера архива\n 6. Выход");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice < 1 || choice > 6);
                switch (choice)
                {
                    case 1:
                        Zadaniye_1_Info();
                        break;
                    case 2:
                        Zadaniye_2_Files();
                        break;
                    case 3:
                        Zadaniye_3_JSON();
                        break;
                    case 4:
                        Zadaniye_4_XML();
                        break;
                    case 5:
                        Zadaniye_5_zip();
                        break;
                    case 6:
                        goto Exit;
                }
            }
            Exit:;
        }
    }
}
