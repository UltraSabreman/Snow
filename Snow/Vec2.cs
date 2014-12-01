using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow {
    class Vec2 {
        public double X { get; set; }
        public double Y { get; set; }


        public Vec2(double x, double y) {
            X = x;
            Y = y;
        }
        public static Vec2 operator +(Vec2 v1, Vec2 v2) {
            return new Vec2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2 operator -(Vec2 v1, Vec2 v2) {
            return new Vec2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vec2 operator *(double m, Vec2 v1) {
            return v1 * m;
        }
        
        public static Vec2 operator *(Vec2 v1, double m) {
            return new Vec2(v1.X * m, v1.Y * m);
        }

        public static Vec2 operator /(double m, Vec2 v1) {
            return v1 / m;
        }
        public static Vec2 operator /(Vec2 v1, double m) {
            return new Vec2(v1.X / m, v1.Y / m);
        }

        public static double Distance(Vec2 v1, Vec2 v2) {
            return (double)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
        }

        public double Length() {
            return (double)Math.Sqrt(X * X + Y * Y);
        }

        public static double DotProduct(Vec2 v1, Vec2 v2) {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public Vec2 Normalized() {
            double len = Length();
            return new Vec2(X / len, Y / len);
        }

    }
}
