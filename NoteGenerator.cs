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

        private readonly NoteContextDTO _context;

        public NoteGenerator(NoteContextDTO noteContextDTO)
        {
            _context = noteContextDTO;
        }

        public void Generate()
        {
            var wordApplication = new Application { Visible = true };
            wordApplication.Documents.Add(path);
            
            GenerateHeader(wordApplication, _context);
            GenerateAppeal(wordApplication, _context);
        }

        private void GenerateHeader(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{ToWhom}"))
                range.Text = noteContextDTO.AdresseePosition + " " + ShowInitialsAndLastName(noteContextDTO.Adressee) + "у";
            else
                throw new Exception();

            range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{From}"))
                range.Text = noteContextDTO.SenderPosition + " " + noteContextDTO.Sender;
            else
                throw new Exception();
        }

        private string ShowInitialsAndLastName(string[] name)
        {
            return name[1].First() + "." + name[2].First() + ". " + name[0];
        }
        
        private void GenerateAppeal(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{Appeal}"))
                range.Text = noteContextDTO.Adressee[1] + " " + noteContextDTO.Adressee[2];
            else
                throw new Exception();
        }

        private void GenerateReason(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{Reason}"))
                range.Text = noteContextDTO.Reason;
            else
                throw new Exception();
        }

        public void CreateTable(GuestDto guestDto)
        {

        }
    }
}
