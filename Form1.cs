using System;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Dto;
using Passificator.ViewModel;
using Passificator.Utilities.Collections;
using System.Linq;
using System.Collections.Generic;
using Passificator.Exceptions;

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
            List<Guest> list = new List<Guest>();
            foreach (var guest in GuestRepository.GetGuestList())
                guestNameComboBox.Items.Add(guest);

            InitializeDataGridSource();
        }

        private void InitializeDataGridSource()
        {
            _people.ClearChanges();
            guestsDataGrid.DataSource = _people;
            guestsDataGrid.Columns[0].Visible = false;
            guestsDataGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void editStaffButton_Click(object sender, EventArgs e)
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

        private void FillDropDownList(ComboBox dropDownName)
        {
            dropDownName.Items.Clear();

            foreach (var administrator in StaffRepository.GetStaffList())
            {
                dropDownName.Items.Add(administrator);
            }
            dropDownName.ValueMember = "Id";
            dropDownName.DisplayMember = "Name";
        }

        private void addresseeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chosen = (Staff)addresseeNameComboBox.SelectedItem;
            addresseePositionTextBox.Text = chosen.Position;
            if (!String.IsNullOrEmpty((senderNameComboBox.Text)))
                generateButton.Enabled = true;
        }

        private void senderNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chosen = (Staff)senderNameComboBox.SelectedItem;
            senderPositionTextBox.Text = chosen.Position;
            toWhomTextBox.Text = chosen.Name;
            escortTextBox.Text = chosen.Name;
            if (!String.IsNullOrEmpty((addresseeNameComboBox.Text)))
                generateButton.Enabled = true;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveGuestUpdates();

                var context = GetNoteContext();
                var noteGenerator = new NoteGenerator(context);
                noteGenerator.Generate();
                var passGenerator = new PassGenerator(context);
                passGenerator.Generate();
            }
            catch (DocumentGeneratorException exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void SaveGuestUpdates()
        {
            var newGuests = _people
                .Where(p => p.Id < 0);

            foreach (var guest in newGuests)
            {
                var entity = new Guest() { Name = guest.Name, Company = guest.Company, Document = guest.Document, Car = guest.Car };
                var id = GuestRepository.Create(entity);
                guest.Id = id;
            }


            var changedEntities = _people
                .Where(x => x.IsDirty() && x.Id > 0)
                .ToList();

            foreach (var c in changedEntities)
            {
                GuestRepository.Update(new Guest()
                { Id = c.Id, Name = c.Name, Company = c.Company, Document = c.Document, Car = c.Car });
                c.ResetDirty();
            }
        }

        private NoteContextDto GetNoteContext()
        {
            var context = new NoteContextDto
            {
                Adressee = addresseeNameComboBox.Text,
                AdresseePosition = addresseePositionTextBox.Text,
                Sender = senderNameComboBox.Text,
                SenderPosition = senderPositionTextBox.Text,
                SeveralDaysVisit = multipleDaysVisitRadioButton.Checked,
                DateOfVisit = visitDatePicker.Value,
                DateOfVisitFrom = visitDateFromPicker.Value,
                DateOfVisitTo = visitDateToPicker.Value,
                TimeOfVisit = timeOfVisitTextBox.Text,
                Reason = reasonTextBox.Text,
                Escort = escortTextBox.Text,
                PersonAndDepartmentToVisit = toWhomTextBox.Text,
                SenderDepartment = ((Staff)senderNameComboBox.SelectedItem).Department,
                Guests = new List<GuestDto>()
            };

            for (int i = 0; i < guestsDataGrid.Rows.Count; i++)
            {
                context.Guests.Add(new GuestDto()
                {
                    GuestName = guestsDataGrid.Rows[i].Cells[1].Value.ToString(),
                    GuestCompany = guestsDataGrid.Rows[i].Cells[2].Value.ToString(),
                    GuestDocument = guestsDataGrid.Rows[i].Cells[3].Value.ToString(),
                    GuestCar = (guestsDataGrid.Rows[i].Cells[4].Value != null) ? guestsDataGrid.Rows[i].Cells[4].Value.ToString() : ""
                });
            }
            // collect all required data and return dto
            return context;
        }

        private void AddExistingGuest(Guest selectedGuest)
        {
            if (selectedGuest == null)
                return;
            var guestViewModel = new GuestViewModel()
            {
                Id = selectedGuest.Id,
                Name = selectedGuest.Name,
                Company = selectedGuest.Company,
                Document = selectedGuest.Document,
                Car = selectedGuest.Car
            };

            _people.Add(guestViewModel);
            guestViewModel.ResetDirty();
        }

        private void addGuestButton_Click(object sender, EventArgs e)
        {
            if (guestNameComboBox.SelectedItem is Guest guest)
                AddExistingGuest(guest);
            else
                AddNewGuest(guestNameComboBox.Text);

            guestNameComboBox.Text = "";
            addGuestButton.Enabled = false;
        }

        private void AddNewGuest(string name)
        {
            _people.Add(new GuestViewModel() { Name = name });
        }

        private void guestNameComboBox_TextChanged(object sender, EventArgs e)
        {
            if (guestNameComboBox.Text.Split(' ').Length > 1)
                addGuestButton.Enabled = true;
        }
    }
}
