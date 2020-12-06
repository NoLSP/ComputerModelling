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
	public partial class EulerMethod : Form
	{
		public EulerMethod()
		{
			InitializeComponent();

			DrawGraph();
		}

		private PointD EulerFunc(double x, double y)
		{
			var h = 0.01;
			var delta = 3;
			return new PointD(x + h * y, y + h * (delta * y * (1 - x * x) - x));
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
			for (double i = 0; i <= 200; i += 0.01)
			{
				// добавим в список точку
				currPoint = EulerFunc(currPoint.X, currPoint.Y);
				list.Add(currPoint.X, currPoint.Y);
			}

			// Создадим кривую с названием "MyFunction", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("MyFunction", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}