namespace Passificator.ViewModel
{
    class GuestViewModel
    {
        private bool _isDirty;
        private string _name;
        private string _company;
        private string _document;
        private string _car;

        public int Id { get; set; } = -1;

        public string Name
        {
            get => _name;
            set
            {
                _isDirty = true;
                _name = value;
            }
        }

        public string Company
        {
            get => _company;
            set
            {
                _isDirty = true;
                _company = value;
            }
        }

        public string Document
        {
            get => _document;
            set
            {
                _isDirty = true;
                _document = value;
            }
        }

        public string Car 
        {
            get => _car;
            set
            {
                _isDirty = true;
                _car = value;
            }
        }

        public bool IsDirty() => _isDirty;
        public void ResetDirty() => _isDirty = false;
    }
}
