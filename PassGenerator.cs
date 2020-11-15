using Microsoft.Office.Interop.Word;
using Passificator.Dto;
using System.IO;
using System.Linq;

namespace Passificator
{
    class PassGenerator
    {
        private readonly string _path = Path.GetFullPath("templatePass.dot");

        private readonly NoteContextDto _context;

        public PassGenerator(NoteContextDto noteContextDto)
        {
            _context = noteContextDto;
        }

        public void Generate()
        {
            var wordApplication = new Application { Visible = true };
            wordApplication.Documents.Add(_path);
            Range table = wordApplication.ActiveDocument.Tables[wordApplication.ActiveDocument.Tables.Count].Range;
            table.Copy();
            for (int i = 0; i < _context.Guests.Count(); i++)
            {
                if (i > 0)
                    CreateTable(table);
                GenerateDates(wordApplication, _context);
                GenerateGuestInformation(wordApplication, _context, _context.Guests[i]);
            }
        }

        private void CreateTable(Range table)
        {
            table.Collapse(WdCollapseDirection.wdCollapseEnd);
            table.Paste();
        }

        private void GenerateGuestInformation(Application wordApplication, NoteContextDto noteContextDto, GuestDto guestDto)
        {
            FillPlaceholders(wordApplication, "{LastName}", guestDto.GuestName.Split(' ')[0]);
            FillPlaceholders(wordApplication, "{Name}", guestDto.GuestName.Split(' ')[1]);
            FillPlaceholders(wordApplication, "{Patronymic}", guestDto.GuestName.Split(' ')[2]);
            FillPlaceholders(wordApplication, "{Document}", guestDto.GuestDocument);
            FillPlaceholders(wordApplication, "{PersonToVisit}", noteContextDto.PersonAndDepartmentToVisit);
            FillPlaceholders(wordApplication, "{DepartmentToVisit}", noteContextDto.SenderDepartment);
            if (guestDto.GuestCar.Split(' ').Length > 1 && guestDto.GuestCar.Split(' ')[0].Length > 3)
            {
                FillPlaceholders(wordApplication, "{Car}", guestDto.GuestCar.Split(' ')[0]);
                FillPlaceholders(wordApplication, "{CarNumber}", guestDto.GuestCar.Substring(guestDto.GuestCar.IndexOf(' ')));
            }
            else
            {
                FillPlaceholders(wordApplication, "{Car}", guestDto.GuestCar);
                FillPlaceholders(wordApplication, "{CarNumber}", "");
            }

        }

        private void GenerateDates(Application wordApplication, NoteContextDto noteContextDto)
        {
            FillPlaceholders(wordApplication, "{DateFrom}", noteContextDto.SeveralDaysVisit ? noteContextDto.DateOfVisitFrom.ToString("d") : noteContextDto.DateOfVisit.ToString("d"));
            FillPlaceholders(wordApplication, "{DateTo}", noteContextDto.SeveralDaysVisit ? noteContextDto.DateOfVisitTo.ToString("d") : noteContextDto.DateOfVisit.ToString("d"));
        }

        private void FillPlaceholders(Application wordApplication, string placeholder, string content)
        {
            Range range = wordApplication.ActiveDocument.Tables[wordApplication.ActiveDocument.Tables.Count].Range;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: placeholder, ReplaceWith: content, Replace: WdReplace.wdReplaceAll);
        }
    }
}
