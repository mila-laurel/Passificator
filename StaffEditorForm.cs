using System;
using System.Linq;
using System.Windows.Forms;
using Passificator.Data;
using Passificator.Model;
using Passificator.Utilities.Collections;

namespace Passificator
{
    public partial class StaffEditorForm : Form
    {
        private TrackList<StaffViewModel> people = new TrackList<StaffViewModel>();

        public StaffEditorForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = people;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }

        private void SaveData()
        {
            using (var context = new DatabaseContext())
            {
                var idToDelete = people.Changes
                    .Where(c => c.ChangeType == ChangeType.Removed)
                    .Where(c => c.Item.Id > 0)
                    .Select(c => c.Item.Id);
                var entitiesToRemove = context.Administrators
                    .Where(a => idToDelete.Contains(a.Id))
                    .ToList();

                context.Administrators.RemoveRange(entitiesToRemove);

                context.SaveChanges();
            }

            using (var context = new DatabaseContext())
            {
                var newEntities = people.Changes
                    .Where(c => c.ChangeType == ChangeType.Added)
                    .Select(c => new Staff() {Name = c.Item.Name, Position = c.Item.Position});

                context.Administrators.AddRange(newEntities);

                context.SaveChanges();
            }

            using (var context = new DatabaseContext())
            {
                var changedEntities = people
                    .Where(x => x.IsDirty() && x.Id > 0)
                    .ToList();

                foreach (var c in changedEntities)
                {
                    var entity = context.Administrators.Find(c.Id);
                    entity.Name = c.Name;
                    entity.Position = c.Position;
                }

                context.SaveChanges();
            }

            people.ClearChanges();
        }

        private void StaffEditorForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            using (var context = new DatabaseContext())
            {
                people.Clear();
                var administrators = from a in context.Administrators
                                     select new StaffViewModel() { Id = a.Id, Name = a.Name, Position = a.Position };

                foreach (var administrator in administrators.ToList())
                {
                    administrator.ResetDirty();
                    people.Add(administrator);
                }

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                people.ClearChanges();
            }
        }

        private class StaffViewModel
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
}
