using System.IO;

namespace MADSPack.Compression
{
    public class PalReader
    {
        public byte[] GetPalette(string filename)
        {
            byte[] buf = new byte[256 * 3];
            using (FileStream fs = File.OpenRead(filename))
            {
                fs.Read(buf, 0, buf.Length);
            }
            return buf;
        }
    }
}
