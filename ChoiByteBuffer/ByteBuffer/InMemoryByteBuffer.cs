using System;
using System.Linq;
using System.Collections.Generic;

namespace Choi.ByteBuffer
{
    public class InMemoryByteBuffer : DefaultByteBuffer
    {
        private List<byte> data = new List<byte>();

        public override void Dispose()
        {
            data.Clear();
            data = null;
            GC.SuppressFinalize(this);
        }

        public override int Size
        {
            get { return data.Count; }
        }

        public InMemoryByteBuffer(byte[] data = null, EscapeDelegate escape = null)
        {
            EscapeDelegate = escape;
            if (data != null && data.Length > 0)
                this.data.AddRange(data.ToList());
        }

        public override void PutBytes(params byte[] input)
        {
            foreach (var b in input)
                data.Add(b);
            PositionToWrite += input.Length;
        }

        public override byte[] GetBytes(int offset, int length)
        {
            return data.Skip(offset).Take(length).ToArray();
        }

        public byte[] ToArray()
        {
            return data.ToArray();
        }
    }
}
