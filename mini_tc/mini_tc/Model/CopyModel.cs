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
            var attribute = File.GetAttributes(source);
            if (attribute.HasFlag(FileAttributes.Directory))
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
            //if we want copy the same file again
            if (Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Contains(Path.GetFileName(source)))
            {
                int count = Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Where(x => x.StartsWith(Path.GetFileNameWithoutExtension(source))).Count(); //counint occurs of file
                string fileName = Path.GetFileNameWithoutExtension(source) + Resources.FileCopy +"_("+ count + ")" +Path.GetExtension(source); //make new name of duplicate copy
                target = Path.Combine(target, fileName); //target
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
            var dir = new DirectoryInfo(source); //soruce dir

            if (!dir.Exists) //if exist
                throw new DirectoryNotFoundException();
            DirectoryInfo[] dirs;
            try
            {
                dirs = dir.GetDirectories(); 
            }
            catch (UnauthorizedAccessException) { return; }

            if (!Directory.Exists(target)) //if we want copy the same dir again
                Directory.CreateDirectory(target);
            else
            {
                int count = Directory.GetDirectories(Directory.GetDirectoryRoot(target)).Where(x => x.StartsWith(target)).Count(); //we count occurs of dir
                target = Path.Combine(Path.GetDirectoryName(target), Path.GetFileNameWithoutExtension(target) + Resources.FileCopy + "_(" + count + ")"); //new name for copied dir
                Directory.CreateDirectory(target); //target of dir
            }

            //each file of dir to copy
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                string path = Path.Combine(target, file.Name);
                file.CopyTo(path);
            }
            
            //each subdirs
            foreach (var subdir in dirs)
            {
                string path = Path.Combine(target, subdir.Name);
                DirectoryCopy(subdir.FullName, path);
            }
        }
    }
}
