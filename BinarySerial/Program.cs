using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace BinarySerial
{
    [Serializable]
    class AgeCal : IDeserializationCallback
    {
        public int Pyear { get; set; }
        public int Byear { get; set; }
        [NonSerialized]
        public int Agecal;

        public void OnDeserialization(object a)
        {
            Agecal= Pyear - Byear;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AgeCal ac = new AgeCal();
            ac.Pyear = DateTime.Now.Year;
            Console.WriteLine("Enter Your Year of Birth:");
            ac.Byear = Convert.ToInt32(Console.ReadLine());
            FileStream fs = new FileStream(@"DOB.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fs, ac);
            fs.Seek(0, SeekOrigin.Begin);
            AgeCal ress = (AgeCal)b.Deserialize(fs);
            Console.WriteLine($"The Present Age is:{ress.Agecal}");
        }
    }
}
