using System;
using System.Linq;

namespace AreaCalculator.GeometricFigures
{
    public abstract class GeometricFigure
    {
        public abstract double Area { get; }
    }

    public class FigureDoseNotExistException : Exception { }

    public class Circle : GeometricFigure
    {
        public double Radius 
        {
            get; 
            private set;
        }

        private double _area = -1;
        public override double Area
        {
            get
            {
                if (_area < 0)
                {
                    _area = Math.Pow(Radius, 2) * Math.PI;
                }
                return _area;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radius"> Радиус круга </param>
        /// <exception cref="ArgumentOutOfRangeException"> Некорректный радиус </exception>
        public Circle(double radius)
        {
            if (radius < 0) throw new ArgumentOutOfRangeException(nameof(radius));
            Radius = radius;
        }
    }

    public class Triangle : GeometricFigure
    {
        private double[] _triangleSides = new double[3];
        public double[] TriangleSides { get { return _triangleSides; } }

        private double _halfPerimetr = 0;
        private double _area = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstSide"> Длинна стороны </param>
        /// <param name="scndSide"> Длинна стороны </param>
        /// <param name="thirdSide"> Длинна стороны </param>
        /// <exception cref="ArgumentOutOfRangeException"> Хотя бы одна из длин сторон не может встечаться в треугольнике </exception>
        /// <exception cref="FigureDoseNotExistException"> Треугольника с таким сочетанием длин сторон не существует </exception>
        public Triangle(double firstSide, double scndSide, double thirdSide)
        {
            if (firstSide < 0 || scndSide < 0 || thirdSide < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _halfPerimetr = (firstSide + scndSide + thirdSide) / 2;
            if (!TriangleExist(firstSide, scndSide, thirdSide))
            {
                throw new FigureDoseNotExistException();
            }

            _triangleSides[0] = firstSide;
            _triangleSides[1] = scndSide;
            _triangleSides[2] = thirdSide;
        }

        public override double Area //Не лучший вариант, если считать из иных входных данных.
        {
            get
            {
                if (_area < 0)
                {
                    var underSqrt = _halfPerimetr;

                    foreach (var triangleSide in _triangleSides)
                    {
                        underSqrt *= (_halfPerimetr - triangleSide);
                    }
                    _area = Math.Sqrt(underSqrt);
                }
                return _area;
            }
        }

        /// <summary>
        /// Явялется ли триугольник прямоугольным
        /// </summary>
        /// <param name="fallibility"> Погрешность вычисления </param>
        /// <returns></returns>
        public bool IsRight(double fallibility = 0)
        {
            double leftEquationSide = _triangleSides.Max();
            leftEquationSide *= leftEquationSide;

            double rigntEquationSide = 0;

            foreach (var triangleSide in _triangleSides)
            {
                rigntEquationSide += triangleSide * triangleSide;
            }
            rigntEquationSide /= 2;

            return Math.Abs(leftEquationSide - rigntEquationSide) <= fallibility;
        }

        private bool TriangleExist(double firstSide, double scndSide, double thirdSide)
        {
            return (_halfPerimetr - firstSide > 0
                && _halfPerimetr - scndSide > 0
                && _halfPerimetr - thirdSide > 0);
        }
    }
}
