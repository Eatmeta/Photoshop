using System;
using System.Drawing;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Transform;

namespace MyPhotoshop.Filters.Transform
{
    public class RotateTransformer : ITransformer<RotationParameters>
    {
        public Size OriginalSize { get; private set; }
        public Size ResultSize { get; set; }
        public double Angle { get; private set; }

        public void Prepare(Size size, RotationParameters parameters)
        {
            OriginalSize = size;
            Angle = Math.PI * parameters.Angle / 180;
            ResultSize =  new Size(
                (int) (size.Width * Math.Abs(Math.Cos(Angle)) + size.Height * Math.Abs(Math.Sin(Angle))),
                (int) (size.Height * Math.Abs(Math.Cos(Angle)) + size.Width * Math.Abs(Math.Sin(Angle))));
        }

        public Point? MapPoint(Point newPoint)
        {
            newPoint = new Point(newPoint.X - ResultSize.Width / 2, newPoint.Y - ResultSize.Height / 2);
            var x = OriginalSize.Width / 2 + (int) (newPoint.X * Math.Cos(Angle) + newPoint.Y * Math.Sin(Angle));
            var y = OriginalSize.Height / 2 + (int) (-newPoint.X * Math.Sin(Angle) + newPoint.Y * Math.Cos(Angle));
            if (x < 0 || x >= OriginalSize.Width || y < 0 || y >= OriginalSize.Height) return null;
            return new Point(x, y);
        }
        
    }
}