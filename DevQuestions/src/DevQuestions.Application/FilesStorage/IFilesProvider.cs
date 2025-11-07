namespace DevQuestions.Application.FileStorage;

public interface IFilesProvider
{
    public Task<string> UploadAsync(Stream stream, string key, string bucket);
}