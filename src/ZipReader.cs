using Ionic.Zip;

namespace CsvTranslationPacker;

public static class ZipReader
{
    public static List<(string fileName, byte[] content)> GetFilesFromZip(string path)
    {
        var extractedFiles = new List<(string, byte[])>();

        using var zipFiles = ZipFile.Read(path);
        foreach (var file in zipFiles)
        {
            using var memoryStream = new MemoryStream();
            file.Extract(memoryStream);
            extractedFiles.Add((file.FileName, memoryStream.ToArray()));
        }

        return extractedFiles;
    } 
}