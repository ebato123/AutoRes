namespace AutoRes.Forms
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbResolution = new ComboBox();
            cbScale = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnCancel = new Button();
            btnAccept = new Button();
            SuspendLayout();
            // 
            // cbResolution
            // 
            cbResolution.FormattingEnabled = true;
            cbResolution.Items.AddRange(new object[] { "7680x4320", "5120x1440", "3840x2160", "3440x1440", "2560x1600", "2560x1440", "2560x1080", "1920x1200", "1920x1080", "1680x1050", "1600x900", "1440x900", "1366x768", "1360x768", "1280x800", "1280x720", "1024x768", "800x600" });
            cbResolution.Location = new Point(20, 48);
            cbResolution.Name = "cbResolution";
            cbResolution.Size = new Size(182, 33);
            cbResolution.TabIndex = 0;
            cbResolution.KeyPress += keyPress;
            // 
            // cbScale
            // 
            cbScale.FormattingEnabled = true;
            cbScale.Items.AddRange(new object[] { "100%", "125%", "150%", "175%", "200%", "225%", "250%", "275%", "300%", "325%", "350%", "375%", "400%", "425%", "450%", "475%", "500%" });
            cbScale.Location = new Point(220, 48);
            cbScale.Name = "cbScale";
            cbScale.Size = new Size(182, 33);
            cbScale.TabIndex = 1;
            cbScale.KeyPress += keyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 19);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 2;
            label1.Text = "Resolución";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(220, 19);
            label2.Name = "label2";
            label2.Size = new Size(59, 25);
            label2.TabIndex = 3;
            label2.Text = "Escala";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(290, 115);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(172, 115);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(112, 34);
            btnAccept.TabIndex = 5;
            btnAccept.Text = "Aceptar";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 161);
            Controls.Add(btnAccept);
            Controls.Add(btnCancel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbScale);
            Controls.Add(cbResolution);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbResolution;
        private ComboBox cbScale;
        private Label label1;
        private Label label2;
        private Button btnCancel;
        private Button btnAccept;
    }
}