using System;
using System.IO;
using System.IO.Compression;

namespace Net.FreeORM.Compress
{
    public class Compresser
    {

        public static void ZipMyFile(string sourceFile, string destinationFile)
        {
            try
            {
                using (FileStream inputStream = File.Open(sourceFile, FileMode.Open),
                    outputStream = new FileStream(destinationFile, FileMode.OpenOrCreate))
                {
                    using (GZipStream zipper = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[inputStream.Length];
                        int counter = 0;
                        while ((counter = inputStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            zipper.Write(buffer, 0, counter);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UnZipMyFile(string sourceFile, string destinationFile)
        {
            try
            {
                using (FileStream inputStream = File.Open(sourceFile, FileMode.Open),
                    outputStream = new FileStream(destinationFile, FileMode.OpenOrCreate))
                {
                    using (GZipStream zipper = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        byte[] buffer = new byte[inputStream.Length];
                        int counter = 0;
                        while ((counter = zipper.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            outputStream.Write(buffer, 0, counter);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
