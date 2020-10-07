using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Passificator.Data;

namespace Passificator
{
    public partial class StaffEditorForm : Form
    {
        public StaffEditorForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StaffEditorForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            //dataGridView1.DataSource = GetStaffDataTable();
            using (var context = new DatabaseContext())
            {
                var administrators = from a in context.Administrators
                    select a;

                dataGridView1.DataSource = administrators.ToList();
            }
        }

        private DataTable GetStaffDataTable()
        {
            using (var context = new DatabaseContext())
            {
                var administrators = from a in context.Administrators.AsEnumerable()
                                     select a;

                var table = new DataTable();
                table.Columns.Add("Name");
                table.Columns.Add("Position");
                foreach (var a in administrators)
                {
                    var dr = table.NewRow();
                    dr["Name"] = a.Name;
                    dr["Position"] = a.Position;
                    table.Rows.Add(dr);
                }
                
                return table;
            }
        }
    }
}
