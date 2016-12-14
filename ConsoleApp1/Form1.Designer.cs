namespace SecondMonClient {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Franklin Gothic Heavy", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(170)))), ((int)(((byte)(16)))));
			this.label1.Location = new System.Drawing.Point(50, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(400, 30);
			this.label1.TabIndex = 1;
			this.label1.Text = "Уведомление МИС Инфоклиника";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Franklin Gothic Demi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(121)))), ((int)(((byte)(123)))));
			this.label2.Location = new System.Drawing.Point(50, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(400, 70);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(223)))), ((int)(((byte)(82)))));
			this.label3.Font = new System.Drawing.Font("Franklin Gothic Demi", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(170)))), ((int)(((byte)(16)))));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Margin = new System.Windows.Forms.Padding(0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 100);
			this.label3.TabIndex = 3;
			this.label3.Text = "!";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonClose
			// 
			this.buttonClose.BackColor = System.Drawing.Color.Beige;
			this.buttonClose.FlatAppearance.BorderSize = 0;
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClose.Font = new System.Drawing.Font("Franklin Gothic Heavy", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonClose.Location = new System.Drawing.Point(420, 0);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(30, 30);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "X";
			this.buttonClose.UseVisualStyleBackColor = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Beige;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(450, 100);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Уведомление МИС Инфоклиника";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonClose;
	}
}