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
                if (i > 0)
                    wordApplication.ActiveDocument.Tables[2].Rows.Add(wordApplication.ActiveDocument.Tables[2].Rows[2]);
                FillTableRow(wordApplication, _context.Guests[i]);
            }

            GenerateSignature(wordApplication, _context);
        }

        private void GenerateHeader(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            FillPlaceholders(wordApplication, "{ToWhom}", PutRightEnding(noteContextDTO.AdresseePosition, "{ToWhom}") + " " + ShowInitialsAndLastName(noteContextDTO.Adressee) + "у");
            FillPlaceholders(wordApplication, "{From}", PutRightEnding(noteContextDTO.SenderPosition, "{From}") + " " + ShowInitialsAndLastName(noteContextDTO.Sender) + "а");
        }
        private string PutRightEnding(string positon, string placeholder)
        {
            string ending;
            string[] splittedPosition = positon.Split(' ');
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
            return splittedName[1].First() + "." + splittedName[2].First() + ". " + splittedName[0];
        }

        private void GenerateAppeal(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            string[] splittedName = noteContextDTO.Adressee.Split(' ');
            string content = splittedName[1] + " " + splittedName[2];
            FillPlaceholders(wordApplication, "{Appeal}", content);
        }

        private void GenerateReason(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            FillPlaceholders(wordApplication, "{Reason}", noteContextDTO.Reason);
        }

        private void GenerateSignature(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            FillPlaceholders(wordApplication, "{SenderPosition}", noteContextDTO.SenderPosition);
            FillPlaceholders(wordApplication, "{SenderName}", ShowInitialsAndLastName(noteContextDTO.Sender));
        }

        private void FillPlaceholders(Application wordApplication, string placeholder, string content)
        {
            Range range = wordApplication.ActiveDocument.Content;
            range.Find.ClearFormatting();
            if (range.Find.Execute(FindText: placeholder))
                range.Text = content;
            else
                throw new Exception();
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
            cell.Range.Text = ShowInitialsAndLastName(_context.PersonAndDepartmentToVisit) + ", " + _context.SenderDepartment;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 4);
            cell.Range.Text = guestDto.GuestCompany;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 5);
            cell.Range.Text = guestDto.GuestDocument;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 6);
            cell.Range.Text = guestDto.GuestCar;
            cell = wordApplication.ActiveDocument.Tables[2].Cell(2, 7);
            cell.Range.Text = ShowInitialsAndLastName(_context.Escort);
        }
    }
}
