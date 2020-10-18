using System;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Dto;
using System.Runtime.Remoting.Messaging;
using Passificator.Utilities.Collections;
using System.Linq;

namespace Passificator
{
    public partial class Form1 : Form
    {
        private readonly TrackList<GuestViewModel> _people = new TrackList<GuestViewModel>();
        public Form1()
        {
            InitializeComponent();

            FillDropDownList(addresseeNameComboBox);
            FillDropDownList(senderNameComboBox);
        }

        private void addGuestButton_Click(object sender, EventArgs e)
        {
            var editorForm = new StaffEditorForm();

            editorForm.ShowDialog(this);

            FillDropDownList(addresseeNameComboBox);
            FillDropDownList(senderNameComboBox);
        }

        private void oneDayVisitRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (oneDayVisitRadioButton.Checked)
            {
                visitDatePicker.Enabled = true;
                visitDateFromPicker.Enabled = false;
                visitDateToPicker.Enabled = false;
            }
            else
            {
                visitDatePicker.Enabled = false;
                visitDateFromPicker.Enabled = true;
                visitDateToPicker.Enabled = true;
            }
        }

        private void FillDropDownList(ComboBox DropDownName)
        {
            DropDownName.Items.Clear();

            foreach (var administrator in StaffRepository.GetStaffList())
            {
                DropDownName.Items.Add(administrator);
            }
            DropDownName.ValueMember = "Id";
            DropDownName.DisplayMember = "Name";
        }

        private void addresseeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Staff chosen = (Staff)addresseeNameComboBox.SelectedItem;
            addresseePositionTextBox.Text = chosen.Position;
        }

        private void senderNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Staff chosen = (Staff)senderNameComboBox.SelectedItem;
            senderPositionTextBox.Text = chosen.Position;
            toWhomTextBox.Text = chosen.Name;
            escortTextBox.Text = chosen.Name;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            var context = GetNoteContext();
            var noteGenerator = new NoteGenerator(context);
            noteGenerator.Generate();
        }

        private NoteContextDTO GetNoteContext()
        {
            NoteContextDTO context = new NoteContextDTO();
            context.Adressee = ((Staff)addresseeNameComboBox.SelectedItem).Name.Split(' ');
            context.AdresseePosition = addresseePositionTextBox.Text;
            context.Sender = senderNameComboBox.Text + "а";
            context.SenderPosition = senderPositionTextBox.Text;
            context.DateOfVisit = visitDatePicker.Value;
            context.DateOfVisitFrom = visitDateFromPicker.Value;
            context.DateOfVisitTo = visitDateToPicker.Value;
            context.Escort = escortTextBox.Text;
            context.PersonAndDepartmentToVisit = toWhomTextBox.Text + ", ";
            // collect all required data and return dto
            return context;
        }

        private void addresseeNameComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string[] LFP = ((Staff)e.ListItem).Name.Split(' ');
            e.Value = LFP[1].Substring(0, 1) + "." + LFP[2].Substring(0, 1) + ". " + LFP[0];
        }

        private void senderNameComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string[] LFP = ((Staff)e.ListItem).Name.Split(' ');
            e.Value = LFP[1].Substring(0, 1) + "." + LFP[2].Substring(0, 1) + ". " + LFP[0];
        }

        private void UpdateData()
        {
            _people.Clear();

            var guests = from a in GuestRepository.GetGuestList()
                                 select new GuestViewModel() { Id = a.Id, Name = a.Name, Company = a.Company, Document = a.Document };

            foreach (var guest in guests.ToList())
            {
                guest.ResetDirty();
                _people.Add(guest);
            }

            guestsDataGrid.Columns[0].Visible = false;
            guestsDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _people.ClearChanges();
        }

        private class GuestViewModel
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

        private void guestNameComboBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
