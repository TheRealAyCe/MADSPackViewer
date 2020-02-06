using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MADSPack.Compression
{
    public class FabDecompressor
    {

        public FabDecompressor()
        {
        }

        public byte[] decompress(byte[] input)
        {

            _inputStream = new MemoryStream(input);
            _outputStream = new MemoryStream();
            byte[] signature = new byte[3];
            _inputStream.Read(signature, 0, 3);
            String signatureString = Encoding.ASCII.GetString(signature, 0, signature.Length);
            if (!signatureString.Equals("FAB"))
                throw new IOException("Invalid compressed data");
            int shiftVal = _inputStream.ReadByte();
            if (shiftVal < 10 || shiftVal > 13)
                throw new IOException("Invalid shift start");
            int copyOfsShift = 16 - shiftVal;
            int copyOfsMask = 255 << shiftVal - 8;
            int copyLenMask = (1 << copyOfsShift) - 1;
            _bitsLeft = 16;
            _bitBuffer = readWord();
            do
                if (getBit() == 0L)
                {
                    long copyOfs;
                    int copyLen;
                    long before;
                    if (getBit() == 0L)
                    {
                        copyLen = (int)((getBit() << 1 | getBit()) + 2L);
                        int tb = _inputStream.ReadByte();
                        copyOfs = (int)(((uint)tb) | 0xffffff00);
                    }
                    else
                    {
                        int b1 = _inputStream.ReadByte();
                        int b2 = _inputStream.ReadByte();
                        copyOfs = (b2 >> copyOfsShift | copyOfsMask) << 8 | b1;
                        copyLen = b2 & copyLenMask;
                        if (copyLen == 0)
                        {
                            copyLen = _inputStream.ReadByte();
                            if (copyLen == 0)
                                break;
                            if (copyLen == 1)
                                continue;
                            copyLen++;
                        }
                        else
                        {
                            copyLen += 2;
                        }
                        unchecked
                        {
                            before = copyOfs;
                            copyOfs = (long)(((ulong)copyOfs) | 0xffffffffffff0000);
                        }
                    }
                    while (copyLen-- > 0)
                    {
                        long pos = _outputStream.Position;
                        _outputStream.Seek(pos + copyOfs, SeekOrigin.Begin);
                        byte read = (byte)_outputStream.ReadByte();
                        _outputStream.Seek(pos, SeekOrigin.Begin);
                        _outputStream.WriteByte(read);
                    }
                }
                else
                {
                    int c = _inputStream.ReadByte();
                    _outputStream.WriteByte((byte)c);
                }
            while (true);
            _outputStream.Flush();
            return ((MemoryStream)_outputStream).ToArray();
        }

        private long readWord()
        {
            long i = _inputStream.ReadByte();
            long i2 = _inputStream.ReadByte();
            return i2 << 8 | i;
        }

        private long getBit()
        {
            _bitsLeft--;
            if (_bitsLeft == 0)
            {
                _bitBuffer = readWord() << 1 | _bitBuffer & 1L;
                _bitsLeft = 16;
            }
            long bit = _bitBuffer & 1L;
            _bitBuffer >>= 1;
            return bit;
        }

        private MemoryStream _inputStream;
        private MemoryStream _outputStream;
        private int _bitsLeft;
        private long _bitBuffer;
    }
}
