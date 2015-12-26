namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU
{
	partial class NeuronInformationPanel
	{
		/// <summary> 
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.button_del = new System.Windows.Forms.Button();
			this.button_add = new System.Windows.Forms.Button();
			this.label_text = new System.Windows.Forms.Label();
			this.label_textInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button_del
			// 
			this.button_del.FlatAppearance.BorderSize = 0;
			this.button_del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_del.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button_del.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button_del.Location = new System.Drawing.Point(502, 0);
			this.button_del.Name = "button_del";
			this.button_del.Size = new System.Drawing.Size(78, 30);
			this.button_del.TabIndex = 0;
			this.button_del.Text = "Удалить";
			this.button_del.UseVisualStyleBackColor = true;
			// 
			// button_add
			// 
			this.button_add.FlatAppearance.BorderSize = 0;
			this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button_add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.button_add.Location = new System.Drawing.Point(414, 0);
			this.button_add.Name = "button_add";
			this.button_add.Size = new System.Drawing.Size(82, 30);
			this.button_add.TabIndex = 1;
			this.button_add.Text = "Добавить";
			this.button_add.UseVisualStyleBackColor = true;
			// 
			// label_text
			// 
			this.label_text.AutoSize = true;
			this.label_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.label_text.Location = new System.Drawing.Point(3, 0);
			this.label_text.Name = "label_text";
			this.label_text.Size = new System.Drawing.Size(20, 22);
			this.label_text.TabIndex = 3;
			this.label_text.Text = "1";
			this.label_text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label_textInfo
			// 
			this.label_textInfo.AutoSize = true;
			this.label_textInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label_textInfo.ForeColor = System.Drawing.Color.Turquoise;
			this.label_textInfo.Location = new System.Drawing.Point(251, 0);
			this.label_textInfo.Name = "label_textInfo";
			this.label_textInfo.Size = new System.Drawing.Size(20, 22);
			this.label_textInfo.TabIndex = 4;
			this.label_textInfo.Text = "1";
			this.label_textInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// NeuronInformationPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.label_textInfo);
			this.Controls.Add(this.label_text);
			this.Controls.Add(this.button_add);
			this.Controls.Add(this.button_del);
			this.Name = "NeuronInformationPanel";
			this.Size = new System.Drawing.Size(580, 30);
			this.Load += new System.EventHandler(this.NeuronInformationPanel_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_text;
		public System.Windows.Forms.Button button_del;
		public System.Windows.Forms.Button button_add;
		private System.Windows.Forms.Label label_textInfo;
	}
}
