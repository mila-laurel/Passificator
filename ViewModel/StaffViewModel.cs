namespace Passificator.ViewModel
{
    class StaffViewModel
    {
        private bool _isDirty;
        private int _id = -1;
        private string _name;
        private string _position;

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

        public bool IsDirty() => _isDirty;
        public void ResetDirty() => _isDirty = false;
    }
}
