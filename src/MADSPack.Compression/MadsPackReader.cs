using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MADSPack.Compression
{
    public class MadsPackReader
    {
        /* member class not found */


        public MadsPackReader()
        {
        }

        public MadsPackReader(FileStream file)
        {
            this.mStream = file;
        }

        public MadsPackReader(String filename)
        {
            mStream = File.OpenRead(filename);
            int pos = filename.IndexOf(".") + 1;
            String extension = filename.Substring(pos);
            if (extension.ToUpperInvariant() == "SS")
                type = FileType.SPRITES;
            else
                if (extension.ToUpperInvariant() == "PIK")
                    type = FileType.IMAGE;
                else
                    if (extension.ToUpperInvariant() == "FF")
                        type = FileType.FONT;
            initialise();
        }

        public bool isCompressed(FileStream stream)
        {
            byte[] tempBuffer = new byte[8];
            stream.Seek(0, SeekOrigin.Begin);
            return stream.Read(tempBuffer, 0, tempBuffer.Length) == 8 && (Encoding.ASCII.GetString(tempBuffer).Trim() == "MADSPACK");
        }

        public int getCount()
        {
            return count;
        }

        public MadsPackEntry getItem(int index)
        {
            return items[index];
        }

        public MadsPackEntry[] getItems()
        {
            return items;
        }

        public MemoryStream getItemStream(int index)
        {
            return new MemoryStream(items[index].getData(), 0, items[index].getSize());
        }

        private void initialise()
        {
            if (!isCompressed(mStream))
                throw new IOException("Attempted to decompress a resource that was not MadsPacked");
            mStream.Seek(14, SeekOrigin.Begin);
            byte[] tcount = new byte[2];
            mStream.Read(tcount, 0, 2);
            count = (int)BitConverter.ToInt16(tcount, 0);
            items = new MadsPackEntry[count];
            byte[] headerData = new byte[160];
            mStream.Read(headerData, 0, headerData.Length);
            // Maybe convert to big-endian?
            MemoryStream header = new MemoryStream(headerData);
            for (int i = 0; i < count; i++)
            {
                byte[] twobyte = new byte[2];
                byte[] fourbyte = new byte[4];

                // maybe reverse twobyte and fourbyte per reading as the file is little-endian?
                items[i] = new MadsPackEntry();
                header.Read(twobyte, 0, 2);
                items[i].setHash(BitConverter.ToInt16(twobyte, 0));
                header.Read(fourbyte, 0, 4);
                items[i].setSize(BitConverter.ToInt32(fourbyte, 0));
                header.Read(fourbyte, 0, 4);
                items[i].setCompressedSize(BitConverter.ToInt32(fourbyte, 0));
                items[i].setData(new byte[items[i].getSize()]);

                if (items[i].getSize() == items[i].getCompressedSize())
                {
                    mStream.Read(items[i].getData(), 0, items[i].getSize());
                }
                else
                {
                    byte[] compressedData = new byte[items[i].getCompressedSize()];
                    mStream.Read(compressedData, 0, items[i].getCompressedSize());
                    FabDecompressor fab = new FabDecompressor();
                    items[i].setData(fab.decompress(compressedData));
                }
            }

        }

        public void setType(FileType type)
        {
            this.type = type;
        }

        public FileType getType()
        {
            return type;
        }

        private MadsPackEntry[] items;
        private FileType type;
        private int count;
        private static string madsPackString = "MADSPACK";
        private FileStream mStream;
    }

    public enum FileType
    {
        SPRITES,
        IMAGE,
        FONT
    }
}
