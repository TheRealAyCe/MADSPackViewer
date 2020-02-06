using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MADSPack.Compression
{
    public class MadsPackImagePIK : MadsPackImage
    {

        public MadsPackImagePIK(MadsPackEntry[] items)
        {
            byte[] headerData = items[0].getData();
            MemoryStream dis = new MemoryStream(headerData);
            try
            {
                byte[] twobyte = new byte[2];
                dis.Read(twobyte, 0, 2);
                setHeight(BitConverter.ToInt16(twobyte, 0));
                dis.Read(twobyte, 0, 2);
                setWidth(BitConverter.ToInt16(twobyte, 0));
                dis.Dispose();
            }
            catch (IOException e)
            {
                // Do something
            }
            setImageData(items[1].getData());
            if (items.Length == 2)
            {
                setHasPalette(false);
            }
            else
            {
                setHasPalette(true);
                setPaletteData(items[2].getData());
            }
        }
    }
}
