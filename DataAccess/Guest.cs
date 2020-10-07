using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Passificator
{
    [Serializable]
    class Guest
    {
        public string FIO { get; set; }
       
        public string Company { get; set; }
        public string Document { get; set; }
        public string DocumentNumber { get; set; }
        public string CarInformation { get; set; }
        public Guest()
        {
            LastName = "";
        }

        public void Save(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream output = File.OpenWrite(path))
            {
                formatter.Serialize(output, this);
            }
        }

        public void OpenFile(string path)
        {
            //this.LastName = lastName;
            BinaryFormatter formatter = new BinaryFormatter();
            Guest guest;
            using (Stream input = File.OpenRead(path))
            {
                guest = (Guest)formatter.Deserialize(input);
            }
            Name = guest.Name;
            Patronymic = guest.Patronymic;
            Company = guest.Company;
            Document = guest.Document;
            DocumentNumber = guest.DocumentNumber;
            CarInformation = guest.CarInformation;
        }
    }
}
