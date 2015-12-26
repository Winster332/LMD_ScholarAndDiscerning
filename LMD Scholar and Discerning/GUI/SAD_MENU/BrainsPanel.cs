using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU
{
	public class BrainsPanel : Panel
	{
		#region variable
		private VScrollBar scrBar;
		private Control parent;
		public NeuronInformationPanel[] nip;
		public delegate void AddNeuron(NeuronInformationPanel view);
		public event AddNeuron addNeuron;
		#endregion
		#region ProcessPanel
		public BrainsPanel(Control parent)
		{
			this.parent = parent;
			parent.Controls.Add(this);
			this.Location = new Point(0, 30);
			this.Size = new Size(parent.Width, parent.Height - 36);
			this.nip = new NeuronInformationPanel[0];

			scrBar = new VScrollBar();
			this.Controls.Add(scrBar);
			scrBar.Location = new Point(this.Width - scrBar.Width, 0);
			scrBar.Size = new Size(20, this.Height);

			UpDateInterface();
		}
		#endregion
		#region UpDateInterface
		public void UpDateInterface()
		{
			try
			{
				for (int i = 0; i < nip.Length; i++)
					nip[i].Dispose();

				String[] paths = Directory.GetFiles("Brains\\");
				nip = new NeuronInformationPanel[paths.Length];
				int indent = 2;
				for (int i = 0; i < paths.Length; i++)
				{
					String name = paths[i].Substring("Brains1".Length, paths[i].Length - "Brains1".Length);
					name = name.Substring(0, name.IndexOf("["));
					String count_ns = paths[i].Substring(paths[i].IndexOf("[") + 1, paths[i].Length - paths[i].IndexOf("[") - 6);

					nip[i] = new NeuronInformationPanel();
					this.Controls.Add(nip[i]);
					nip[i].path = paths[i];
					nip[i].Location = new Point(0, indent);
					nip[i].Text = "Имя: " + name;
					nip[i].Text_Info = " Нейронов: " + count_ns;

					nip[i].Add += () =>
						{
							for (int fuck = 0; fuck < nip.Length; fuck++)
								if (nip[fuck].button_add.ContainsFocus)
								{
									// Тут мы распаковываем данные в буфер и удаление произойдет
									// в классе LMD_Interface после добавления всех нейронов в нейросеть
									// вызовется функция очищения буфера
									ZipManager.CreateBuffer();
									ZipManager.DecompressionFile(nip[fuck].path, ZipManager.path_buffer);
									// После того как мы распоковали данные, 
									// можно приступить к их загрузке в нейронную сеть.
									if (addNeuron != null)
										addNeuron(nip[fuck]);

									UpDateInterface();
								}
						};
					nip[i].Del += () =>
					{
						for (int fuck = 0; fuck < nip.Length; fuck++)
							if (nip[fuck].button_del.ContainsFocus)
							{
								Directory.Delete(nip[fuck].path, true);
								nip[fuck].Dispose();
								UpDateInterface();
							}
					};

					indent += nip[i].Height + 1;
				}

				scrBar.Maximum = paths.Length;
				int prev_value = 0;
				scrBar.ValueChanged += (o, e) =>
					{
						for (int i = 0; i < nip.Length; i++)
						{
							if (scrBar.Value > prev_value)
								nip[i].Top -= nip[i].Height + 1;
							else nip[i].Top += nip[i].Height + 1;
						}

						prev_value = scrBar.Value;
					};
			}
			catch (Exception ex) 
			{
				LogManager.Write(ex);
				MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!"); 
			}
		}
		#endregion
	}
}
