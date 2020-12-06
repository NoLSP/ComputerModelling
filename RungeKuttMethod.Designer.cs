using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleSignal
{
	partial class RungeKuttMethod
	{
		TextBox pointXTextBox;
		TextBox pointYTextBox;
		TextBox deltaTextBox;
		TextBox endDelta;

		Button AddButton;
		Button TestExampleButton;
		Button MyExampleButton;
		Button DeleteButton;
		Button SearchCicle;
		Button DrawPeriod;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container ();
			this.zedGraph = new ZedGraph.ZedGraphControl ();
			this.SuspendLayout ();
			// 
			// zedGraph
			// 
			this.zedGraph.Dock = System.Windows.Forms.DockStyle.Left;
			this.zedGraph.Location = new System.Drawing.Point (0, 0);
			this.zedGraph.Name = "zedGraph";
			this.zedGraph.ScrollGrace = 0;
			this.zedGraph.ScrollMaxX = 0;
			this.zedGraph.ScrollMaxY = 0;
			this.zedGraph.ScrollMaxY2 = 0;
			this.zedGraph.ScrollMinX = 0;
			this.zedGraph.ScrollMinY = 0;
			this.zedGraph.ScrollMinY2 = 0;
			this.zedGraph.Size = new System.Drawing.Size (592, 471);
			this.zedGraph.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (592, 471);
			this.Size = new System.Drawing.Size(900, 600);
			this.Controls.Add (this.zedGraph);
			this.Name = "Runge-Kutt";
			this.Text = "Runge-Kutt";
			this.ResumeLayout (false);

			AddButtons();
		}

		void AddButtons()
        {
			//Buttons
			pointXTextBox = new TextBox();
			pointXTextBox.Size = new Size(50, 50);
			pointXTextBox.Text = "1";
			pointXTextBox.Location = new Point(750, 100);
			this.Controls.Add(pointXTextBox);

			pointYTextBox = new TextBox();
			pointYTextBox.Size = new Size(50, 50);
			pointYTextBox.Text = "0";
			pointYTextBox.Location = new Point(750, 130);
			this.Controls.Add(pointYTextBox);

			deltaTextBox = new TextBox();
			deltaTextBox.Size = new Size(50, 50);
			deltaTextBox.Text = "0";
			deltaTextBox.Location = new Point(640, 200);
			this.Controls.Add(deltaTextBox);

			endDelta = new TextBox();
			endDelta.Size = new Size(50, 50);
			endDelta.Text = "10";
			endDelta.Location = new Point(800, 200);
			this.Controls.Add(endDelta);

			var pointLabel = new Label();
			pointLabel.Text = "Начальная точка:";
			pointLabel.Size = new Size(300, 30);
			pointLabel.Location = new Point(630, 50);
			pointLabel.Font = new Font(pointLabel.Font.Name, 18, FontStyle.Regular);
			this.Controls.Add(pointLabel);

			var pointXLabel = new Label();
			pointXLabel.Text = "X0:";
			pointXLabel.Size = new Size(50, 30);
			pointXLabel.Location = new Point(705, 95);
			pointXLabel.Font = new Font(pointXLabel.Font.Name, 15, FontStyle.Regular);
			this.Controls.Add(pointXLabel);

			var pointYLabel = new Label();
			pointYLabel.Text = "Y0:";
			pointYLabel.Size = new Size(50, 30);
			pointYLabel.Location = new Point(705, 125);
			pointYLabel.Font = new Font(pointYLabel.Font.Name, 15, FontStyle.Regular);
			this.Controls.Add(pointYLabel);

			var deltaLabel = new Label();
			deltaLabel.Text = "< Delta <";
			deltaLabel.Size = new Size(100, 30);
			deltaLabel.Location = new Point(700, 195);
			deltaLabel.Font = new Font(deltaLabel.Font.Name, 15, FontStyle.Regular);
			this.Controls.Add(deltaLabel);

			AddButton = new Button();
			AddButton.Size = new Size(100, 50);
			AddButton.Location = new Point(620, 270);
			AddButton.Text = "Добавить кривую";
			AddButton.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
			AddButton.Click += AddButton_Click;
			this.Controls.Add(AddButton);

			TestExampleButton = new Button();
			TestExampleButton.Size = new Size(100, 50);
			TestExampleButton.Location = new Point(620, 350);
			TestExampleButton.Text = "Тестовый пример";
			TestExampleButton.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
			TestExampleButton.Click += TestExampleButton_Click;
			this.Controls.Add(TestExampleButton);

			MyExampleButton = new Button();
			MyExampleButton.Size = new Size(100, 50);
			MyExampleButton.Location = new Point(750, 350);
			MyExampleButton.Text = "Мой пример";
			MyExampleButton.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
			MyExampleButton.Click += MyExampleButton_Click;
			this.Controls.Add(MyExampleButton);

			DeleteButton = new Button();
			DeleteButton.Size = new Size(100, 50);
			DeleteButton.Location = new Point(750, 270);
			DeleteButton.Text = "Удалить кривую";
			DeleteButton.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
			DeleteButton.Click += DeleteButton_Click; ;
			this.Controls.Add(DeleteButton);

            SearchCicle = new Button();
            SearchCicle.Size = new Size(100, 50);
            SearchCicle.Location = new Point(620, 420);
            SearchCicle.Text = "Найти цикл";
            SearchCicle.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
            SearchCicle.Click += SearchCicle_Click;
            this.Controls.Add(SearchCicle);

			DrawPeriod = new Button();
			DrawPeriod.Size = new Size(100, 50);
			DrawPeriod.Location = new Point(750, 420);
			DrawPeriod.Text = "Периоды циклов";
			DrawPeriod.Font = new Font(deltaLabel.Font.Name, 10, FontStyle.Regular);
            DrawPeriod.Click += DrawPeriod_Click; ;
			this.Controls.Add(DrawPeriod);
		}

        private void DrawPeriod_Click(object sender, EventArgs e)
        {
			DrawPeriods(double.Parse(pointXTextBox.Text.Replace('.', ',')),
					  double.Parse(pointYTextBox.Text.Replace('.', ',')),
					  double.Parse(deltaTextBox.Text.Replace('.', ',')),
					  double.Parse(endDelta.Text.Replace('.', ',')));
		}

        private void SearchCicle_Click(object sender, EventArgs e)
        {
            DrawCicle(double.Parse(pointXTextBox.Text.Replace('.', ',')),
                      double.Parse(pointYTextBox.Text.Replace('.', ',')),
                      double.Parse(deltaTextBox.Text.Replace('.', ',')));
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
			var list = pane.CurveList;
			if (list.Count != 0)
				list.RemoveAt(list.Count-1);
			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange();

			// Обновляем график
			zedGraph.Invalidate();
		}

        private void MyExampleButton_Click(object sender, System.EventArgs e)
        {
			f = (x, y) => y;
			g = (x, y) => double.Parse(deltaTextBox.Text.Replace('.', ',')) * y * (1 - x*x) - x;
		}

		private void TestExampleButton_Click(object sender, System.EventArgs e)
        {
			f = (x, y) => y;
			g = (x, y) => -1 * x;
		}

        private void AddButton_Click(object sender, System.EventArgs e)
        {
			DrawGraph(double.Parse(pointXTextBox.Text.Replace('.', ',')), 
					  double.Parse(pointYTextBox.Text.Replace('.', ',')), 
					  double.Parse(deltaTextBox.Text.Replace('.', ',')));
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph;
	}
}

