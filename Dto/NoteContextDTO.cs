using Passificator.Model;
using System;
using System.Collections.Generic;

namespace Passificator.Dto
{
    internal class NoteContextDTO
    {
        public string Adressee { get; set; }
        public string AdresseePosition { get; set; }
        public string Sender { get; set; }
        public string SenderPosition { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime DateOfVisitFrom { get; set; }
        public DateTime DateOfVisitTo { get; set; }
        public string Reason { get; set; }
        public string PersonAndDepartmentToVisit { get; set; }
        public string Escort { get; set; }
        public List<GuestDto> Guests { get; set; }
    }
}
