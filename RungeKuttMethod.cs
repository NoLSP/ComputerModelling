using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace SimpleSignal
{
	public partial class RungeKuttMethod : Form
	{
		GraphPane pane;

		Func<double, double, double> f = (x, y) => y;
		Func<double, double, double> g = (x, y) => -1 * x;

        double E = 0.001;
        double stableX = 0;
        double stableY = 0;
        double h = 0.01;

        public RungeKuttMethod()
		{
			InitializeComponent();
		}

		private PointD RungeKuttFunc(double x0, double y0, double delta)
		{
			var k1 = h * f(x0, y0);
			var l1 = h * g(x0, y0);
			var k2 = h * f(x0 + k1 / 2, y0 + l1 / 2);
			var l2 = h * g(x0 + k1 / 2, y0 + l1 / 2);
			var k3 = h * f(x0 + k2 / 2, y0 + l2 / 2);
			var l3 = h * g(x0 + k2 / 2, y0 + l2 / 2);
			var k4 = h * f(x0 + k3, y0 + l3);
			var l4 = h * g(x0 + k3, y0 + l3);
			double k = 0.166666667;

			var resX = x0 + k * (k1 + 2 * k2 + 2 * k3 + k4);
			var resY = y0 + k * (l1 + 2 * l2 + 2 * l3 + l4);
			return new PointD(resX, resY);
		}

		private void DrawGraph (double x0, double y0, double delta)
		{
			// Получим панель для рисования
			pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			//pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();
			var currPoint = new PointD(x0, y0);
			list.Add(currPoint.X, currPoint.Y);

			// Заполняем список точек
			for (double i = 0; i <= 2000; i++)
			{
				// добавим в список точку
				currPoint = RungeKuttFunc(currPoint.X, currPoint.Y, delta);
				list.Add(currPoint.X, currPoint.Y);
			}

			var rnd = new Random();
			// Создадим кривую с названием "MyFunction", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("MyFunction", list, Color.FromArgb(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255)), 
											  SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}

        public PointPairList GetCiclePointsList(double x0, double y0, double delta)
        {
            var XPs = new List<double>();
            var currPoint = new PointD(x0, y0);

            for (double i = 0; i <= 20000; i++)
            {
                var newPoint = RungeKuttFunc(currPoint.X, currPoint.Y, delta);
                if (currPoint.X > stableX && newPoint.X > stableX && (newPoint.Y - stableY) * (currPoint.Y - stableY) < 0)
                {
                    //Выполнилось условие отбора
                    var Xp = GetXp(currPoint, newPoint);
                    XPs.Add(Xp);
                    if (XPs.Count >= 2 && Math.Abs(XPs[XPs.Count - 1] - XPs[XPs.Count - 2]) < E)
                    {
                        currPoint = new PointD(Xp, stableY);
                        return DrawCicleWhenFind(currPoint, delta);
                    }
                }
                currPoint = newPoint;
            }
            return new PointPairList();
        }

        public void DrawPeriods(double x0, double y0, double startDelta, double endDelta)
        {
            pane = zedGraph.GraphPane;
            var pointsList = new PointPairList();

            for (double i = startDelta; i <= endDelta; i+=0.001)
            {
                g = (x, y) => i * y * (1 - x * x) - x;
                var period = GetCiclePointsList(x0, y0, i);
                var t = period.Count * h;
                pointsList.Add(i, t);
            }

            var rnd = new Random();
            LineItem myCurve = pane.AddCurve("MyFunction", pointsList, Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                                              SymbolType.None);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        public void DrawCicle(double x0, double y0, double delta)
        {
            pane = zedGraph.GraphPane;
            var XPs = new List<double>();
            var currPoint = new PointD(x0, y0);
            var list = GetCiclePointsList(x0, y0, delta);
            var rnd = new Random();
            LineItem myCurve = pane.AddCurve("MyFunction", list, Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                                              SymbolType.None);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private PointPairList DrawCicleWhenFind(PointD curr, double delta)
        {
			var result = new PointPairList();
            result.Add(curr.X, curr.Y);
            var currPoint = new PointD(curr.X, curr.Y);

            for (double i = 0; i <= 5000; i++)
            {
                var newPoint = RungeKuttFunc(currPoint.X, currPoint.Y, delta);
                if (currPoint.X > stableX && newPoint.X > stableX && (newPoint.Y - stableY) * (currPoint.Y - stableY) < 0)
                {
                    //Выполнилось условие отбора
                    break;
                }
                result.Add(newPoint.X, newPoint.Y);
                currPoint = newPoint;
            }
            return result;
        }

        private double GetXp(PointD currPoint, PointD newPoint)
        {
            double b = 1.0 / (currPoint.X - newPoint.X) * (newPoint.Y - currPoint.Y * newPoint.X);
            double k = (currPoint.Y - b) / currPoint.X;
            return (stableY - b) / k;
        }
    }
}