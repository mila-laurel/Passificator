using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;
using Passificator.Dto;
using Passificator.Exceptions;
using System.Runtime.InteropServices;

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
            Application wordApplication = null;
            Document document = null;

            try
            {
                wordApplication = new Application { Visible = true };
                document = wordApplication.Documents.Add(_path);

                GenerateHeader(document, _context);
                GenerateAppeal(document, _context);
                GenerateReason(document, _context);

                for (int i = 0; i < _context.Guests.Count(); i++)
                {
                    if (i > 0)
                        document.Tables[2].Rows
                            .Add(document.Tables[2].Rows[2]);
                    FillTableRow(document, _context.Guests[i]);
                }

                GenerateSignature(document, _context);
            }
            catch (DocumentGeneratorException)
            {
                wordApplication.Quit(false);
                throw;
            }
            catch (COMException e)
            {
                throw new DocumentGeneratorException("Failed to generate document\r\nPlease try again later or contact your system administrator", e);
            }
        }

        private void GenerateHeader(Document document, NoteContextDto noteContextDto)
        {
            FillPlaceholders(document, "{ToWhom}", PutRightEnding(noteContextDto.AdresseePosition, "{ToWhom}") + " " + ShowInitialsAndLastName(noteContextDto.Adressee) + "у");
            FillPlaceholders(document, "{From}", PutRightEnding(noteContextDto.SenderPosition, "{From}") + " " + ShowInitialsAndLastName(noteContextDto.Sender) + "а");
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

        private void GenerateAppeal(Document document, NoteContextDto noteContextDto)
        {
            string[] splittedName = noteContextDto.Adressee.Split(' ');
            string content = splittedName[1] + " " + splittedName[2];
            FillPlaceholders(document, "{Appeal}", content);
        }

        private void GenerateReason(Document document, NoteContextDto noteContextDto)
        {
            FillPlaceholders(document, "{Reason}", noteContextDto.Reason);
        }

        private void GenerateSignature(Document document, NoteContextDto noteContextDto)
        {
            FillPlaceholders(document, "{SenderPosition}", noteContextDto.SenderPosition);
            FillPlaceholders(document, "{SenderName}", ShowInitialsAndLastName(noteContextDto.Sender));
        }

        private void FillPlaceholders(Document document, string placeholder, string content)
        {
            Range range = document.Content;
            range.Find.ClearFormatting();
            bool result = range.Find.Execute(FindText: placeholder);

            if (!result)
                throw new DocumentGeneratorException($"Document template {_path} is corrupt");

            range.Text = content;
        }

        public void FillTableRow(Document document, GuestDto guestDto)
        {

            Cell cell = document.Tables[2].Cell(2, 1);
            if (_context.SeveralDaysVisit)
                cell.Range.Text = _context.DateOfVisitFrom.Date.ToString("d") + "-" + _context.DateOfVisitTo.Date.ToString("d") + " " + _context.TimeOfVisit;
            else
                cell.Range.Text = _context.DateOfVisit.ToString("d") + " " + _context.TimeOfVisit; ;
            cell = document.Tables[2].Cell(2, 2);
            cell.Range.Text = guestDto.GuestName;
            cell = document.Tables[2].Cell(2, 3);
            if (_context.PersonAndDepartmentToVisit == _context.Sender)
                cell.Range.Text = ShowInitialsAndLastName(_context.PersonAndDepartmentToVisit) + ", " +
                                  _context.SenderDepartment;
            else
                cell.Range.Text = _context.PersonAndDepartmentToVisit;
            cell = document.Tables[2].Cell(2, 4);
            cell.Range.Text = guestDto.GuestCompany;
            cell = document.Tables[2].Cell(2, 5);
            cell.Range.Text = guestDto.GuestDocument;
            cell = document.Tables[2].Cell(2, 6);
            cell.Range.Text = guestDto.GuestCar;
            cell = document.Tables[2].Cell(2, 7);
            if (_context.Escort == _context.Sender)
                cell.Range.Text = ShowInitialsAndLastName(_context.Escort);
            else
                cell.Range.Text = _context.Escort;
        }
    }
}
