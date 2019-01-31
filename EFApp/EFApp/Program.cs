using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

namespace EFApp
{
    class Program
    {
        static void Main(string[] args)
        {
            User.PrintPublisherWithJSON();
            User.SavePublisher(); // Add to .XML file and to .JSON file
            User.PrintPublisherWithXML(); // Just the last one added

            // User.PrintPublishers(); // Nu merge chiar tot ce e in metoda aceasta
            //User.UpdatePublisher();

            Console.ReadKey();

             


        }
    }
}
