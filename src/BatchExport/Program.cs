using MADSPack.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchExport
{
    class Program
    {
        static string exportFolder = @"C:\Users\AyCe\Desktop\ColoExport\";
        static string coloFolder = @"E:\Programme\CGN\Colonization";

        static void Main(string[] args)
        {

            foreach (var file in Directory.EnumerateFiles(coloFolder))
            {
                var filenameLower = Path.GetFileName(file).ToLowerInvariant();
                try
                {
                    if (filenameLower.EndsWith(".pik"))
                    {
                        ExportPik(file, filenameLower);
                    }
                    else if (filenameLower.EndsWith(".ss"))
                    {
                        ExportSs(file, filenameLower);
                    }
                    else if (filenameLower.EndsWith(".ff"))
                    {
                        ExportFf(file, filenameLower);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error for '{filenameLower}': {e}");
                }
            }
            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        static void ExportPik(string file, string filenameLower)
        {
            MadsPackReader r = new MadsPackReader(file);
            var pik = new MadsPackImagePIK(r.getItems());
            pik.pathtoCol = coloFolder;
            using (Bitmap bmp = pik.GetImage())
            {
                bmp.Save(exportFolder + filenameLower + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        static void ExportSs(string file, string filenameLower)
        {
            MadsPackReader r = new MadsPackReader(file);
            var ss = new MadsPackImageSS(r.getItems(), 0);
            ss.pathtoCol = coloFolder;
            var numImages = ss.getPictureCount();
            for (int i = 0; i < numImages; i++)
            {
                try
                {
                    using (Bitmap bmp = ss.GetImage(i))
                    {
                        bmp.Save(exportFolder + filenameLower + $"_{i}.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error for '{filenameLower}_{i}': {e}");
                }
            }
        }

        static void ExportFf(string file, string filenameLower)
        {
            //MadsPackReader r = new MadsPackReader(file);
            //var ff = new MadsPackFont(r.getItems()[0]);
            //ff.pathtoCol = coloFolder;

            //Bitmap b = ff.GenerateFontMap();

            //var folder = exportFolder + "/" + filenameLower;
            //Directory.CreateDirectory(folder);

            //var numImages = ff.getPictureCount();
            //for (int i = 0; i < numImages; i++)
            //{
            //    try
            //    {
            //        using (Bitmap bmp = ff.GetImage(i))
            //        {
            //            bmp.Save(folder + "/" + i+".png", System.Drawing.Imaging.ImageFormat.Png);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine($"Error for '{filenameLower}_{i}': {e}");
            //    }
            //}
        }
    }
}
