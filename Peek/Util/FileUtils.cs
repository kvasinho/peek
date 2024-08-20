
namespace Peek.Util;

public static class FileUtils
{
    public static long GetFileSizeBytes(string filepath)
    {
        FileInfo fileInfo = new FileInfo(filepath);

        return fileInfo.Length;
    }

    public static string GetFileSize(string filepath)
    {
        FileInfo fileInfo = new FileInfo(filepath);
        long bytes = fileInfo.Length;

        if (bytes < 1024)
        {
            return $"{bytes} B";
        }
        if (bytes < 1024 * 1024)
        {
            return $"{(bytes / 1024.0):F2} KB";
        }
        if (bytes < 1024 * 1024 * 1024)
        {
            return $"{(bytes / 1024.0 / 1024.0):F2} MB";
        }
        return $"{(bytes / 1024.0 / 1024.0 / 1024.0):F2} GB";
        
    }

    public static string GetFileName(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Filepath cannot be null or empty", nameof(filePath));
        }

        int lastSeparator = filePath.LastIndexOfAny(new char[] { '\\', '/'});

        if (lastSeparator == -1) return filePath;

        return filePath.Substring(lastSeparator + 1);
    }

    public enum PathStatus
    {
        ValidFreePath,
        FileExists,
        MissingDirectories,
        InvalidPath,
    }

    public static PathStatus ValidateFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || filePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            return PathStatus.InvalidPath;
        }
        
        string directoryPath = Path.GetDirectoryName(filePath)!;
        
        if (!Directory.Exists(directoryPath))
        {
            return PathStatus.MissingDirectories;
        }

        if (File.Exists(filePath))
        {
            return PathStatus.FileExists;
        }

        return PathStatus.ValidFreePath;
    }
}