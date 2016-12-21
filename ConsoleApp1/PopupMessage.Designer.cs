namespace SecondMonClient {
	partial class PopupMessage {
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
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelMessage = new System.Windows.Forms.Label();
			this.labelMark = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelTitle
			// 
			this.labelTitle.Font = new System.Drawing.Font("Franklin Gothic Heavy", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(170)))), ((int)(((byte)(16)))));
			this.labelTitle.Location = new System.Drawing.Point(50, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(400, 30);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.Text = "Уведомление МИС Инфоклиника";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelMessage
			// 
			this.labelMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.labelMessage.Font = new System.Drawing.Font("Franklin Gothic Demi", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(121)))), ((int)(((byte)(123)))));
			this.labelMessage.Location = new System.Drawing.Point(50, 30);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(400, 70);
			this.labelMessage.TabIndex = 2;
			this.labelMessage.Text = "label2";
			this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelMark
			// 
			this.labelMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(223)))), ((int)(((byte)(82)))));
			this.labelMark.Font = new System.Drawing.Font("Franklin Gothic Demi", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(170)))), ((int)(((byte)(16)))));
			this.labelMark.Location = new System.Drawing.Point(0, 0);
			this.labelMark.Margin = new System.Windows.Forms.Padding(0);
			this.labelMark.Name = "labelMark";
			this.labelMark.Size = new System.Drawing.Size(50, 100);
			this.labelMark.TabIndex = 3;
			this.labelMark.Text = "!";
			this.labelMark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// PopupMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Beige;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(450, 100);
			this.ControlBox = false;
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.labelMark);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.labelTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PopupMessage";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Уведомление МИС Инфоклиника";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.Label labelMark;
		private System.Windows.Forms.Button buttonClose;
	}
}