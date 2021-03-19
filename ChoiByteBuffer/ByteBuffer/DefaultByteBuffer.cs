using System;

namespace Choi.ByteBuffer
{
    public abstract class DefaultByteBuffer : IByteBuffer
    {
        public abstract void Dispose();

        public int PositionToWrite { get; protected set; }

        public int PositionToRead { get; protected set; }

        public EscapeDelegate EscapeDelegate { get; set; }

        public void Rewind()
        {
            PositionToWrite = 0;
            PositionToRead = 0;
        }

        public abstract int Size { get; }

        public abstract byte[] GetBytes(int offset, int length);

        public abstract void PutBytes(params byte[] input);

        public IByteBuffer Put(byte[] inputs)
        {
            foreach (byte b in inputs)
            {
                Put(b);
            }

            return this;
        }

        public IByteBuffer Put(byte[] inputs, int length)
        {
            int alength = inputs.Length < length ? inputs.Length : length;

            byte[] result = new byte[length];

            Array.Clear(result, 0, length);
            Array.Copy(inputs, 0, result, 0, alength);

            return Put(result);
        }

        public IByteBuffer Put(byte input, bool escapeIfExist = true)
        {
            if (EscapeDelegate != null && escapeIfExist)
            {
                byte[] escDatas = EscapeDelegate(input);

                foreach (byte item in escDatas)
                {
                    PutBytes(item);
                }
            }
            else
            {
                PutBytes(input);
            }

            return this;
        }

        public IByteBuffer PutNull(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Put((byte)0x00);
            }

            return this;
        }

        public IByteBuffer Put(int input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(long input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(short input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(ushort input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(uint input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(ulong input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        public IByteBuffer Put(float input)
        {
            return Put(BitConverter.GetBytes(input));
        }

        #region get

        public T Get<T>()
        {
            return Get<T>(0);
        }

        public T Get<T>(int length)
        {
            object result = null;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Single:
                    result = BitConverter.ToSingle(Get(Math.Max(4, length)), 0);
                    break;
                case TypeCode.Double:
                    result = BitConverter.ToDouble(Get(Math.Max(8, length)), 0);
                    break;
                case TypeCode.UInt16:
                    result = BitConverter.ToUInt16(Get(Math.Max(2, length)), 0);
                    break;
                case TypeCode.UInt32:
                    result = BitConverter.ToUInt32(Get(Math.Max(4, length)), 0);
                    break;
                case TypeCode.UInt64:
                    result = BitConverter.ToUInt64(Get(Math.Max(8, length)), 0);
                    break;
                case TypeCode.Int16:
                    result = BitConverter.ToInt16(Get(Math.Max(2, length)), 0);
                    break;
                case TypeCode.Int32:
                    result = BitConverter.ToInt32(Get(Math.Max(4, length)), 0);
                    break;
                case TypeCode.Int64:
                    result = BitConverter.ToInt64(Get(Math.Max(8, length)), 0);
                    break;
                case TypeCode.String:
                    result = GetString(length);
                    break;
                case TypeCode.Byte:
                    result = BitConverter.ToSingle(Get(1), 0);
                    break;
                case TypeCode.Char:
                    result = BitConverter.ToChar(Get(1), 0);
                    break;
                case TypeCode.Object:
                    if (typeof(T).Equals(typeof(byte[])))
                        result = Get(length);
                    break;
            }
            if (result != null)
                return (T)result;
            return default(T);
        }

        //public int GetInt()
        //{
        //    return BitConverter.ToInt32(Get(4), 0);
        //}

        //public uint GetUInt()
        //{
        //    return BitConverter.ToUInt32(Get(4), 0);
        //}

        //public short GetShort()
        //{
        //    return BitConverter.ToInt16(Get(2), 0);
        //}

        //public ushort GetUShort()
        //{
        //    return BitConverter.ToUInt16(Get(2), 0);
        //}

        //public long GetLong()
        //{
        //    return BitConverter.ToInt64(Get(8), 0);
        //}

        //public ulong GetULong()
        //{
        //    return BitConverter.ToUInt64(Get(8), 0);
        //}


        public byte[] GetBytes()
        {
            return Get(PositionToWrite - PositionToRead);
        }

        public byte[] Get(int size, bool moveReadPoint)
        {
            var bytes = GetBytes(PositionToRead, size);
            if (moveReadPoint)
                PositionToRead += size;
            return bytes;
        }

        public byte[] Get(int size)
        {
            return Get(size, true);
        }

        public byte Get()
        {
            return Get(1)[0];
        }

        public string GetString(int length, char[] trimChars, System.Text.Encoding encoding = null)
        {
            return encoding.GetString(Get(length)).Trim(trimChars);
        }

        public string GetString(int length)
        {
            return GetString(length, new char[] { '\0' }, System.Text.Encoding.UTF8);
        }

        public byte GetChecksum(int length)
        {
            byte[] bytes = GetBytes(PositionToRead, length);
            int CheckSum = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                CheckSum += (int)(bytes[i] & 0xFF);
            }

            return (byte)CheckSum;
        }

        #endregion
    }
}
