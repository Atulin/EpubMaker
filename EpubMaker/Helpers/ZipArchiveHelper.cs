﻿using System.IO.Compression;

namespace EpubMaker.Helpers;

public static class ZipArchiveHelper
{
    public static async Task AddFile(this ZipArchive file, string path, string? content)
    {
        var archiveEntry = file.CreateEntry(path, CompressionLevel.NoCompression);
        await using var streamWriter = new StreamWriter(archiveEntry.Open());
        await streamWriter.WriteAsync(content);
        streamWriter.Close();
    }
}