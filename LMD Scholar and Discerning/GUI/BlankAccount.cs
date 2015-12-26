using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning.GUI
{
	public partial class BlankAccount : Form
	{
		#region variables
		public ComboBox comboBox;
		public RadioButton[] rBut;
		public Bitmap img_fon;
		public RichTextBox[] rtb;
		public RichTextBox rtb_key;
		public LMD_TopBoard topBoard;
		public Button but_start;
		public String str_message;
		private System.Drawing.Drawing2D.LinearGradientBrush linearGrad;
		#endregion
		#region BlankAccount
		public BlankAccount()
		{
			InitializeComponent();
		}
		#endregion
		#region BlankAccount_Load
		private void BlankAccount_Load(object sender, EventArgs e)
		{
			topBoard = new LMD_TopBoard(this, true);
			topBoard.but_account.Enabled = false;
			topBoard.but_account.Visible = false;
			topBoard.pos_title_no_free = new Point(5, 5);

			linearGrad = new System.Drawing.Drawing2D.LinearGradientBrush(
				new RectangleF(1, topBoard.Bottom - 1, topBoard.Width, 15),
				topBoard.BackColor, Color.FromArgb(150, 192, 192, 192), System.Drawing.Drawing2D.LinearGradientMode.Vertical);

			this.Paint += BlankAccount_Paint;

			img_fon = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(img_fon);

			for (int y = 0; y < this.Height; y += 10)
				g.DrawLine(new Pen(Color.FromArgb(50, 100, 100, 100), 2), 0, y, this.Width, y);

			rtb = new RichTextBox[6];
			int intend = 70;
			for (int i = 0; i < rtb.Length; i++)
			{
				rtb[i] = new RichTextBox();
				rtb[i].Size = new System.Drawing.Size(250, 30);
				rtb[i].Location = new Point(this.Width / 2 - rtb[i].Width / 2 + 60, intend);
				rtb[i].BorderStyle = BorderStyle.None;
				rtb[i].BackColor = Color.FromArgb(192, 192, 192);
				rtb[i].Font = new Font("Arial", 17f, FontStyle.Bold);
				rtb[i].ForeColor = Color.FromArgb(110, 110, 110);

				this.Controls.Add(rtb[i]);

				intend += rtb[i].Height + 20;
			}

			rtb[0].Tag = "Имя:";
			rtb[1].Tag = "Фамилия:";
			rtb[2].Tag = "Ник:";
			rtb[3].Tag = "E-mail:";
			rtb[4].Tag = "Пароль:";
			rtb[5].Tag = "Еще раз:";

			comboBox = new ComboBox();
			comboBox.Location = new Point(230, 375);
			comboBox.FlatStyle = FlatStyle.Flat;
			comboBox.BackColor = Color.FromArgb(192, 192, 192);
			comboBox.ForeColor = Color.FromArgb(110, 110, 110);
			
			for (int i = 2016; i > 1900; i--)
				comboBox.Items.Add(i);

			this.Controls.Add(comboBox);

			but_start = new Button();
			but_start.FlatStyle = FlatStyle.Flat;
			but_start.FlatAppearance.BorderSize = 0;
			but_start.Size = new System.Drawing.Size(100, 30);
			but_start.Location = new Point(270, this.Height - but_start.Height - 10);
			but_start.Text = "Отправить";
			but_start.Font = new Font("Arial", 12f);
			but_start.BackColor = Color.FromArgb(100, 100, 100);
			but_start.ForeColor = Color.FromArgb(192, 192, 192);
			this.Controls.Add(but_start);
			but_start.Click += but_start_Click;

			str_message = "На вашу электронную почту будет\n отправлен код подтверждения.";

			rtb_key = new RichTextBox();
			rtb_key.Size = new System.Drawing.Size(250, 30);
			rtb_key.Location = new Point(10, this.Height - rtb_key.Height - 10);
			rtb_key.BorderStyle = BorderStyle.None;
			rtb_key.BackColor = Color.FromArgb(192, 192, 192);
			rtb_key.Font = new Font("Arial", 17f, FontStyle.Bold);
			rtb_key.ForeColor = Color.FromArgb(110, 110, 110);
			rtb_key.Visible = false;
			rtb_key.Text = "Введите код";
			this.Controls.Add(rtb_key);

			rBut = new RadioButton[2];
			intend = 150;
			for (int i = 0; i < rtb.Length; i++)
			{
				rBut[i] = new RadioButton();
				rBut[i].Size = new System.Drawing.Size(110, 30);
				rBut[i].Location = new Point(intend, 420);
				rBut[i].Font = new Font("Arial", 13f);
				rBut[i].FlatStyle = FlatStyle.Flat;
				rBut[i].BackColor = Color.FromArgb(192, 192, 192);
				
				if (i == 0)
					rBut[0].Text = "Мужской";
				else rBut[1].Text = "Женский";

				this.Controls.Add(rBut[i]);

				intend += rBut[i].Width;
			}
		}
		#endregion
		#region Button click
		void but_start_Click(object sender, EventArgs e)
		{
			// Тут идет отправка...
			str_message = "";
			but_start.Text = "Готово";
			rtb_key.Visible = true;
			this.Invalidate();
		}
		#endregion

		#region Drawing
		void BlankAccount_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Color.FromArgb(192, 192, 192));

			g.DrawImage(img_fon, 0, topBoard.Bottom);

			g.FillRectangle(linearGrad, 1, topBoard.Bottom - 1, topBoard.Width, 15);
		//	g.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)), 1, topBoard.Bottom, this.Width - 2, this.Height - topBoard.Height - 2);
			g.DrawRectangle(new Pen(topBoard.BackColor, 2), 1, 0, this.Width - 2, this.Height - 1);

			g.DrawString("Пол:", new Font("Arial", 17, FontStyle.Bold), new SolidBrush(Color.FromArgb(100, 100, 100)), new PointF(10, 420));
			g.DrawString("Дата рождения:", new Font("Arial", 17, FontStyle.Bold), new SolidBrush(Color.FromArgb(100, 100, 100)), new PointF(10, 370));
			g.DrawString(str_message, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(100, 100, 100)), new PointF(10, 455));
			for (int i = 0; i < rtb.Length; i++)
			{
				if (rtb[i].Visible == true)
				{
					g.DrawString(rtb[i].Tag.ToString(), new Font("Arial", 17, FontStyle.Bold), new SolidBrush(Color.FromArgb(100, 100, 100)), new PointF(10, rtb[i].Top));
					g.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 255, 255)), rtb[i].Left - 5, rtb[i].Top - 5, rtb[i].Width + 10, rtb[i].Height + 10);
				}
			}
		}
		#endregion
	}
}
