using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exml01();
            // Exml02();
            // Exml03();
            Exml04();
        }
        /// <summary>
        /// бинарная сериализация
        /// </summary>
        public static void Exml01()
        {
            //создадим объект сериализации
            Person person = new Person("Tom", 23);
            Console.WriteLine("объект создан");

            //создадим объект binaryFormatter - сериализатор
            BinaryFormatter formatter = new BinaryFormatter();

            //создаем поток, куда будем записывать объект сериализации
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);
                Console.WriteLine("объект сериализирован");
            }

            Person personDes = new Person();
            using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                var result = formatter.Deserialize(fs);
                //formatter.Deserialize(fs);
                personDes = (Person)result;
                Console.WriteLine("объект сериализирован");
                Console.WriteLine("Name: " + personDes.Name);
                Console.WriteLine("Age: " + personDes.Year);

            }

        }
        /// <summary>
        /// soap
        /// </summary>
        public static void Exml02()
        {
            //создадим объект сериализации
            Person person = new Person("Tom", 23);
            Console.WriteLine("объект создан");

            //создадим объект Soap - сериализатор
            SoapFormatter soap = new SoapFormatter();

            //создаем поток, куда будем записывать объект сериализации
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                soap.Serialize(fs, person);
                Console.WriteLine("объект сериализирован");
            }

            Person personDes = new Person();
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                var result = soap.Deserialize(fs);
                //formatter.Deserialize(fs);
                personDes = (Person)result;
                Console.WriteLine("объект сериализирован");
                Console.WriteLine("Name: " + personDes.Name);
                Console.WriteLine("Age: " + personDes.Year);

            }

        }

        public static void Exml03()
        {
            //создадим объект сериализации
            Person person = new Person("Tom", 23);
            Console.WriteLine("объект создан");

            //создадим объект xml - сериализатор
            XmlSerializer formatter = new XmlSerializer(typeof(Person));

            //создаем поток, куда будем записывать объект сериализации
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);
                Console.WriteLine("объект сериализирован");
            }

            Person personDes = new Person();
            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                var result = formatter.Deserialize(fs);
                //formatter.Deserialize(fs);
                personDes = (Person)result;
                Console.WriteLine("объект сериализирован");
                Console.WriteLine("Name: " + personDes.Name);
                Console.WriteLine("Age: " + personDes.Year);

            }

        }

        public static void Exml04()
        {
            using (WebClient client = new WebClient())
            {
                string url = "https://api.randomuser.me/?results=1";
                string json = client.DownloadString(url);
                var data = JsonConvert.DeserializeObject<randomUser>(json);

                //if (json!=null)
            }
        }
    }

    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public Person(string Name, int Year)
        {
            this.Name = Name;
            this.Year = Year;
        }
        public Person() { }
    }

    public class randomUser
    {
        public List<results> results = new List<results>();
    }
    public class results
    {
        public string gender { get; set; }
        public name name { get; set; }
        public login login { get; set; }
        public location location { get; set; }
        public string email { get; set; }
        public timezone timezone { get; set; }
    }
    public class name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class location
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
    }

    public class login
    {
        public string uuid { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }

    public class timezone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }
}

