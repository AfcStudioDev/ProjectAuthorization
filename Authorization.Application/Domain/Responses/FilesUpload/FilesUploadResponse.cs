namespace Authorization.Application.Domain.Responses.FilesUpload
{
    public class GetFilesUploadResponse : BaseResponse
    {
        public Stream? File { get; set; }
    }
}
