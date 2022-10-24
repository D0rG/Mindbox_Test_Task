using System;
using Xunit;
using AreaCalculator.GeometricFigures;

namespace AreaCalculator.Tests
{
    public class AreaCalculatorTests
    {
        [Fact]
        public void CircleRadiusSuccessTest()
        {
            double radius = 1;

            var res = new Circle(radius).Area;

            Assert.Equal(Math.PI, res);
        }

        [Fact] 
        public void CircleRadiusFalureTest()
        {
            double radius = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(radius).Area);
        }

        [Fact]
        public void TriangSuccessTest()
        {
            var res = new Triangle(2, 2, 2).Area;

            Assert.True(Math.Abs(res - 1.73) <= 0.05);
        }

        [Fact]
        public void TriangleArgumentBadTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Triangle(-1,0,0).Area);
        }

        [Fact]
        public void TriangleNotExistTest()
        {
            Assert.Throws<FigureDoseNotExistException>(() => new Triangle(4, 2, 2).Area);
        }

        [Fact]
        public void TriangleIsRightTrueTest()
        {
            Assert.True(new Triangle(10, 5, Math.Sqrt(125)).IsRight(0.01));
        }

        [Fact]
        public void TriangleIsRightFalseTest()
        {
            Assert.False(new Triangle(1, 1, 1).IsRight());
        }
    }
}
