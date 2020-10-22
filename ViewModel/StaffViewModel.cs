namespace Passificator.ViewModel
{
    class StaffViewModel
    {
        private bool _isDirty;
        private int _id = -1;
        private string _name;
        private string _position;
        private string _department;

        public int Id
        {
            get => _id;
            set
            {
                _isDirty = true;
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _isDirty = true;
                _name = value;
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _isDirty = true;
                _position = value;
            }
        }

        public string Department
        {
            get => _department;
            set
            {
                _isDirty = true;
                _department = value;
            }
        }

        public bool IsDirty() => _isDirty;
        public void ResetDirty() => _isDirty = false;
    }
}
