namespace Passificator.Dto
{
    internal class GuestDto
    {
        public string GuestName { get; set; }
        public string GuestCompany { get; set; }
        public string GuestDocument { get; set; }
        public string? GuestCar { get; internal set; }
    }
}