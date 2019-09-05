using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThemeFileEditor
{
    public partial class ThemeEditor : Form
    {
        public ThemeEditor()
        {
            InitializeComponent();
        }

        public ThemeForm ActiveThemeForm
        {
            get
            {
                if (ActiveMdiChild is ThemeForm)
                {
                    return (ThemeForm)ActiveMdiChild;
                }
                return null;
            }
        }

        private void Save()
        {
            if (ActiveThemeForm == null) {return;}

            if (string.IsNullOrEmpty(ActiveThemeForm.FileName))
            {
                SaveAs();
            }
            else
            {
                ActiveThemeForm.Save();
            }
        }


        private void SaveAs()
        {
            if (ActiveThemeForm == null) { return; }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (string.IsNullOrEmpty(ActiveThemeForm.FileName))
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
            }
            else
            {
                saveFileDialog.FileName = ActiveThemeForm.FileName;
            }

            saveFileDialog.Filter = "Theme Files (*.theme)|*.theme|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ActiveThemeForm.SaveAs(FileName);
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new ThemeForm();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            const string openpath = @"C:\WorkingCopy\ThemeFileEditor\Themes";
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (File.Exists(openpath))
            {
                openFileDialog.InitialDirectory = openpath;
            }else if (File.Exists(ActiveThemeForm?.FileName))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(ActiveThemeForm.FileName);
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
            }

            openFileDialog.Filter = "Theme Files (*.theme)|*.theme|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                Form childForm = new ThemeForm(FileName);
                childForm.MdiParent = this;
                childForm.Show();
            }

        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        private void ThemeEditor_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                Form childForm = new ThemeForm(file);
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void ThemeEditor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButtonApply_Click(object sender, EventArgs e)
        {
            if (ActiveThemeForm == null) { return; }

            ActiveThemeForm.Apply();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButtonTestForm_Click(object sender, EventArgs e)
        {
            Form childForm = new Form1();
            childForm.MdiParent = this;
            childForm.Show();
        }
    }
}
