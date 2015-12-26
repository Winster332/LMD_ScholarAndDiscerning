using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace LMD_Scholar_and_Discerning.GUI
{
	public class LMD_Interface : Panel
	{
		#region variables
		public delegate void ClickRecognize(Bitmap image, List<float> x);
		public delegate void ClickAddNeuron(Bitmap image, List<float> w);
		public delegate void EndLoadFiles(List<NeuralNetwork.Neuron> ns);
		public delegate void ClickLoadNeurons();
		public delegate void ClickSaveNN();

		public event ClickRecognize onClickRecognize;
		public event ClickAddNeuron onClickAddNeuron;
		public event EndLoadFiles onEndLoadFiles;
		public event ClickLoadNeurons onClickLoadNeurons;
		public event ClickSaveNN onClickSaveNN;

		public Color color_buttons_Background = Color.FromArgb(110, 70, 170, 70);
		public Color color_buttons_Fore = Color.FromArgb(50, 50, 50);
		public Color color_buttons_MouseOverBackColor = Color.FromArgb(192, 70, 170, 70);
		public Color color_Cursor = Color.Black;
		public int Size_Cursor = 40;
		public Button[] but;
		public RichTextBox textBox;
		public List<float> X;
		public PictureBox pBox;
		public System.Windows.Forms.Timer UpDate;
		public LMD_Loading loading;
		private Form form;
		private Graphics g;
		private Bitmap image;
		private int LoadingElement;
		private int CountElement;
		private PaintManager pManager;
		private ContextMenu context_menu;
		#endregion
		#region LMD_Interface
		public LMD_Interface(Form form)
		{
			#region this
			this.X = new List<float>();
			this.form = form;
			base.Parent = form;
			this.pBox = new PictureBox();
			this.form.Controls.Add(this.pBox);
			this.pBox.BackColor = Color.FromArgb(192, 192, 192);
			this.pBox.Location = new Point(1, ((Form1)form).topBoard.Bottom);
			this.pBox.Size = new Size(form.Width - 2, form.Height - ((Form1)form).topBoard.Height - 122);
			this.BackColor = Color.FromArgb(100, 100, 100);
			base.Location = new Point(1, this.pBox.Bottom);
			base.Size = new Size(form.Width - 2, form.Width - this.pBox.Height + ((Form1)form).topBoard.Height - 2);
			this.image = new Bitmap(this.pBox.Width, this.pBox.Height);
			this.g = Graphics.FromImage(this.image);
			this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			this.UpDate = new System.Windows.Forms.Timer();
			this.UpDate.Interval = 30;
			this.UpDate.Tick += new EventHandler(this.UpDate_Tick);
			this.loading = new LMD_Loading(this);
			this.pManager = new PaintManager(new Pen(color_Cursor, 20));
			#endregion
			#region context menu
			MenuItem[] m_items = new MenuItem[5];
			m_items[0] = new MenuItem("Отменить");
			m_items[1] = new MenuItem("Обновить");
			m_items[2] = new MenuItem("Очистить");
			m_items[3] = new MenuItem("Распознать");
			m_items[4] = new MenuItem("Добавить нейрон");

			for (int i = 0; i < m_items.Length; i++)
			{
				m_items[i].Click += (o, e) =>
					{
						MenuItem m = (MenuItem)o;

						switch (m.Text)
						{
							case "Отменить":
								if (pManager.getPositions().Count > 0)
								{
									pManager.RemoveAt(pManager.getPositions().Count - 1);
									UpDate.Start();
								}
								break;
							case "Обновить":
								UpDate.Start();
								break;
							case "Очистить":
								Clear();
								break;
							case "Распознать":
								Find();
								break;
							case "Добавить нейрон":
								AddNeuron();
								break;
						}
					};
			}
			context_menu = new ContextMenu(m_items);

			pBox.ContextMenu = context_menu;
			#endregion
			#region mouse
			bool b_m_Down = false;
			this.pBox.MouseDown += delegate(object o, MouseEventArgs e)
			{
				b_m_Down = true;

				this.UpDate.Start();
			};
			this.pBox.MouseMove += delegate(object o, MouseEventArgs e)
			{
				if (b_m_Down)
				{
					pManager.MouseMoveIfMouseDown(e);
				}
			};
			this.pBox.MouseUp += delegate(object o, MouseEventArgs e)
			{
				pManager.MouseUp(e);

				b_m_Down = false;
				this.UpDate.Stop();
				pBox.Invalidate();
			};
			#endregion
			#region create interface
			this.textBox = new RichTextBox();
			base.Controls.Add(this.textBox);
			this.textBox.BorderStyle = BorderStyle.None;
			this.textBox.Location = new Point(5, 5);
			this.textBox.Size = new Size(base.Width - 90, base.Height - 65);
			this.textBox.BackColor = Color.FromArgb(150, 150, 150);
			this.textBox.Font = new Font("Arial", 35f, FontStyle.Bold);
			this.textBox.ForeColor = Color.FromArgb(0, 120, 200);
			int num = 5;
			this.but = new Button[5];
			for (int i = 0; i < this.but.Length; i++)
			{
				this.but[i] = new Button();
				base.Controls.Add(this.but[i]);
				this.but[i].Size = new Size(base.Width / 4 - 2, 50);
				this.but[i].FlatAppearance.BorderSize = 0;
				this.but[i].FlatStyle = FlatStyle.Flat;
				this.but[i].BackColor = this.color_buttons_Background;
				this.but[i].FlatAppearance.MouseOverBackColor = this.color_buttons_MouseOverBackColor;
				this.but[i].Location = new Point(num, this.textBox.Bottom + 5);
				this.but[i].TextAlign = ContentAlignment.MiddleCenter;
				this.but[i].Font = new Font("Arial", 15f);
				this.but[i].ForeColor = this.color_buttons_Fore;
				this.but[i].Click += delegate(object param0, EventArgs param1)
				{
					for (int j = 0; j < this.but.Length; j++)
					{
						if (this.but[j].ContainsFocus)
						{
							switch (j)
							{
								case 0:
									this.Find();
									break;
								case 1:
									this.AddNeuron();
									break;
								case 2:
									if (onClickSaveNN != null)
										onClickSaveNN();
									break;
								case 3:
									this.Load();
									break;
								case 4:
									this.Clear();
									break;
							}
						}
					}
				};
				num += this.but[i].Width;
			}
			this.but[0].Text = "Распознать";
			this.but[1].Text = "Добавить";
			this.but[2].Text = "Сохранить";
			this.but[3].Text = "Загрузить";
			this.but[4].Location = new Point(this.textBox.Right + 5, 5);
			this.but[4].Size = new Size(75, this.textBox.Height);
			this.but[4].Font = new Font("Arial", 30f, FontStyle.Bold);
			this.but[4].Text = "C";
			#endregion
			
		}
		#endregion
		#region Find
		private void Find()
		{
			#region Find rect
			/*
			RectangleF img_rect = new Rectangle(-100, -100, 1, 1);
			Boolean b_x = false, b_y = false, b_r = false, b_b = false;
			Bitmap img = new Bitmap(this.pBox.Image);
			for (int y = 0; y < img.Height; y++)
			{
				for (int x = 0; x < img.Width; x++)
				{
					Color color = img.GetPixel(x, y);

					if (color.R > 130 && color.G > 130 && color.B > 130)
					{
						color = Color.White;
					}
					else
					{
						color = Color.Black;
					}

					if (color == Color.Black)
					{
						if (!b_y)
						{
							img_rect.Y = y;
							b_y = true;
						}
						img_rect.Height = y;
					}
				}
			}
			for (int x = 0; x < img.Width; x++)
			{
				for (int y = 0; y < img.Height; y++)
				{
					Color color = img.GetPixel(x, y);

					if (color.R > 130 && color.G > 130 && color.B > 130)
					{
						color = Color.White;
					}
					else
					{
						color = Color.Black;
					}

					if (color == Color.Black)
					{
						if (!b_x)
						{
							img_rect.X = x;							
							b_x = true;
						}
						img_rect.Width = x;
					}
				}
			}

			img_rect.Height -= img_rect.Top;
			img_rect.Width -= img_rect.Left;
			*/
			#endregion

			//		g.DrawRectangle(new Pen(Color.Red, 2), img_rect.X, img_rect.Y, img_rect.Width, img_rect.Height);

			//		Bitmap IMG = new Bitmap((int)img_rect.Width, (int)img_rect.Height);
			//		Graphics G = Graphics.FromImage(IMG);
			//		G.DrawImage(img, 0, 0, img_rect, GraphicsUnit.Pixel);

			//	pBox.Image = new Bitmap(IMG, pBox.Width, pBox.Height);

			//	pManager.Zoom(1.5f, 1.5f);
			pManager.Draw(g);

			pBox.Invalidate();

			Bitmap bitmap = new Bitmap(this.pBox.Image);
			this.X.Clear();
			for (int i = 0; i < this.pBox.Width; i++)
			{
				for (int j = 0; j < this.pBox.Height; j++)
				{
					Color color = bitmap.GetPixel(i, j);
					if (color.R > 130 && color.G > 130 && color.B > 130)
					{
						color = Color.White;
					}
					else
					{
						color = Color.Black;
					}
					this.X.Add((float)color.ToArgb());
				}
			}

			//		this.textBox.Text = this.ns.Find(this.X);
			if (onClickRecognize != null)
				onClickRecognize((Bitmap)pBox.Image, X);
		}
		#endregion
		#region AddNeuron
		private void AddNeuron()
		{
			try
			{
				string text = this.textBox.Text;
				Bitmap bitmap = new Bitmap(this.pBox.Image);
				float _x = 0;
				this.X.Clear();
				for (int i = 0; i < this.pBox.Width; i++)
				{
					for (int j = 0; j < this.pBox.Height; j++)
					{
						Color color = bitmap.GetPixel(i, j);
						if (color.R > 130 && color.G > 130 && color.B > 130)
						{
							color = Color.White;
						}
						else
						{
							color = Color.Black;
						}
						this.X.Add((float)color.ToArgb());
					}
				}
				//		this.textBox.Text = this.ns.AddNeuron(this.X, text);
				if (onClickAddNeuron != null)
					onClickAddNeuron(image, X);
			}
			catch (Exception ex) 
			{
				LogManager.Write(ex); 
			}
		}
		#endregion
		#region Save
		public void Save(String name)
		{
			try
			{
				ZipManager.CreateBuffer();
				XmlSerializer serializer = new XmlSerializer(typeof(SER_NEURON));
				string name_dir = name;
				List<NeuralNetwork.Neuron> ns = ((Form1)form).net.getNeurons();
				this.LoadingElement = 0;
				this.CountElement = ((Form1)form).net.neurons_count;
				this.pManager.Clear();
				this.loading.Enable = true;
				List<SER_NEURON> sns = new List<SER_NEURON>();

				if (!Directory.Exists(ZipManager.path_buffer + name_dir))
					Directory.CreateDirectory(ZipManager.path_buffer + name_dir);

				System.Threading.Thread thread = new System.Threading.Thread(delegate()
				{
					for (int i = 0; i < ns.Count; i++)
					{
						SER_NEURON n = new SER_NEURON();
						n.userDate = ns[i].userDate;
						int present_count = 0;
						float prev_w = 0;

						if (i > 1)
						{
							//	prev_w = ns[i].w[0];
							for (int f = 0; f < ns[i].w.Count; f++)
								if (ns[i].w[f] == ns[i].w[0])
									present_count++;
							//	n.count_power.Add(present_count);
							//	MessageBox.Show(""+present_count);
						}

						for (int f = 0; f < ns[i].w.Count; f++)
						{
							if (prev_w != ns[i].w[f])
							{
								n.first_power.Add(ns[i].w[f]);
								n.count_power.Add(present_count);

								present_count = 0;
							}

							prev_w = ns[i].w[f];
							present_count++;
						}

						if (i > 0)
						{
							n.count_power.RemoveAt(0);

							for (int f = 0; f < ns[i].w.Count; f++)
								if (ns[i].w[f] == ns[i].w[ns[i].w.Count - 1])
									present_count++;
							n.count_power.Add(present_count / 2);
						}

						sns.Add(n);
					}

					for (int i = 0; i < ns.Count; i++)
					{
						using (Stream stream = new FileStream(string.Concat(new object[] 
						{
							
							ZipManager.path_buffer,
							name_dir,
							"\\",
							i,
							"_neuron",
							".xml"
						}), FileMode.Create))
						{
							serializer.Serialize(stream, sns[i]);
							this.LoadingElement++;
						}

						if (this.LoadingElement == this.CountElement)
						{
							this.LoadingElement = 0;
							this.loading.Enable = false;
							this.g.Clear(this.pBox.BackColor);
							this.pBox.Image = this.image;
						}
					}

					ZipManager.CompressionFile(ZipManager.path_buffer + name_dir, ZipManager.path_brains + name_dir + "[" + ns.Count + "]" + ".zip");
					ZipManager.ClearBuffer();
				});
				thread.Start();
			}
			catch (Exception ex) 
			{
				LogManager.Write(ex);
				MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!"); 
			}

			#region old
			/*
			XmlSerializer serializer = new XmlSerializer(typeof(NeuralNetwork.Neuron));
			if (!Directory.Exists("Brains\\"))
			{
				Directory.CreateDirectory("Brains\\");
			}
			string name_dir = (Directory.GetDirectories("Brains\\").Length + 1).ToString();
			Directory.CreateDirectory("Brains\\" + name_dir);
			List<NeuralNetwork.Neuron> ns = ((Form1)form).net.getNeurons();
			this.LoadingElement = 0;
			this.CountElement = ((Form1)form).net.neurons_count;
			this.pManager.Clear();
			this.loading.Enable = true;
			System.Threading.Thread thread = new System.Threading.Thread(delegate()
			{
				for (int i = 0; i < ns.Count; i++)
				{
					if (ns[i].userDate != null || ns[i].userDate != "" || ns[i].userDate != string.Empty)
					{
						using (Stream stream = new FileStream(string.Concat(new object[]
						{
							"Brains\\",
							name_dir,
							"\\N",
							i,
							"_neuron[",
							ns[i].userDate,
							"].xml"
						}), FileMode.Create))
						{
							serializer.Serialize(stream, ns[i]);
							this.LoadingElement++;
						}
					}
					if (this.LoadingElement == this.CountElement)
					{
						this.LoadingElement = 0;
						this.loading.Enable = false;
						this.g.Clear(this.pBox.BackColor);
						this.pBox.Image = this.image;
					}
				}
			});
			thread.Start();
			*/
			#endregion
		}
		#endregion
		#region Load
		private void Load()
		{
			if (onClickLoadNeurons != null)
				onClickLoadNeurons();

			#region old
			/*
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Brains\\";
			DialogResult dialogResult = openFileDialog.ShowDialog();
			List<NeuralNetwork.Neuron> nns = new List<NeuralNetwork.Neuron>();
			if (dialogResult == DialogResult.OK)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(NeuralNetwork.Neuron));
				string[] names = openFileDialog.FileNames;
				this.LoadingElement = 0;
				this.CountElement = names.Length;
				this.pManager.Clear();
				this.loading.Enable = true;
				System.Threading.Thread thread = new System.Threading.Thread(delegate()
				{
					for (int i = 0; i < names.Length; i++)
					{
						using (Stream stream = new FileStream(names[i], FileMode.Open))
						{
							nns.Add(new NeuralNetwork.Neuron());
							nns[nns.Count - 1] = (NeuralNetwork.Neuron)serializer.Deserialize(stream);
							this.LoadingElement++;
						}
					}
					if (this.LoadingElement == this.CountElement)
					{
						this.LoadingElement = 0;
						this.loading.Enable = false;
						this.g.Clear(this.pBox.BackColor);
						this.pBox.Image = this.image;
					}
					//	for (int i = 0; i < nns.Count; i++)
					//	{
					//		if (nns[i].userDate != "" || nns[i].userDate != null || nns[i].userDate != string.Empty)
					//		{
					//			this.ns.neurons.Add(new Neuron());
					//			this.ns.neurons[this.ns.neurons.Count - 1].W = nns[i].W;
					//			this.ns.neurons[this.ns.neurons.Count - 1].Symbol = nns[i].Symbol;
					//		}
					//	}
					((Form1)form).net.setNeurons(nns);
				});
				thread.Start();
			}
			*/
			#endregion
		}
		#endregion
		#region LoadNotWindow
		public void LoadNoWindow(String path)
		{
			try
			{
				// Мы распоковали данные в буфер в классе BrainsPanel на событии
				// добавления нейронов
				List<NeuralNetwork.Neuron> nns = new List<NeuralNetwork.Neuron>();
				XmlSerializer serializer = new XmlSerializer(typeof(SER_NEURON));
				string[] names = Directory.GetFiles(ZipManager.path_buffer);
				this.LoadingElement = 0;
				this.CountElement = names.Length;
				this.pManager.Clear();
				this.loading.Enable = true;

				System.Threading.Thread thread = new System.Threading.Thread(delegate()
				{
					try
					{
						for (int i = 0; i < names.Length; i++)
						{
							using (Stream stream = new FileStream(names[i], FileMode.Open))
							{
								NeuralNetwork.Neuron n = new NeuralNetwork.Neuron();
								SER_NEURON sns = (SER_NEURON)serializer.Deserialize(stream);

								n.userDate = sns.userDate;

								for (int j = 0; j < sns.first_power.Count; j++)
								{
									for (int c = 0; c < sns.count_power[j]; c++)
									{
										n.w.Add(sns.first_power[j]);
									}
								}

								nns.Add(n);

								this.LoadingElement++;
							}
						}

						((Form1)form).net.setNeurons(nns);

						if (this.LoadingElement == this.CountElement)
						{
							if (onEndLoadFiles != null)
								onEndLoadFiles(nns);
						}
						// Придется очистить буфер т.к. мы его в BrainsPanel не очищали
						ZipManager.ClearBuffer();
					}
					catch (Exception ex)
					{
						LogManager.Write(ex);
						MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
					}
				});

				onEndLoadFiles += (e) =>
				{
					this.LoadingElement = 0;
					this.loading.Enable = false;
					this.g.Clear(this.pBox.BackColor);
					this.pBox.Image = this.image;
				};
				thread.Start();
			}
			catch (Exception ex)
			{
				LogManager.Write(ex);
				MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
			}
			#region old
			/*
			List<NeuralNetwork.Neuron> nns = new List<NeuralNetwork.Neuron>();

			XmlSerializer serializer = new XmlSerializer(typeof(SER_NEURON));
			string[] names = Directory.GetFiles(path);
			this.LoadingElement = 0;
			this.CountElement = names.Length;
			this.pManager.Clear();
			this.loading.Enable = true;

			System.Threading.Thread thread = new System.Threading.Thread(delegate()
			{
				for (int i = 0; i < names.Length; i++)
				{
					using (Stream stream = new FileStream(names[i], FileMode.Open))
					{
						NeuralNetwork.Neuron n = new NeuralNetwork.Neuron();
						SER_NEURON sns = (SER_NEURON)serializer.Deserialize(stream);

						n.userDate = sns.userDate;

						for (int j = 0; j < sns.first_power.Count; j++)
						{
							for (int c = 0; c < sns.count_power[j]; c++)
							{
								n.w.Add(sns.first_power[j]);
							}
						}

						nns.Add(n);

						this.LoadingElement++;
					}
				}

				((Form1)form).net.setNeurons(nns);

				if (this.LoadingElement == this.CountElement)
				{
					if (onEndLoadFiles != null)
						onEndLoadFiles(nns);
				}
			});

			onEndLoadFiles += (e) =>
			{
				this.LoadingElement = 0;
				this.loading.Enable = false;
				this.g.Clear(this.pBox.BackColor);
				this.pBox.Image = this.image;
			};
			thread.Start();
			*/
			#endregion
		}
		#endregion
		#region Clear
		public void Clear()
		{
			try
			{
				this.textBox.Text = "";
				this.pManager.Clear();
				this.g.Clear(this.pBox.BackColor);
				this.pBox.Image = this.image;
			}
			catch (Exception ex) 
			{
				LogManager.Write(ex);
				MessageBox.Show("Данные загружены.\nОкно небыло обновлено, для этого нажмите кнопку \"Очистить\"", "Окно не обновлено"); 
			}
		}
		#endregion
		#region UpDate
		private void UpDate_Tick(object sender, EventArgs e)
		{
			this.g.Clear(this.pBox.BackColor);

			pManager.Draw(g);

			this.loading.Text = this.LoadingElement + "\\" + this.CountElement;
			this.loading.Draw(this.g);
			this.pBox.Image = this.image;
		}
		#endregion

		#region SER_NEURON
		public class SER_NEURON
		{
			public String userDate;
			public List<float> first_power;
			public List<int> count_power;

			public SER_NEURON()
			{
				first_power = new List<float>();
				count_power = new List<int>();
			}
		}
		#endregion
		#region PaintManager
		public class PaintManager
		{
			#region variables
			public Pen pen;
			private Graphics g;
			private List<List<PointF>> pos;
			#endregion
			#region PaintManager
			public PaintManager(Pen pen)
			{
				this.pen = pen;
				pos = new List<List<PointF>>();
				pos.Add(new List<PointF>());
				this.pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
				this.pen.Brush = new SolidBrush(pen.Color);
				this.pen.Width = 4;
			}
			#endregion
			#region MouseMoveIfMouseDown
			public void MouseMoveIfMouseDown(MouseEventArgs e)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
					pos[pos.Count - 1].Add(e.Location);
			}
			#endregion
			#region MouseUp
			public void MouseUp(MouseEventArgs e)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
					pos.Add(new List<PointF>());
			}
			#endregion
			#region Draw
			public void Draw(Graphics g)
			{
				this.g = g;

				for (int k = 0; k < pos.Count; k++)
					for (int i = 0, j = 1; i < pos[k].Count; j = i++)
					{
						if (j < pos[k].Count - 1)
						{
							g.DrawLine(pen, pos[k][i], pos[k][j]);
						}
					}
			}
			#endregion
			#region clear
			public void Clear()
			{
				pos.Clear();
				pos.Add(new List<PointF>());
			}
			#endregion
			#region Zoom
			public void Zoom(float x, float y)
			{
				System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Scale(x, y);

				g.Transform = matrix;
			}
			#endregion
			#region getPositions
			public List<PointF> getPositions()
			{
				List<PointF> p = new List<PointF>();

				for (int i = 0; i < pos.Count; i++)
					for (int j = 0; j < pos[i].Count; i++)
						p.Add(pos[i][j]);

				return p;
			}
			#endregion
			#region RemoveAt
			public void RemoveAt(int i)
			{
				pos.RemoveAt(i);
			}
			#endregion
			public void setBuffer(List<PointF> p)
			{

			}
		}
		#endregion
	}
}
