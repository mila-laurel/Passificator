using System;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Dto;
using System.Runtime.Remoting.Messaging;

namespace Passificator
{
    public partial class Form1 : Form
    {
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

        private static void FillDropDownList(ComboBox DropDownName)
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
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            var context = GetNoteContext();
            //var noteGenerator = new NoteGenerator(context);
            //noteGenerator.Generate();
        }

        private NoteContextDTO GetNoteContext()
        {
            NoteContextDTO context = new NoteContextDTO();
            context.Adressee = addresseeNameComboBox.Text;
            if (oneDayVisitRadioButton.Checked)
                context.DateOfVisit = visitDatePicker.Value;
            else
            {
                context.DateOfVisitFrom = visitDateFromPicker.Value;
                context.DateOfVisitTo = visitDateToPicker.Value;
            }
            // collect all required data and return dto
            return context;
        }
    }
}
