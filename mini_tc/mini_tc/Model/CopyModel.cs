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
            if (Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Contains(Path.GetFileName(source)))
            {
                int count = Directory.GetFiles(target).Select(x => Path.GetFileName(x)).Where(x => x.StartsWith(Path.GetFileNameWithoutExtension(source))).Count();
                string fileName = Path.GetFileNameWithoutExtension(source) + " - " + Resources.FileCopy + count + Path.GetExtension(source);
                target = Path.Combine(target, fileName);
            }
            else
            {
                target = Path.Combine(target, Path.GetFileName(source));
            }
            try
            {
                File.Copy(source, target);
            }
            catch (UnauthorizedAccessException) { return; }
        }


        private void DirectoryCopy(string source, string target)
        {
            var dir = new DirectoryInfo(source);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            DirectoryInfo[] dirs;
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (UnauthorizedAccessException) { return; }
        }
    }
}
