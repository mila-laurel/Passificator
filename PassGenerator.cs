using Microsoft.Office.Interop.Word;
using Passificator.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passificator
{
    class PassGenerator
    {
        private readonly string path = Path.GetFullPath("templatePass.dot");

        private readonly NoteContextDTO _context;

        public PassGenerator(NoteContextDTO noteContextDTO)
        {
            _context = noteContextDTO;
        }

        public void Generate()
        {
            var wordApplication = new Application { Visible = true };
            wordApplication.Documents.Add(path);
            GenerateDates(wordApplication, _context);
            GenerateGuestInformation(wordApplication, _context, _context.Guests[0]);
            for (int i = 1; i < _context.Guests.Count() - 1; i++)
            {
                CreateTable(wordApplication, _context.Guests[i], i + 2);
                GenerateDates(wordApplication, _context);
                GenerateGuestInformation(wordApplication, _context, _context.Guests[i]);
            }
        }

        private void CreateTable(Application wordApplication, GuestDto guestDto, int v)
        {
            throw new NotImplementedException();
        }

        private void GenerateGuestInformation(Application wordApplication, NoteContextDTO context, GuestDto guestDto)
        {
            throw new NotImplementedException();
        }

        private void GenerateDates(Application wordApplication, NoteContextDTO context)
        {
            throw new NotImplementedException();
        }
    }
}
