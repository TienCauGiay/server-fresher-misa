using MISA.WebFresher042023.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.UnitTests
{
    /// <summary>
    /// Test các hàm của class Caculator
    /// </summary>
    /// Created By: BNTIEN (11/06/2023)

    [TestFixture]
    public class CaculatorTests
    {
        /// <summary>
        /// Test hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedResult"></param>
        [TestCase(4, 5, 9)]
        [TestCase(1, 2, 3)]
        [TestCase(int.MaxValue, 1, (long)int.MaxValue + 1)]
        public void Add_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Add(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedResult"></param>
        [TestCase(4, 5, -1)]
        [TestCase(1, 3, -2)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MaxValue * 2 + 1)]
        public void Sub_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Sub(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedResult"></param>
        [TestCase(4, 5, 20)]
        [TestCase(1, 1, 1)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MaxValue * int.MinValue)]
        public void Mul_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Mul(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test hàm chia 2 số nguyên (trường hợp cho cho số khác 0)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedResult"></param>
        [TestCase(20, 4, 5)]
        [TestCase(4, 5, 0.8)]
        [TestCase(5, 3, 1.666667)]
        public void Div_ValidInput_Success(int a, int b, double expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Div(a, b);

            // Assert
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-6));
        }

        /// <summary>
        /// Test hàm chia 2 số nguyên (trường hợp chia cho số 0)
        /// </summary>
        [Test]
        public void Div_InvalidInput_Exception()
        {
            // Arrange
            var a = 5;
            var b = 0;
            var expectedException = new Exception("Không chia được cho 0");
            // Act && Assert
            var handle = () => new Caculator().Div(a, b);
            var actualException = Assert.Throws<Exception>(() => handle(), expectedException.Message);
        }
    }
}
