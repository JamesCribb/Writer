namespace Writer
{
    partial class WriterForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.loadButton = new System.Windows.Forms.Button();
            this.monthPanel = new System.Windows.Forms.Panel();
            this.notificationLabel = new System.Windows.Forms.Label();
            this.writingRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Transparent;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Lato", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(897, 29);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 31);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // progressPanel
            // 
            this.progressPanel.Location = new System.Drawing.Point(978, 29);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(65, 65);
            this.progressPanel.TabIndex = 3;
            this.progressPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.progressPanel_Paint);
            // 
            // loadButton
            // 
            this.loadButton.FlatAppearance.BorderSize = 0;
            this.loadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadButton.Font = new System.Drawing.Font("Lato", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(897, 63);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 31);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // monthPanel
            // 
            this.monthPanel.Location = new System.Drawing.Point(33, 29);
            this.monthPanel.Name = "monthPanel";
            this.monthPanel.Size = new System.Drawing.Size(650, 134);
            this.monthPanel.TabIndex = 5;
            this.monthPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.monthPanel_Paint);
            this.monthPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.monthPanel_MouseClick);
            // 
            // notificationLabel
            // 
            this.notificationLabel.AutoSize = true;
            this.notificationLabel.Font = new System.Drawing.Font("Lato", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationLabel.Location = new System.Drawing.Point(916, 110);
            this.notificationLabel.Name = "notificationLabel";
            this.notificationLabel.Size = new System.Drawing.Size(0, 13);
            this.notificationLabel.TabIndex = 6;
            // 
            // writingRichTextBox
            // 
            this.writingRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.writingRichTextBox.Font = new System.Drawing.Font("Lato", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writingRichTextBox.Location = new System.Drawing.Point(33, 189);
            this.writingRichTextBox.Name = "writingRichTextBox";
            this.writingRichTextBox.Size = new System.Drawing.Size(1010, 539);
            this.writingRichTextBox.TabIndex = 7;
            this.writingRichTextBox.Text = "";
            this.writingRichTextBox.TextChanged += new System.EventHandler(this.writingRichTextBox_TextChanged);
            // 
            // WriterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 761);
            this.Controls.Add(this.writingRichTextBox);
            this.Controls.Add(this.notificationLabel);
            this.Controls.Add(this.monthPanel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.saveButton);
            this.Name = "WriterForm";
            this.Text = "Writer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WriterForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Panel monthPanel;
        private System.Windows.Forms.Label notificationLabel;
        private System.Windows.Forms.RichTextBox writingRichTextBox;
    }
}

