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

        private void GenerateGuestInformation(Application wordApplication, NoteContextDTO noteContextDTO, GuestDto guestDto)
        {
            FillPlaceholders(wordApplication, "{LastName}", guestDto.GuestName.Split(' ')[0]);
            FillPlaceholders(wordApplication, "{Name}", guestDto.GuestName.Split(' ')[1]);
            FillPlaceholders(wordApplication, "{Patronymic}", guestDto.GuestName.Split(' ')[2]);
            FillPlaceholders(wordApplication, "{Document}", guestDto.GuestDocument);
            FillPlaceholders(wordApplication, "{PersonToVisit}", noteContextDTO.PersonAndDepartmentToVisit);
            FillPlaceholders(wordApplication, "{DepartmentToVisit}", noteContextDTO.SenderDepartment);
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

        private void GenerateDates(Application wordApplication, NoteContextDTO noteContextDTO)
        {
            FillPlaceholders(wordApplication, "{DateFrom}", noteContextDTO.SeveralDaysVisit ? noteContextDTO.DateOfVisitFrom.ToString("d") : noteContextDTO.DateOfVisit.ToString("d"));
            FillPlaceholders(wordApplication, "{DateTo}", noteContextDTO.SeveralDaysVisit ? noteContextDTO.DateOfVisitTo.ToString("d") : noteContextDTO.DateOfVisit.ToString("d"));
        }

        private void FillPlaceholders(Application wordApplication, string placeholder, string content)
        {
            Range range = wordApplication.ActiveDocument.Tables[wordApplication.ActiveDocument.Tables.Count].Range;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: placeholder, ReplaceWith: content, Replace: WdReplace.wdReplaceAll);
        }
    }
}
