using Ionic.Zip;

namespace CsvTranslationPacker.Services;

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

    public static string GetZipFile(string inputPath)
    {
        return Directory.GetFiles("./", inputPath).FirstOrDefault() 
            ?? throw new IOException($"No file found by search '{inputPath}'");
    }
}