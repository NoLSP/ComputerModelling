using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace SimpleSignal
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			DrawGraph();
		}

		private PointD TestFunc(double x, double y)
		{
			var h = 0.01;
			return new PointD(x + h * y, y + h * (-1) * x);
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			//double xmin = -50;
			//double xmax = 50;
			var currPoint = new PointD(1, 0);
			list.Add(currPoint.X, currPoint.Y);
			// Заполняем список точек
			for (double i = 0; i <= 100; i += 0.01)
			{
				// добавим в список точку
				currPoint = TestFunc(currPoint.X, currPoint.Y);
				list.Add(currPoint.X, currPoint.Y);
			}

			// Создадим кривую с названием "Sinc", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}