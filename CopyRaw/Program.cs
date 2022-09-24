string GetExecutingAssemblyDirectory()
{
    return Environment.CurrentDirectory;
}

try
{
    ConsoleExtension.Hide();
    var serverDirectoryPath = @"C:\Users\Gulyaev\Desktop\флешка";
    Console.WriteLine($"{nameof(serverDirectoryPath)} = {serverDirectoryPath}");

    var assemblyDirectoryPath = GetExecutingAssemblyDirectory();
    Console.WriteLine($"{nameof(assemblyDirectoryPath)} = {assemblyDirectoryPath}");

    Directory.GetFiles(assemblyDirectoryPath, "*.jpeg").ToList().ForEach(Console.WriteLine);

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
            Console.WriteLine($"{nameof(copyFile.LocalRawPath)} = {copyFile.LocalRawPath}");
            Console.WriteLine($"{nameof(copyFile.ServerRawPath)} = {copyFile.ServerRawPath}");
            File.Copy(copyFile.ServerRawPath, copyFile.LocalRawPath);
        });
}
catch(Exception exception)
{
    ConsoleExtension.Show();
    Console.WriteLine(exception);
    Console.ReadKey();
}