using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning
{
	public partial class Form1 : Form
	{
		#region variables
		public NeuralNetwork.Net net;
		public GUI.LMD_TopBoard topBoard;
		public GUI.LMD_Interface myInterface;
		public GUI.PanelCorrect panelCorrect;
		#endregion
		#region Form1
		public Form1()
		{
			InitializeComponent();

			this.BackColor = Color.FromArgb(0, 140, 220);
		}
		#endregion
		#region Form1_Load
		private void Form1_Load(object sender, EventArgs e)
		{
			#region Initialize
			List<float> last_x = new List<float>();

			topBoard = new GUI.LMD_TopBoard(this, false);
			panelCorrect = new GUI.PanelCorrect(this);
			myInterface = new GUI.LMD_Interface(this);
			
			net = new NeuralNetwork.Net(myInterface.pBox.Width * myInterface.pBox.Height);
			#endregion
			#region my interface
			#region Событие на кнопку Распознать
			myInterface.onClickRecognize += (img, x) =>
				{
					last_x = x;
					GUI.CorrectEffect.getRectangle(img);
					myInterface.textBox.Text = net.Recognize(x);
				};
			#endregion
			#region Событие на кнопку Добавить
			myInterface.onClickAddNeuron += (img, w) =>
				{
					net.AddNeuron(w, myInterface.textBox.Text);
				};
			#endregion
			#region Событие на окончание загрузки файлов
			myInterface.onClickLoadNeurons += () =>
				{
					GUI.SAD_MENU.Form_menu f = new GUI.SAD_MENU.Form_menu();
					f.Show();
					f.workspace.OpenLocalDate();
					f.workspace.p_process.Calculate(net.getNeurons());

					f.workspace.onClickButton += (location) =>
					{
						if (location == GUI.SAD_MENU.PanelWorkspace.LOCATION_STATE.processes)
						{
							f.workspace.p_process.Calculate(net.getNeurons());
						}
					};
					f.workspace.p_brains.addNeuron += (NIP) =>
					{
						f.Close();
						myInterface.LoadNoWindow(NIP.path);
					};
				};
			#endregion
			#region Событие не сохранение НС
			myInterface.onClickSaveNN += () =>
				{
					GUI.SAD_MENU.Form_save fs = new GUI.SAD_MENU.Form_save();
					fs.Show();
					fs.label_count_neurons.Text = net.neurons_count.ToString();

					fs.button_save.Click += (o, ee) =>
						{
							if (fs.richTextBox_name.Text != "<Имя>")
							{
								myInterface.Save(fs.richTextBox_name.Text);
								fs.Close();								
							}
							else MessageBox.Show("Неверно введено название", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
						};
				};
			#endregion
			#endregion
			#region top board
			#region Событие на кнопку открытия панели корректировки весов
			topBoard.butClick += () =>
				{
					if (!panelCorrect.Visible)
						panelCorrect.setVisible(true);
					else if (panelCorrect.Visible)
						panelCorrect.setVisible(false);
				};
			#endregion
			#region Событие на кнопку корректировки весов
			topBoard.butClickMenu += () =>
				{
					GUI.SAD_MENU.Form_menu f = new GUI.SAD_MENU.Form_menu();
					f.Show();
					f.workspace.p_process.Calculate(net.getNeurons());

					f.workspace.onClickButton += (location) =>
					{
						if (location == GUI.SAD_MENU.PanelWorkspace.LOCATION_STATE.processes)
						{
							f.workspace.p_process.Calculate(net.getNeurons());
						}
					};
					f.workspace.p_brains.addNeuron += (NIP) =>
						{
							myInterface.LoadNoWindow(NIP.path);
						};
				};
			#endregion
			#endregion
			#region panel correct
			panelCorrect.onButtonClick += (flag) =>
				{
					panelCorrect.setVisible(false);
					int delta = 0;

					if (flag)
						delta = 1;
					else if (!flag)
						delta = -1;
						
					net.Correct(net.last_index_neuron, last_x, 0.1f, delta);
					myInterface.textBox.Text = net.Recognize(last_x);
				};
			#endregion
		}
		#endregion
	}
}
