using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ImageEditor;

public class FilePickerHelper
{
    public static async Task<string> OpenFilePicker(Window parent)
    {
        var topLevel = TopLevel.GetTopLevel(parent);

        if (topLevel?.StorageProvider == null)
        {
            Console.WriteLine("Error: Storage Providor not available");
            return null;
        }

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Text files") { Patterns = new[] { "*b2img.txt" } },
                },
            }
        );

        if (files == null || files.Count == 0)
        {
            Console.WriteLine("No file selected. Dialog closed or canceled.");
            return null;
        }

        return files.FirstOrDefault()?.Path.AbsolutePath;
    }
}

public class FileSaverHelper
{
    public static async Task<string> SaveFilePicker(TopLevel parent)
    {
        var topLevel = TopLevel.GetTopLevel(parent);
        if (topLevel?.StorageProvider == null)
        {
            Console.WriteLine("Error: Storage Providor not available");
            return null;
        }

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(
            new FilePickerSaveOptions
            {
                Title = "Save Drawing",
                DefaultExtension = ".b2img.txt",
                ShowOverwritePrompt = true,
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("Text File") { Patterns = new[] { "*.b2img.txt" } },
                },
            }
        );

        if (file == null)
        {
            Console.WriteLine("File save canceled.");
            return null;
        }

        return file.Path.AbsolutePath;
    }
}
