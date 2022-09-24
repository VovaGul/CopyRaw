using System.Reflection;

string GetExecutingAssemblyPath()
{
    string codeBase = Assembly.GetExecutingAssembly().CodeBase;
    UriBuilder uri = new UriBuilder(codeBase);
    return Uri.UnescapeDataString(uri.Path);
}

string GetExecutingAssemblyDirectory()
{
    return Path.GetDirectoryName(GetExecutingAssemblyPath());
}

try
{
    var serverDirectoryPath = @"C:\Users\Gulyaev\Desktop\флешка";

    var assemblyDirectoryPath = GetExecutingAssemblyDirectory();

    Directory.GetFiles(assemblyDirectoryPath, "*.jpeg")
        .Select(Path.GetFileNameWithoutExtension)
        .Select(jpegFileNameWithoutExtension => $"{jpegFileNameWithoutExtension}.raw")
        .Select(rawFileName => new
        {
            ServerRawPath = Path.Combine(serverDirectoryPath, rawFileName),
            LocalRawPath = Path.Combine(assemblyDirectoryPath, $"равы {DateTime.Now:yyyy/MM/dd hh.mm.ss}", rawFileName)
        })
        .ToList()
        .ForEach(copyFile =>
        {
            var copyFileLocalDirectoryPath = Path.GetDirectoryName(copyFile.LocalRawPath);
            Directory.CreateDirectory(copyFileLocalDirectoryPath);

            File.Copy(copyFile.ServerRawPath, copyFile.LocalRawPath);
        });
}
catch(Exception exception)
{
    Console.WriteLine(exception);
    Console.ReadKey();
}