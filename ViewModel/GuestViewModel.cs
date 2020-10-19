﻿namespace Passificator.ViewModel
{
    class GuestViewModel
    {
        private bool _isDirty;
        private int _id = -1;
        private string _name;
        private string _company;
        private string _document;

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

        public bool IsDirty() => _isDirty;
        public void ResetDirty() => _isDirty = false;
    }
}
