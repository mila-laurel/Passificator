using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;
using Passificator.Dto;

namespace Passificator
{
    class NoteGenerator
    {
        private readonly string _path = Path.GetFullPath("template.dot");

        private readonly NoteContextDto _context;

        public NoteGenerator(NoteContextDto noteContextDto)
        {
            _context = noteContextDto;
        }

        public void Generate()
        {
            var wordApplication = new Application { Visible = true };
            wordApplication.Documents.Add(_path);

            GenerateHeader(wordApplication, _context);
            GenerateAppeal(wordApplication, _context);
            GenerateReason(wordApplication, _context);

            for (int i = 0; i < _context.Guests.Count(); i++)
            {
                if (i > 0)
                    wordApplication.ActiveDocument.Tables[2].Rows.Add(wordApplication.ActiveDocument.Tables[2].Rows[2]);
                FillTableRow(wordApplication, _context.Guests[i]);
            }

            GenerateSignature(wordApplication, _context);
        }

        private void GenerateHeader(Application wordApplication, NoteContextDto noteContextDto)
        {
            FillPlaceholders(wordApplication, "{ToWhom}", PutRightEnding(noteContextDto.AdresseePosition, "{ToWhom}") + " " + ShowInitialsAndLastName(noteContextDto.Adressee) + "у");
            FillPlaceholders(wordApplication, "{From}", PutRightEnding(noteContextDto.SenderPosition, "{From}") + " " + ShowInitialsAndLastName(noteContextDto.Sender) + "а");
        }
        private string PutRightEnding(string position, string placeholder)
        {
            string ending;
            string[] splittedPosition = position.Split(' ');
            if (splittedPosition[0].EndsWith("ь"))
            {
                ending = (placeholder == "{ToWhom}") ? "ю" : "я";
                return splittedPosition[0].TrimEnd('ь') + ending + " " + String.Join(" ", splittedPosition.Skip(1));
            }
            else
            {
                ending = (placeholder == "{ToWhom}") ? "у" : "а";
                return splittedPosition[0] + ending + " " + String.Join(" ", splittedPosition.Skip(1));
            }
        }

        private string ShowInitialsAndLastName(string name)
        {
            string[] splittedName = name.Split(' ');
            string result;
            if (splittedName.Length == 3)
                result = splittedName[1].First() + "." + splittedName[2].First() + ". " + splittedName[0];
            else
                result = name;
            return result;
        }

        private void GenerateAppeal(Application wordApplication, NoteContextDto noteContextDto)
        {
            string[] splittedName = noteContextDto.Adressee.Split(' ');
            string content = splittedName[1] + " " + splittedName[2];
            FillPlaceholders(wordApplication, "{Appeal}", content);
        }

        private void GenerateReason(Application wordApplication, NoteContextDto noteContextDto)
        {
            FillPlaceholders(wordApplication, "{Reason}", noteContextDto.Reason);
        }

        private void GenerateSignature(Application wordApplication, NoteContextDto noteContextDto)
        {
            FillPlaceholders(wordApplication, "{SenderPosition}", noteContextDto.SenderPosition);
            FillPlaceholders(wordApplication, "{SenderName}", ShowInitialsAndLastName(noteContextDto.Sender));
        }

        private void FillPlaceholders(Application wordApplication, string placeholder, string content)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            try
            {
                range.Find.Execute(FindText: placeholder);
                range.Text = content;
            }
            catch (Exception e)
            {
                Console.WriteLine("There is now such placeholder in the file");
                throw;
            }
        }

        public void FillTableRow(Application wordApplication, GuestDto guestDto)
        {

            Cell cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 1);
            if (_context.SeveralDaysVisit)
                cell.Range.Text = _context.DateOfVisitFrom.Date.ToString("d") + "-" + _context.DateOfVisitTo.Date.ToString("d") + " " + _context.TimeOfVisit;
            else
                cell.Range.Text = _context.DateOfVisit.ToString("d") + " " + _context.TimeOfVisit; ;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 2);
            cell.Range.Text = guestDto.GuestName;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 3);
            if (_context.PersonAndDepartmentToVisit == _context.Adressee)
                cell.Range.Text = ShowInitialsAndLastName(_context.PersonAndDepartmentToVisit) + ", " +
                                  _context.SenderDepartment;
            else
                cell.Range.Text = _context.PersonAndDepartmentToVisit;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 4);
            cell.Range.Text = guestDto.GuestCompany;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 5);
            cell.Range.Text = guestDto.GuestDocument;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 6);
            cell.Range.Text = guestDto.GuestCar;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 7);
            if (_context.Escort == _context.Adressee)
                cell.Range.Text = ShowInitialsAndLastName(_context.Escort);
            else
                cell.Range.Text = _context.Escort;
        }
    }
}
