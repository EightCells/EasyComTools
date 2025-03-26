namespace EasyComTools
{
    /// <summary>
    /// CRC16校验函数
    /// </summary>
    public class Crc16
    {
        private readonly ushort _polynomial;
        private readonly ushort _initialValue;
        private readonly bool _useInReverse;
        private readonly bool _useOutReverse;
        private readonly ushort _xorOutValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Crc16(
            ushort polynomial = 0x8005,
            ushort initialValue = 0xFFFF,
            bool useInReverse = true,
            bool useOutReverse = true,
            ushort xorOutValue = 0x0000)
        {
            _polynomial = polynomial;
            _initialValue = initialValue;
            _useInReverse = useInReverse;
            _useOutReverse = useOutReverse;
            _xorOutValue = xorOutValue;
        }

        /// <summary>
        /// 计算CRC16校验值
        /// </summary>
        /// <param name="data">输入数据（自动进行位反转）</param>
        public ushort ComputeChecksum(byte[] data)
        {
            ushort crc = _initialValue;

            foreach (byte b in data)
            {
                byte tmpb = _useInReverse ? ReverseBits(b) : b;

                crc ^= (ushort)(tmpb << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ _polynomial);
                    else
                        crc <<= 1;
                }
            }

            crc = _useOutReverse ? ReverseUInt16(crc) : crc;
            crc ^= _xorOutValue;

            return crc;
        }

        /// <summary>
        /// 单字节位反转（如0x01 → 0x80）
        /// </summary>
        public static byte ReverseBits(byte b)
        {
            // 每1位反转
            b = (byte)(((b & 0xAA) >> 1) | ((b & 0x55) << 1));
            // 每2位反转
            b = (byte)(((b & 0xCC) >> 2) | ((b & 0x33) << 2));
            // 最终每4位反转
            return (byte)((b >> 4) | (b << 4));
        }

        /// <summary>
        /// 16位整数的整体位反转（如0x1234 → 0x2C48）
        /// </summary>
        public static ushort ReverseUInt16(ushort value)
        {
            // 每1位反转
            value = (ushort)(((value & 0xAAAA) >> 1) | ((value & 0x5555) << 1));
            // 每2位反转
            value = (ushort)(((value & 0xCCCC) >> 2) | ((value & 0x3333) << 2));
            // 每4位反转
            value = (ushort)(((value & 0xF0F0) >> 4) | ((value & 0x0F0F) << 4));
            // 最终每8位反转
            return (ushort)((value >> 8) | (value << 8));

        }
    }
}
