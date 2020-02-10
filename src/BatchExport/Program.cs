using MADSPack.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace BatchExport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run(args.Length == 0 ? null : args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fatal error: {e}");
            }
            Console.WriteLine("\nPress RETURN to close.\n");
            Console.ReadLine();
        }

        static void Run(string folderColo)
        {
            if (folderColo == null)
            {
                Console.WriteLine("Usage: drag & drop the Colonization folder on me. I will create 'ColoExport' in the current Working Directory. :)");
                return;
            }

            if (!Directory.Exists(folderColo))
            {
                Console.WriteLine($"Not a folder: {folderColo}");
                return;
            }

            var folderTarget = Path.Combine(Environment.CurrentDirectory, "ColoExport");

            Directory.CreateDirectory(folderTarget);

            new Program(folderColo, folderTarget);

            // open in explorer
            Process.Start(folderTarget);
        }

        private readonly string _folderColo, _folderTarget;
        private readonly List<string> _errorFiles = new List<string>();

        private Program(string folderColo, string folderTarget)
        {
            _folderColo = folderColo;
            _folderTarget = folderTarget;

            var anyFilesProcessed = false;

            foreach (var file in Directory.EnumerateFiles(_folderColo))
            {
                var filenameLower = Path.GetFileName(file).ToLowerInvariant();
                try
                {
                    if (filenameLower.EndsWith(".pik"))
                    {
                        anyFilesProcessed = true;
                        ExportPik(file, filenameLower);
                    }
                    else if (filenameLower.EndsWith(".ss"))
                    {
                        anyFilesProcessed = true;
                        ExportSs(file, filenameLower);
                    }
                    //else if (filenameLower.EndsWith(".ff"))
                    //{
                    //    anyFilesProcessed = true;
                    //    ExportFf(file, filenameLower);
                    //}
                }
                catch (Exception e)
                {
                    _errorFiles.Add(filenameLower);
                    Console.WriteLine($"Error for '{filenameLower}': {e}");
                }
            }
            Console.WriteLine();
            if (!anyFilesProcessed)
            {
                Console.WriteLine("No files found. :|");
            }
            else if (_errorFiles.Count == 0)
            {
                Console.WriteLine("Finished successfully! :)");
            }
            else
            {
                Console.WriteLine("Finished with errors. Press RETURN to view all files with errors...");
                Console.ReadLine();
                foreach (var file in _errorFiles)
                {
                    Console.WriteLine(file);
                }
            }
            Console.WriteLine("\nPress RETURN to view the folder.");
            Console.ReadLine();
        }

        void ExportPik(string file, string filenameLower)
        {
            MadsPackReader r = new MadsPackReader(file);
            var pik = new MadsPackImagePIK(r.getItems())
            {
                pathtoCol = _folderColo
            };
            using (Bitmap bmp = pik.GetImage())
            {
                bmp.Save(Path.Combine(_folderTarget, filenameLower + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        void ExportSs(string file, string filenameLower)
        {
            MadsPackReader r = new MadsPackReader(file);
            var ss = new MadsPackImageSS(r.getItems(), 0)
            {
                pathtoCol = _folderColo
            };
            var numImages = ss.getPictureCount();
            for (int i = 0; i < numImages; i++)
            {
                var filename = $"{filenameLower}_{i}";
                try
                {
                    using (Bitmap bmp = ss.GetImage(i))
                    {
                        bmp.Save(Path.Combine(_folderTarget, $"{filename}.png"), System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception e)
                {
                    _errorFiles.Add(filename);
                    Console.WriteLine($"Error for '{filename}': {e}");
                }
            }
        }

        //void ExportFf(string file, string filenameLower)
        //{
            // currently does not support "just export all chars" AND chars also have size info.
            // no good format for this yet

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
        //}
    }
}
