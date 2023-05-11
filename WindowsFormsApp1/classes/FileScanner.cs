using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WindowsFormsApp1.classes
{
    internal class FileScanner
    {
        public void ScanDirectory(string directoryPath, out List<string> xmlFiles, out List<string> xsdFiles)
        {
            xmlFiles = new List<string>();
            xsdFiles = new List<string>();

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory {directoryPath} does not exist.");
            }

            try
            {
                // Get XML files
                xmlFiles.AddRange(Directory.EnumerateFiles(directoryPath, "*.xml", SearchOption.AllDirectories));

                // Get XSD files
                xsdFiles.AddRange(Directory.EnumerateFiles(directoryPath, "*.xsd", SearchOption.AllDirectories));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
