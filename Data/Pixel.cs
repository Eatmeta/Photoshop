using System;

namespace MyPhotoshop
{
    public struct Pixel
    {
        private static double Check(double value)
        {
            if (value > 1 || value < 0)
                throw new ArgumentException();
            return value;
        }
        
        public static double Trim(double value)
        {
            if (value > 1)
                return 1;
            if (value < 0)
                return 0;
            return value;
        }
        
        public Pixel(double r, double g, double b) : this()
        {
            R = r;
            G = g;
            B = b;
        }
        
        private double _r;
        private double _g;
        private double _b;
        public double R
        {
            get => _r;
            private set => _r = Check(value);
        }
        public double G
        {
            get => _g;
            private set => _g = Check(value);
        }
        public double B
        {
            get => _b;
            private set => _b = Check(value);
        }

        public static Pixel operator *(Pixel pixel, double value)
        {
            return new Pixel(Trim(pixel.R * value), Trim(pixel.G * value), Trim(pixel.B * value));
        }
        
        public static Pixel operator *(double value, Pixel pixel)
        {
            return pixel * value;
        }
    }
}