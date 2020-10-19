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
            GenerateReason(wordApplication, _context);

            CreateTable(wordApplication, _context.Guests[0], 1);
            //for (int i = 1; i < _context.Guests.Count(); i++)
            //{
            //    CreateTable(wordApplication, _context.Guests[i], i);
            //}

            GenerateSignature(wordApplication, _context);
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
                range.Text = noteContextDTO.SenderPosition + " " + ShowInitialsAndLastName(noteContextDTO.Sender) + "а";
            else
                throw new Exception();
        }

        private string ShowInitialsAndLastName(string name)
        {
            string[] splittedName = name.Split(' ');
            return splittedName[1].First() + "." + splittedName[2].First() + ". " + splittedName[0];
        }

        private void GenerateAppeal(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{Appeal}"))
            {
                string[] splittedName = noteContextDTO.Adressee.Split(' ');
                range.Text = splittedName[1] + " " + splittedName[2];
            }
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

        private void GenerateSignature(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{SenderPosition}"))
                range.Text = noteContextDTO.SenderPosition;
            else
                throw new Exception();

            range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: "{SenderName}"))
                range.Text = ShowInitialsAndLastName(noteContextDTO.Sender);
            else
                throw new Exception();
        }

        public void CreateTable(Application wordApplication, GuestDto guestDto, int orderNumber)
        {
            wordApplication.ActiveDocument.Tables[2].Rows.Add(wordApplication.ActiveDocument.Tables[2].Rows[2]);
        }
    }
}
