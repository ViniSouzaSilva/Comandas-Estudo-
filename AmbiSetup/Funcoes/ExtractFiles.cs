using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AmbiSetup.Funcoes
{
    public class ExtractFiles
    {
        public async Task<bool> ExtraiArquivos(
                IProgress<(string textToReport,
                double percentage)> progress,
                string originPath,
                string destinationPath
            )
        {
            if (!File.Exists(originPath))
            {
                progress.Report(("ERROR. FILE NOT FOUND", 0));
                return false;
            }
            if (!originPath.EndsWith(".zip"))
            {
                progress.Report(("ERROR. WRONG FILE TYPE", 0));
                return false;
            }
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);
            ZipArchive zip = ZipFile.OpenRead(originPath);
            //int fileCount = (from entry in zip.Entries
            //                 where !String.IsNullOrWhiteSpace(entry.Name)
            //                 select entry).Count();
            int entryCount = zip.Entries.Count;
            for (int i = 0; i < entryCount; i++)
            {
                ZipArchiveEntry entry = zip.Entries[i];
                progress.Report(("Extraindo arquivo...", ((double)i / (double)entryCount)));
                if (String.IsNullOrWhiteSpace(entry.Name)) //É um diretório
                {
                    await Task.Run(() => Directory.CreateDirectory(Path.Combine(destinationPath, entry.FullName.Replace('/', '\\'))));
                }
                else
                {
                    await Task.Run(() => ExtractWithFolder(entry, destinationPath));
                }
                

            }
            return true;
        }

        private void ExtractWithFolder(ZipArchiveEntry entry, string extractPath)
        {
            string pathToExtractTo = Path.Combine(extractPath, entry.FullName.Replace('/','\\'));
            entry.ExtractToFile(pathToExtractTo, true);
                return;
        }
    }
}
