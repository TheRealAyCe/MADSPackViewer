using System;
using System.Collections.Generic;

namespace MADSPack.Compression
{
    public class MadsPackEntry
{

    public MadsPackEntry()
    {
    }

    public short getHash()
    {
        return hash;
    }

    public void setHash(short hash)
    {
        this.hash = hash;
    }

    public int getSize()
    {
        return size;
    }

    public void setSize(int size)
    {
        this.size = size;
    }

    public int getCompressedSize()
    {
        return compressedSize;
    }

    public void setCompressedSize(int compressedSize)
    {
        this.compressedSize = compressedSize;
    }

    public byte[] getData()
    {
        return data;
    }

    public void setData(byte[] data)
    {
        this.data = data;
    }

    private short hash;
    private int size;
    private int compressedSize;
    private byte[] data;
}
}
