using System;

namespace Poi
{
    /// <summary>
    /// 数据报结构
    /// </summary>
	public class SocketHeader : ICloneable
	{
		public ushort length;
		public uint msg;

        static readonly public int Size = sizeof(ushort) + sizeof(int); // SocketHeader成员变更时，手动更改长度

		public SocketHeader()
		{
			length = 0;
			msg = 0;
		}
		public SocketHeader(byte[] packetHeader)
		{
			length = BitConverter.ToUInt16(packetHeader, 0);
			msg = BitConverter.ToUInt32(packetHeader, sizeof(ushort));
		}
		public SocketHeader(ushort length, uint msg)
		{
			this.length = length;
			this.msg = msg;
		}

		public object Clone()
		{
			var clone = new SocketHeader();
			clone.length = length;
			clone.msg = msg;

			return clone;
		}
	}
	public class SocketData : ICloneable
	{
		public SocketHeader header = new SocketHeader();
		public byte[] data;

		public SocketData(SocketHeader header)
		{
			this.header = (SocketHeader) header.Clone();
			this.data = new byte[this.header.length];
		}
		public SocketData(SocketHeader header, byte[] packet_data)
		{
			this.header = (SocketHeader) header.Clone();
			this.data = new byte[this.header.length];
            Buffer.BlockCopy(packet_data, 0, this.data, 0, this.header.length);
		}

		public object Clone()
		{
			return new SocketData(header, data);
		}
	}
}

namespace CAPTCHA
{ 
    static public class CAPTCHA
    {
        static public int EncodeCaptcha(string count,int server)
        {
            int temp = count.GetHashCode();
            return temp+server;
        }

        static public bool CheckCAPTCHA(string count,int server,int Captcha)
        {
            if (EncodeCaptcha(count, server) == Captcha)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}