namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU
{
	partial class Form_save
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
			this.richTextBox_name = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button_save = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label_count_neurons = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// richTextBox_name
			// 
			this.richTextBox_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.richTextBox_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.richTextBox_name.ForeColor = System.Drawing.Color.Coral;
			this.richTextBox_name.Location = new System.Drawing.Point(97, 44);
			this.richTextBox_name.Name = "richTextBox_name";
			this.richTextBox_name.Size = new System.Drawing.Size(175, 18);
			this.richTextBox_name.TabIndex = 0;
			this.richTextBox_name.Text = "<Имя>";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label1.Location = new System.Drawing.Point(12, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Название:";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.Color.Silver;
			this.button1.Location = new System.Drawing.Point(1, 105);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(142, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Не сохранять";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_save
			// 
			this.button_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.button_save.FlatAppearance.BorderSize = 0;
			this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button_save.ForeColor = System.Drawing.Color.Silver;
			this.button_save.Location = new System.Drawing.Point(141, 105);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(142, 23);
			this.button_save.TabIndex = 3;
			this.button_save.Text = "Сохранить";
			this.button_save.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label2.Location = new System.Drawing.Point(12, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(175, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Колличество нейронов:";
			// 
			// label_count_neurons
			// 
			this.label_count_neurons.AutoSize = true;
			this.label_count_neurons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label_count_neurons.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label_count_neurons.ForeColor = System.Drawing.Color.Coral;
			this.label_count_neurons.Location = new System.Drawing.Point(192, 75);
			this.label_count_neurons.Name = "label_count_neurons";
			this.label_count_neurons.Size = new System.Drawing.Size(40, 18);
			this.label_count_neurons.TabIndex = 5;
			this.label_count_neurons.Text = "1000";
			// 
			// Form_save
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 130);
			this.Controls.Add(this.label_count_neurons);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button_save);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox_name);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_save";
			this.Text = "Сохранение";
			this.Load += new System.EventHandler(this.Form_save_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Button button_save;
		public System.Windows.Forms.RichTextBox richTextBox_name;
		public System.Windows.Forms.Label label_count_neurons;
	}
}