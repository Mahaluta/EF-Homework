using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections;

namespace EFApp
{
    public class User
    {
        public static void SavePublisher()
        {
            using (LibEntities library = new LibEntities())
            {

                // Create Publisher
                Publisher p = new Publisher();
                Console.Write("Read the publisher's name: ");
                p.Name = Console.ReadLine();

                // Add Publisher to DB
                library.Publishers.Add(p);
                library.SaveChanges();
                Console.WriteLine("\n\t Publisher added to the DB.\n");

                // Write to .XML file
                Console.WriteLine("\t XML");
                using (FileStream stream = new FileStream("Publishers.xml", FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(Publisher));
                    XML.Serialize(stream, p);
                }
                Console.WriteLine("\n\t Publisher added to 'Publishers.xml'\n");

                // Write to .JSON file
                Console.WriteLine("\t JSON");
                string publisherResult = JsonConvert.SerializeObject(p);
                using (StreamWriter file = new StreamWriter(@"Publishers.json",true))
                {
                    file.WriteLine(publisherResult);
                    file.Flush();
                    file.Close();
                }
                Console.WriteLine("\n\t Publisher added to 'Publishers.json'\n");
                Console.WriteLine($"\n\t JSON-Test ==> {publisherResult}\n"); // test

            }
        }

        public static void UpdatePublisher()
        {


            using (LibEntities library = new LibEntities())
            {
                Console.Write("Read the publisher's ID in order to change his name: "); ;
                int PID = Convert.ToInt32(Console.ReadLine());
                var result = library.Publishers.SingleOrDefault(p => p.PublisherId == PID);
                if (result != null)
                {
                    Console.Write("Read the new publisher's name: ");
                    result.Name = Console.ReadLine();
                    library.SaveChanges();
                }
                else
                {
                    Console.WriteLine("The publisher could not be found");
                }
            }
        }

        public static void PrintPublishers()
        {
            using (LibEntities library = new LibEntities())
            {

                // AICI PRINTEZ TOT FISIERUL .XML

                using (StreamReader reader = File.OpenText(@"Publishers.xml"))
                {
                    var line = reader.ReadToEnd();
                    Console.WriteLine(line);
                }


                // AICI SE INTAMPLA CEVA DUBIOS CU FISIERUL .XML; CRED CA SE ADUGA LINIILE GRESIT, SAU
                // CEL MAI PROBABIL, NU POT DESERIALIZA INTR-O LISTA List<Publisher> MAI MULTI PUBLISHERI ADAUGATI IN ACELASI FISIER .XLM IN FELUL ACESTA.

                Console.WriteLine("\t XML Deserialization");
                using (FileStream stream = File.OpenRead(@"Publishers.xml")) // using (FileStream stream = new FileStream("Publishers.xml", FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Publisher));
                    var deserializer = serializer.Deserialize(stream) as List<Publisher>;

                    foreach (Publisher p in deserializer)
                    {
                        Console.WriteLine($"{p.PublisherId} - {p.Name}");
                    }

                }


                // MERGE FOLOSIND LINQ DIRECT PE LIBRARY

                var converter = from pub in library.Publishers select pub;
                var publishers = converter.ToList();

                foreach (Publisher p in publishers)
                {
                    Console.WriteLine($"{p.PublisherId} - {p.Name}");
                }



            }

        }

        public static void PrintPublisherWithJSON()
        {
            Console.WriteLine("\t JSON Deserialization");
            string deserializer = String.Empty;
            deserializer = File.ReadAllText(@"Publishers.json");
            List<Publisher> resultPublisher = JsonConvert.DeserializeObject<List<Publisher>>(deserializer);

            foreach (Publisher p in resultPublisher)
            {
                Console.WriteLine($"{p.ToString()}\n");
            }
        }

        public static void PrintPublisherWithXML()
        {
            Console.WriteLine("\t XML Deserialization");
            using (FileStream stream = File.OpenRead(@"Publishers.xml"))
            {
                var serializer = new XmlSerializer(typeof(Publisher));
                var deserializer = serializer.Deserialize(stream);
                Console.WriteLine($"{deserializer.ToString()}\n");
            }
        }
    }
}
