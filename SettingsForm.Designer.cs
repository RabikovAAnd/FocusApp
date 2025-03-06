namespace Приложение_Для_Концентрации
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NumericUpDown numWorkTime;
        private System.Windows.Forms.NumericUpDown numBreakTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numWorkTime = new System.Windows.Forms.NumericUpDown();
            this.numBreakTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreakTime)).BeginInit();
            this.SuspendLayout();
            // 
            // numWorkTime
            // 
            this.numWorkTime.Location = new System.Drawing.Point(100, 50);
            this.numWorkTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numWorkTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWorkTime.Name = "numWorkTime";
            this.numWorkTime.Size = new System.Drawing.Size(100, 20);
            this.numWorkTime.TabIndex = 0;
            this.numWorkTime.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numBreakTime
            // 
            this.numBreakTime.Location = new System.Drawing.Point(100, 100);
            this.numBreakTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numBreakTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBreakTime.Name = "numBreakTime";
            this.numBreakTime.Size = new System.Drawing.Size(100, 20);
            this.numBreakTime.TabIndex = 1;
            this.numBreakTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Work:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Break:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(50, 150);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numBreakTime);
            this.Controls.Add(this.numWorkTime);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numWorkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreakTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}