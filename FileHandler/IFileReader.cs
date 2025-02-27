namespace FileHandler
{
    public interface IFileReader
    {
        public Task<List<T>> ReadFileAsync<T>(string fileNameWithPath);
    }
}
