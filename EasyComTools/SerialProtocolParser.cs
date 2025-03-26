using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace EasyComTools
{
    /// <summary>
    /// 通信协议核心类
    /// </summary>
    public class SerialProtocolParser
    {
        #region 常量
        private const byte FrameHeader = 0xEE;   // 帧头示例
        private const byte FrameFooter = 0xFF;   // 帧尾示例
        private const int HeaderFooterLen = 1;   // 头尾长度
        private const int CrcLen = 2;            // CRC长度
        #endregion

        #region DataFrame结构
        public enum DataType : byte
        {
            UInt32 = 1,
            Int32 = 2,
            Float32 = 3,
            Double64 = 4,
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DataFrame
        {
            public byte Identifier;          // 标识字节
            public DataType TypeId;          // 类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] BatchID;           // 4字节批次标识
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Payload;           // 8字节数据
        }
        #endregion

        #region 成员变量
        private readonly int FullFrameLength = HeaderFooterLen * 2 + Marshal.SizeOf<DataFrame>() + CrcLen;
        private readonly Queue<(DateTime, DataFrame)> _dataQueue = new Queue<(DateTime, DataFrame)>();
        private readonly Semaphore _sm_dataQueue = new Semaphore(1, 1);
        private readonly List<byte> _buffer = new List<byte>(256);
        private readonly Crc16 crc16;
        private readonly bool _enableCRC;
        #endregion

        #region 构造函数
        public SerialProtocolParser(Crc16 crc, bool enableCRC = true)
        {
            crc16 = crc;
            _enableCRC = enableCRC;
        }
        #endregion

        #region 公开接口
        /// <summary>
        /// 接收原始字节数据并解析
        /// </summary>
        public void ReceiveBytes(byte[] bytes)
        {
            _buffer.AddRange(bytes);
            ProcessBuffer();
        }

        /// <summary>
        /// 从队列获取解析完成的数据
        /// </summary>
        public bool TryGetData(out (DateTime, DataFrame) data)
        {
            if (_dataQueue.Count > 0)
            {
                _sm_dataQueue.WaitOne();
                data = _dataQueue.Dequeue();
                _sm_dataQueue.Release();
                return true;
            }
            data = default;
            return false;
        }
        #endregion

        #region 核心处理逻辑
        private void ProcessBuffer()
        {
            int processedCount = 0;

            while (processedCount <= _buffer.Count - FullFrameLength)
            {
                // 扫描帧头
                int headerIndex = _buffer.FindIndex(processedCount, b => b == FrameHeader);
                if (headerIndex == -1) break;

                // 检查后续数据完整性
                int remainingLength = _buffer.Count - headerIndex;
                if (remainingLength < FullFrameLength)
                {
                    // 保留可能构成完整帧的数据
                    int validDataStart = Math.Max(0, headerIndex - (FullFrameLength - 1));
                    _buffer.RemoveRange(0, validDataStart);
                    return;
                }

                // 提取候选帧
                var candidateFrame = _buffer.GetRange(headerIndex, FullFrameLength).ToArray();

                // 帧尾验证
                if (candidateFrame[FullFrameLength - HeaderFooterLen] != FrameFooter)
                {
                    processedCount = headerIndex + 1; // 跳过错误帧头
                    continue;
                }

                // CRC校验
                if (_enableCRC)
                {
                    if (ValidateFrame(candidateFrame))
                    {
                        // 解析数据
                        var data = BytesToStruct<DataFrame>(candidateFrame, HeaderFooterLen);
                        _sm_dataQueue.WaitOne();
                        _dataQueue.Enqueue((DateTime.Now, data));
                        _sm_dataQueue.Release();

                        // 标记处理位置
                        processedCount = headerIndex + FullFrameLength;
                    }
                    else
                    {
                        processedCount = headerIndex + 1; // CRC错误跳过当前帧头
                    }
                }
                else
                {
                    // 解析数据
                    var data = BytesToStruct<DataFrame>(candidateFrame, HeaderFooterLen);
                    _sm_dataQueue.WaitOne();
                    _dataQueue.Enqueue((DateTime.Now, data));
                    _sm_dataQueue.Release();

                    // 标记处理位置
                    processedCount = headerIndex + FullFrameLength;
                }
            }

            // 清理已处理数据
            if (processedCount > 0)
            {
                _buffer.RemoveRange(0, processedCount);
            }
            else if (_buffer.Count >= FullFrameLength * 2)
            {
                // 防御性清理：当堆积大量无效数据时，保留最后N字节
                int retainSize = FullFrameLength * 2 - 1;
                int removeCount = _buffer.Count - retainSize;
                _buffer.RemoveRange(0, removeCount);
            }
        }

        private bool ValidateFrame(byte[] frame)
        {
            // 提取数据段（排除头尾和CRC）
            int dataLength = FullFrameLength - HeaderFooterLen * 2 - CrcLen;
            byte[] dataSegment = new byte[dataLength];
            Array.Copy(frame, HeaderFooterLen, dataSegment, 0, dataLength);

            // 提取接收到的CRC
            ushort receivedCrc = BitConverter.ToUInt16(frame, HeaderFooterLen + dataLength);

            // 计算实际CRC
            ushort calculatedCrc = crc16.ComputeChecksum(dataSegment);

            return receivedCrc == calculatedCrc;
        }
        #endregion

        #region 结构体转换
        private static T BytesToStruct<T>(byte[] bytes, int startIndex) where T : struct
        {
            int size = Marshal.SizeOf<T>();
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, startIndex, ptr, size);
                return Marshal.PtrToStructure<T>(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
        #endregion
    }
}
