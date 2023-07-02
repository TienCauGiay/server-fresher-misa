namespace MISA.WebFresher042023.Api
{
    /// <summary>
    /// Lớp cộng trừ nhân chia
    /// </summary>
    /// Created By: BNTIEN (11/06/2023)
    public class Caculator
    {
        /// <summary>
        /// Hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Tổng 2 số nguyên</returns>
        /// Created By: BNTIEN (11/06/2023)
        public long Add(int a, int b)
        {
            return (long)a + b;
        }
        /// <summary>
        /// Hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="a">Số trừ</param>
        /// <param name="b">Số bị trừ</param>
        /// <returns>Hiệu 2 số a, b</returns>
        /// Created By: BNTIEN (11/06/2023)
        public long Sub(int a, int b)
        {
            return (long)a - b;
        }
        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a">Thừa số 1</param>
        /// <param name="b">Thừa số 2</param>
        /// <returns>Tích 2 số</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: BNTIEN (11/06/2023)
        public long Mul(int a, int b)
        {
            return (long)(a) * b;
        }
        /// <summary>
        /// Hàm chia 2 số nguyên
        /// </summary>
        /// <param name="a">Số chia</param>
        /// <param name="b">Số bị chia</param>
        /// <returns>Thương 2 số</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// Created By: BNTIEN (11/06/2023)
        public double Div(int a, int b)
        {
            if (b == 0) throw new Exception("Khong chia duoc cho 0");
            return (double)(a) / b;
        }
    }
}
