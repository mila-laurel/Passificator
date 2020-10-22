using System;
using System.IO;
using System.Linq;
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

            for (int i = 0; i < _context.Guests.Count(); i++)
            {
                CreateTable(wordApplication, _context.Guests[i], i + 2);
            }

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
            wordApplication.ActiveDocument.Tables[2].Rows.Add(wordApplication.ActiveDocument.Tables[2].Rows[orderNumber]);
            Cell cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 1);
            if (_context.SeveralDaysVisit)
                cell.Range.Text = _context.DateOfVisitFrom.Date.ToString("d") + "-" + _context.DateOfVisitTo.Date.ToString("d") + " " + _context.TimeOfVisit;
            else
                cell.Range.Text = _context.DateOfVisit.ToString("d") + " " + _context.TimeOfVisit; ;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 2);
            cell.Range.Text = guestDto.GuestName;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 3);
            cell.Range.Text = ShowInitialsAndLastName(_context.PersonAndDepartmentToVisit) + ", " + _context.SenderDepartment;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 4);
            cell.Range.Text = guestDto.GuestCompany;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 5);
            cell.Range.Text = guestDto.GuestDocument;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 6);
            cell.Range.Text = guestDto.GuestCar;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(orderNumber, 7);
            cell.Range.Text = ShowInitialsAndLastName(_context.Escort);
        }
    }
}
