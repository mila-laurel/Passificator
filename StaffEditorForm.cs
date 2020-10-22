using System;
using System.Linq;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Utilities.Collections;
using Passificator.ViewModel;

namespace Passificator
{
    public partial class StaffEditorForm : Form
    {
        private readonly TrackList<StaffViewModel> _people = new TrackList<StaffViewModel>();

        public StaffEditorForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = _people;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }

        private void SaveData()
        {
            var idToDelete = _people.Changes
                .Where(c => c.ChangeType == ChangeType.Removed)
                .Where(c => c.Item.Id > 0)
                .Select(c => c.Item.Id);
            foreach (int id in idToDelete)
                StaffRepository.Delete(id);

            var newEntities = _people.Changes
                .Where(c => c.ChangeType == ChangeType.Added)
                .Select(c => new Staff() { Name = c.Item.Name, Position = c.Item.Position, Department = c.Item.Department });
            foreach (Staff entity in newEntities)
                StaffRepository.Create(entity);


            var changedEntities = _people
                    .Where(x => x.IsDirty() && x.Id > 0)
                    .ToList();

                foreach (var c in changedEntities)
                {
                StaffRepository.Update(new Staff() { Id = c.Id, Name = c.Name, Position = c.Position, Department = c.Department });
                }

            _people.ClearChanges();
        }

        private void StaffEditorForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            _people.Clear();
            
                var administrators = from a in StaffRepository.GetStaffList()
                select new StaffViewModel() { Id = a.Id, Name = a.Name, Position = a.Position, Department = a.Department};

                foreach (var administrator in administrators.ToList())
                {
                    administrator.ResetDirty();
                    _people.Add(administrator);
                }

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _people.ClearChanges();
        } 
    }
}
