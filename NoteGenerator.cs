using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Passificator.Dto;

namespace Passificator
{
    class NoteGenerator
    {
        private readonly string path = Path.GetFullPath("template.dot");

        public NoteGenerator(NoteContextDTO noteContextDTO)
        {
            Application temple = new Application();
            temple.Documents.Add(path);
        }

        public void Generate()
        {

        }

        public void CreateTable(GuestDto guestDto)
        {

        }
    }
}
