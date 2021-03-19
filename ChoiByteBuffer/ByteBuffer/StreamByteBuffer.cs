using System;
using System.IO;

namespace Choi.ByteBuffer
{
    public class StreamByteBuffer : DefaultByteBuffer
    {
        public override void Dispose()
        {
            Stream?.Dispose();
            Stream = null;
            GC.SuppressFinalize(this);
        }

        private Stream Stream { get; set; }

        public override int Size
        {
            get { return (int)Stream.Length; }
        }

        public StreamByteBuffer(Stream stream, EscapeDelegate escape = null)
        {
            EscapeDelegate = escape;
            Stream = stream;
        }

        public override void PutBytes(params byte[] input)
        {
            foreach (var b in input)
                Stream.WriteByte(b);
            PositionToWrite += input.Length;
        }

        public override byte[] GetBytes(int offset, int length)
        {
            Stream.Position = offset;
            byte[] bytes = new byte[length];
            Stream.Read(bytes, 0, length);
            Stream.Position -= length;
            return bytes;
        }
    }
}
