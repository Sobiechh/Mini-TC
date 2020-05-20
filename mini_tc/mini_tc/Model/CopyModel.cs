using System;
using System.Collections.Generic;
using System.Linq; //linq to get files/dirs
using mini_tc.Properties;
using System.IO;

namespace mini_tc.Model
{
    class CopyModel
    {
        public void Copy(string source, string target)
        {
            var attrib = File.GetAttributes(source);
            if (attrib.HasFlag(FileAttributes.Directory)) //if source is directory
            {
                target = Path.Combine(target, Path.GetFileName(source));
                DirectoryCopy(source, target);
            }
            else
            {
                FileCopy(source, target);
            }
        }

        private void FileCopy(string source, string target)
        {
            if (Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Contains(Path.GetFileName(source))) //if file occrus we create copy of copy
            {
                int count = Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Where(x => x.StartsWith(Path.GetFileNameWithoutExtension(source))).Count(); // counting file occurs
                string fileName = Path.GetFileNameWithoutExtension(source) + Resources.FileCopy + count + Path.GetExtension(source); // copy duplication
                //tests
                //Console.WriteLine(fileName+);
                target = Path.Combine(target, fileName);
            }
            else
            {
                target = Path.Combine(target, Path.GetFileName(source));
            }
            try
            {
                File.Copy(source, target);
            } //no access
            catch (UnauthorizedAccessException) { return; }
        }

        private void DirectoryCopy(string source, string target)
        {
            // get subdirs from directory
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory: {source}  could not be found");
            }

            
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            else //same situation as file copy
            { 
                int count = Directory.GetDirectories(Directory.GetDirectoryRoot(target)).Where(x => x.StartsWith(target)).Count(); // counting occurs
                target = Path.Combine(Path.GetDirectoryName(target), Path.GetFileNameWithoutExtension(target) + Resources.FileCopy + count); //copy duplication
                Directory.CreateDirectory(target); //make duplication folder
            }

            // Files into directory and copy to new location
            var files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(target, file.Name);
                file.CopyTo(temppath);
            }

            // copy subidirs to new location
            var subdirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in subdirs)
            {
                string temppath = Path.Combine(target, subdir.Name);
                DirectoryCopy(temppath, target);
            }
        }
    }
}
