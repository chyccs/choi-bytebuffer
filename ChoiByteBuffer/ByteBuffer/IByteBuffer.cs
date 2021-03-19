using System;

namespace Choi.ByteBuffer
{
    public delegate byte[] EscapeDelegate(byte input);

    public interface IByteBuffer : IDisposable
    {
        EscapeDelegate EscapeDelegate { get; set; }

        int PositionToWrite { get; }

        int PositionToRead { get; }

        int Size { get; }

        void PutBytes(params byte[] inputs);

        byte[] GetBytes(int offset, int size);

        IByteBuffer Put(params byte[] b);

        IByteBuffer Put(string b, int length);

        IByteBuffer Put(byte[] b, int length);

        IByteBuffer PutNull(int length);

        IByteBuffer Put(short input);

        IByteBuffer Put(ushort input);

        IByteBuffer Put(int input);

        IByteBuffer Put(uint input);

        IByteBuffer Put(long input);

        IByteBuffer Put(ulong input);

        IByteBuffer Put(float input);

        byte[] Get(int length);

        T Get<T>();

        T Get<T>(int length);

        byte GetChecksum(int length);
    }
}
