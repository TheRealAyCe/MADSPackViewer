using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace MADSPack.Compression
{
    public class MadsPackImageSS : MadsPackImage
    {

        public MadsPackImageSS(MadsPackEntry[] items, int selectedImage)
        {
            try
            {
                byte[] animHeader = items[0].getData();
                pictureCount = animHeader[38];
                if (pictureCount < 0)
                    pictureCount += 256;
                byte[] headerData = items[1].getData();
                MemoryStream dis = new MemoryStream(headerData);
                startOffsets = new int[pictureCount];
                lengths = new int[pictureCount];
                heights = new short[pictureCount];
                heightsPadded = new short[pictureCount];
                widths = new short[pictureCount];
                widthsPadded = new short[pictureCount];

                byte[] twobyte = new byte[2];
                byte[] fourbyte = new byte[4];

                for (int i = 0; i < pictureCount; i++)
                {
                    dis.Read(fourbyte, 0, 4);
                    startOffsets[i] = BitConverter.ToInt32(fourbyte, 0);
                    dis.Read(fourbyte, 0, 4);
                    lengths[i] = BitConverter.ToInt32(fourbyte, 0);
                    dis.Read(twobyte, 0, 2);
                    widthsPadded[i] = BitConverter.ToInt16(twobyte, 0);
                    dis.Read(twobyte, 0, 2);
                    heightsPadded[i] = BitConverter.ToInt16(twobyte, 0);
                    dis.Read(twobyte, 0, 2);
                    widths[i] = BitConverter.ToInt16(twobyte, 0);
                    dis.Read(twobyte, 0, 2);
                    heights[i] = BitConverter.ToInt16(twobyte, 0);
                }

                setHeight(heights[selectedImage]);
                setWidth(widths[selectedImage]);
                spriteImageData = items[3].getData();
                byte[] imageData = uncompressRLEimageData(spriteImageData, startOffsets[selectedImage], lengths[selectedImage], heights[selectedImage], widths[selectedImage]);
                setImageData(imageData);
                dis.Dispose();
            }
            catch (IOException e)
            {
                // Do something
            }
            setHasPalette(true);
            setPaletteData(items[2].getData());
        }

        private byte[] uncompressRLEimageData(byte[] wholeData, int offset, int length, int height, int width)
        {
            MemoryStream bufStream = new MemoryStream(wholeData, offset, length);
            MemoryStream _outputStream = new MemoryStream();
            bool spriteEnd = false;
            try
            {
                for (int yp = 0; yp < height; yp++)
                {
                    long pos = yp * width;
                    _outputStream.Seek(pos, SeekOrigin.Begin);
                    bool newLine = false;
                    int cmd = bufStream.ReadByte();
                    int x2 = 0;
                    if (cmd == 252)
                    {
                        spriteEnd = true;
                        break;
                    }
                    if (cmd == TRANSPARENT_COLOUR_INDEX)
                    {
                        while (x2++ < width)
                            _outputStream.WriteByte(TRANSPARENT_COLOUR_INDEX);
                        newLine = true;
                    }
                    else
                        if (cmd == 253)
                        while (x2 < width)
                        {
                            cmd = bufStream.ReadByte();
                            if (cmd == TRANSPARENT_COLOUR_INDEX)
                            {
                                while (x2++ < width)
                                    _outputStream.WriteByte(TRANSPARENT_COLOUR_INDEX);
                                newLine = true;
                                break;
                            }
                            int v = bufStream.ReadByte();
                            while (cmd-- > 0)
                            {
                                if (x2 < width)
                                    _outputStream.WriteByte(v != 253 ? (byte)v : TRANSPARENT_COLOUR_INDEX);
                                x2++;
                            }
                        }
                    else
                        while (x2 < width)
                        {
                            cmd = bufStream.ReadByte();
                            if (cmd == TRANSPARENT_COLOUR_INDEX)
                            {
                                while (x2++ < width)
                                    _outputStream.WriteByte(TRANSPARENT_COLOUR_INDEX);
                                newLine = true;
                                break;
                            }
                            if (cmd == 254)
                            {
                                cmd = bufStream.ReadByte();
                                int v = bufStream.ReadByte();
                                while (cmd-- > 0)
                                {
                                    if (x2 < width)
                                        _outputStream.WriteByte(v != 253 ? (byte)v : TRANSPARENT_COLOUR_INDEX);
                                    x2++;
                                }
                            }
                            else
                            {
                                _outputStream.WriteByte(cmd != 253 ? (byte)cmd : TRANSPARENT_COLOUR_INDEX);
                                x2++;
                            }
                        }
                    if (!newLine)
                        while (bufStream.ReadByte() != TRANSPARENT_COLOUR_INDEX) ;
                }

                if (!spriteEnd)
                {
                    int v = bufStream.ReadByte();
                    if (v != 252)
                    {
                        // Do something
                    }
                }
                _outputStream.Flush();
                if (_outputStream.Length < (long)(height * width))
                {
                    long missingLength = (long)(height * width) - _outputStream.Length;
                    for (int i = 0; (long)i < missingLength; i++)
                        _outputStream.WriteByte(TRANSPARENT_COLOUR_INDEX);

                    _outputStream.Flush();
                }
                _outputStream.Flush();
            }
            catch (IOException ioexception) { }
            return _outputStream.ToArray();
        }

        public short getPictureCount()
        {
            return pictureCount;
        }

        public Rectangle getPictureSize(int selectedImage)
        {
            Rectangle dim = new Rectangle(0, 0, widths[selectedImage], heights[selectedImage]);
            return dim;
        }

        public Bitmap GetImage(int index)
        {
            if (!this.hasPalette())
            {
                PalReader r = new PalReader();
                this.setPaletteData(r.GetPalette(pathtoCol + "\\VICEROY.PAL"));
            }

            byte[] imageData = uncompressRLEimageData(spriteImageData, startOffsets[index], lengths[index], heights[index], widths[index]);

            // Create Bitmap
            Bitmap bmp = new Bitmap(this.widths[index], this.heights[index]);
            for (int y = 0; y < this.heights[index]; y++)
            {
                for (int x = 0; x < this.widths[index]; x++)
                {
                    // Find index in array
                    int pos = (y * this.widths[index]) + x;
                    // Get index for this pixel
                    int idx = imageData[pos];
                    // Get RGB values for this index

                    // @BUGFIX
                    //int a = idx == 255 || idx == 0 ? 0 : 255;
                    int a = idx == TRANSPARENT_COLOUR_INDEX ? 0 : TRANSPARENT_COLOUR_INDEX;

                    int r = this.getPaletteData()[idx * 3] * 4;
                    int g = this.getPaletteData()[(idx * 3) + 1] * 4;
                    int b = this.getPaletteData()[(idx * 3) + 2] * 4;
                    // Set this pixel color in image
                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            return bmp;
        }

        private int[] startOffsets;
        private int[] lengths;
        private short[] heights;
        private short[] heightsPadded;
        private short[] widths;
        private short[] widthsPadded;
        private short pictureCount;
        private byte[] spriteImageData;
        private const byte TRANSPARENT_COLOUR_INDEX = 255;
    }
}
