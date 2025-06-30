namespace AutoRes
{
    partial class AutoRes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoRes));
            pnlTop = new Panel();
            pnlFooter = new Panel();
            btnSaveChanges = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            pnlMain = new Panel();
            dgvConfigs = new DataGridView();
            dgvColSelect = new DataGridViewCheckBoxColumn();
            dgvColProgram = new DataGridViewTextBoxColumn();
            dgvColResolution = new DataGridViewComboBoxColumn();
            dgvColScale = new DataGridViewComboBoxColumn();
            dgvColID = new DataGridViewTextBoxColumn();
            notifyIcon = new NotifyIcon(components);
            cmsNotifyIcon = new ContextMenuStrip(components);
            tsmiCerrarAutoRes = new ToolStripMenuItem();
            pnlFooter.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfigs).BeginInit();
            cmsNotifyIcon.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(578, 22);
            pnlTop.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(btnSaveChanges);
            pnlFooter.Controls.Add(btnAdd);
            pnlFooter.Controls.Add(btnDelete);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 734);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(578, 60);
            pnlFooter.TabIndex = 1;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(12, 14);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(112, 34);
            btnSaveChanges.TabIndex = 2;
            btnSaveChanges.Text = "Guardar";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // btnAdd
            // 
            btnAdd.ForeColor = Color.Lime;
            btnAdd.Location = new Point(336, 14);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Agregar";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.ForeColor = Color.Red;
            btnDelete.Location = new Point(454, 14);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 34);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(dgvConfigs);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 22);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(578, 712);
            pnlMain.TabIndex = 2;
            // 
            // dgvConfigs
            // 
            dgvConfigs.AllowUserToAddRows = false;
            dgvConfigs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConfigs.Columns.AddRange(new DataGridViewColumn[] { dgvColSelect, dgvColProgram, dgvColResolution, dgvColScale, dgvColID });
            dgvConfigs.Dock = DockStyle.Fill;
            dgvConfigs.Location = new Point(0, 0);
            dgvConfigs.Name = "dgvConfigs";
            dgvConfigs.RowHeadersVisible = false;
            dgvConfigs.RowHeadersWidth = 62;
            dgvConfigs.Size = new Size(578, 712);
            dgvConfigs.TabIndex = 0;
            dgvConfigs.ColumnHeaderMouseClick += dgvConfigs_ColumnHeaderMouseClick;
            dgvConfigs.CurrentCellDirtyStateChanged += dgvConfigs_CurrentCellDirtyStateChanged;
            // 
            // dgvColSelect
            // 
            dgvColSelect.Frozen = true;
            dgvColSelect.HeaderText = "✅";
            dgvColSelect.MinimumWidth = 8;
            dgvColSelect.Name = "dgvColSelect";
            dgvColSelect.Resizable = DataGridViewTriState.False;
            dgvColSelect.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvColSelect.Width = 50;
            // 
            // dgvColProgram
            // 
            dgvColProgram.Frozen = true;
            dgvColProgram.HeaderText = "Programa";
            dgvColProgram.MinimumWidth = 8;
            dgvColProgram.Name = "dgvColProgram";
            dgvColProgram.ReadOnly = true;
            dgvColProgram.Width = 150;
            // 
            // dgvColResolution
            // 
            dgvColResolution.Frozen = true;
            dgvColResolution.HeaderText = "Resolución";
            dgvColResolution.Items.AddRange(new object[] { "7680x4320", "5120x1440", "3840x2160", "3440x1440", "2560x1600", "2560x1440", "2560x1080", "1920x1200", "1920x1080", "1680x1050", "1600x900", "1440x900", "1366x768", "1360x768", "1280x800", "1280x720", "1024x768", "800x600" });
            dgvColResolution.MinimumWidth = 8;
            dgvColResolution.Name = "dgvColResolution";
            dgvColResolution.Resizable = DataGridViewTriState.False;
            dgvColResolution.Width = 150;
            // 
            // dgvColScale
            // 
            dgvColScale.Frozen = true;
            dgvColScale.HeaderText = "Escala";
            dgvColScale.Items.AddRange(new object[] { "100%", "125%", "150%", "175%", "200%", "225%", "250%", "275%", "300%", "325%", "350%", "375%", "400%", "425%", "450%", "475%", "500%" });
            dgvColScale.MinimumWidth = 8;
            dgvColScale.Name = "dgvColScale";
            dgvColScale.Resizable = DataGridViewTriState.False;
            dgvColScale.Width = 150;
            // 
            // dgvColID
            // 
            dgvColID.Frozen = true;
            dgvColID.HeaderText = "ID";
            dgvColID.MinimumWidth = 8;
            dgvColID.Name = "dgvColID";
            dgvColID.ReadOnly = true;
            dgvColID.Resizable = DataGridViewTriState.False;
            dgvColID.Visible = false;
            dgvColID.Width = 150;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = cmsNotifyIcon;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "AutoRes";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // cmsNotifyIcon
            // 
            cmsNotifyIcon.ImageScalingSize = new Size(24, 24);
            cmsNotifyIcon.Items.AddRange(new ToolStripItem[] { tsmiCerrarAutoRes });
            cmsNotifyIcon.Name = "cmsNotifyIcon";
            cmsNotifyIcon.Size = new Size(241, 69);
            // 
            // tsmiCerrarAutoRes
            // 
            tsmiCerrarAutoRes.Name = "tsmiCerrarAutoRes";
            tsmiCerrarAutoRes.Size = new Size(240, 32);
            tsmiCerrarAutoRes.Text = "Cerrar";
            tsmiCerrarAutoRes.Click += tsmiCerrarAutoRes_Click;
            // 
            // AutoRes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 794);
            Controls.Add(pnlMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AutoRes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AutoRes";
            FormClosing += AutoRes_FormClosing;
            Load += AutoRes_Load;
            pnlFooter.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvConfigs).EndInit();
            cmsNotifyIcon.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Panel pnlFooter;
        private Panel pnlMain;
        private DataGridView dgvConfigs;
        private Button btnAdd;
        private Button btnDelete;
        private DataGridViewCheckBoxColumn dgvColSelect;
        private DataGridViewTextBoxColumn dgvColProgram;
        private DataGridViewComboBoxColumn dgvColResolution;
        private DataGridViewComboBoxColumn dgvColScale;
        private DataGridViewTextBoxColumn dgvColID;
        private Button btnSaveChanges;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip cmsNotifyIcon;
        private ToolStripMenuItem tsmiCerrarAutoRes;
    }
}
