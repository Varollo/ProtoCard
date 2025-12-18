using System.IO;
using System.IO.Compression;

public static class Utils
{
    public static void MergeZipFiles(string sourceZipPath, string targetZipPath)
    {
        using ZipArchive targetArchive = ZipFile.Open(targetZipPath, ZipArchiveMode.Update);
        using ZipArchive sourceArchive = ZipFile.OpenRead(sourceZipPath);

        foreach (ZipArchiveEntry entry in sourceArchive.Entries)
        {
            // Create a new entry in the target with the same name
            ZipArchiveEntry newEntry = targetArchive.CreateEntry(entry.FullName);

            // Copy the contents from the source entry to the new target entry
            using Stream sourceStream = entry.Open();
            using Stream targetStream = newEntry.Open();

            sourceStream.CopyTo(targetStream);
        }
    }
}