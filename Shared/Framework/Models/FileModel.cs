namespace Framework.Models
{
    public class FileModel
    {
        /// Gets the raw Content-Type header of the uploaded file.
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets the file length in bytes.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets the form field name from the Content-Disposition header.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets the file name from the Content-Disposition header.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        public string Folder { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;
    }
}

