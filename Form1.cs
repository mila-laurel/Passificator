﻿using System;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Dto;
using Passificator.ViewModel;
using Passificator.Utilities.Collections;
using System.Linq;
using System.Collections.Generic;

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
            var newEntities = _people.Changes
               .Where(c => c.ChangeType == ChangeType.Added)
               .Select(c => new Guest() { Name = c.Item.Name, Company = c.Item.Company, Document = c.Item.Document });
            foreach (Guest entity in newEntities)
                GuestRepository.Create(entity);

            var changedEntities = _people
                    .Where(x => x.IsDirty() && x.Id > 0)
                    .ToList();

            foreach (var c in changedEntities)
            {
                GuestRepository.Update(new Guest() { Id = c.Id, Name = c.Name, Company = c.Company, Document = c.Document });
            }

            _people.ClearChanges();

            var context = GetNoteContext();
            var noteGenerator = new NoteGenerator(context);
            noteGenerator.Generate();
        }

        private NoteContextDTO GetNoteContext()
        {
            NoteContextDTO context = new NoteContextDTO();
            context.Adressee = addresseeNameComboBox.Text;
            context.AdresseePosition = addresseePositionTextBox.Text;
            context.Sender = senderNameComboBox.Text;
            context.SenderPosition = senderPositionTextBox.Text;
            context.DateOfVisit = visitDatePicker.Value;
            context.DateOfVisitFrom = visitDateFromPicker.Value;
            context.DateOfVisitTo = visitDateToPicker.Value;
            context.Reason = reasonTextBox.Text;
            context.Escort = escortTextBox.Text;
            context.PersonAndDepartmentToVisit = toWhomTextBox.Text + ", ";
            context.Guests = new List<GuestDto>();
            for (int i = 0; i < guestsDataGrid.Rows.Count-1; i++)
            {
                context.Guests.Add(new GuestDto() { GuestName = guestsDataGrid.Rows[i].Cells[1].Value.ToString(), GuestCompany = guestsDataGrid.Rows[i].Cells[2].Value.ToString(), GuestDocument = guestsDataGrid.Rows[i].Cells[3].Value.ToString() });
            }
            // collect all required data and return dto
            return context;
        }

        private void UpdateData(string selectedGuest)
        {
            var guests = (from a in GuestRepository.GetGuestList()
                          where a.Name.Equals(selectedGuest)
                          select new GuestViewModel() { Id = a.Id, Name = a.Name, Company = a.Company, Document = a.Document }).ToList();
            if (guests.Any())
            {
                foreach (var guest in guests)
                {
                    guest.ResetDirty();
                    _people.Add(guest);
                }
            }
            else
            {
                _people.Add(new GuestViewModel() { Name = selectedGuest });
            }

            guestsDataGrid.DataSource = _people;
            guestsDataGrid.Columns[0].Visible = false;
            guestsDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _people.ClearChanges();
        }

        private void guestNameComboBox_TextChanged(object sender, EventArgs e)
        {
            if (guestNameComboBox.Text.Length > 3)
            {
                var guestsNames = (from a in GuestRepository.GetGuestList()
                                   where a.Name.Contains(guestNameComboBox.Text)
                                   select a.Name).ToArray();
                if (guestsNames.Any())
                {
                    if (guestNameComboBox.Items.Count > 0)
                        guestNameComboBox.Items.Clear();
                    guestNameComboBox.Items.AddRange(guestsNames);
                    guestNameComboBox.DroppedDown = true;
                }
            }
        }

        private void addGuestButton_Click(object sender, EventArgs e)
        {
            UpdateData(guestNameComboBox.Text);
        }
    }
}
