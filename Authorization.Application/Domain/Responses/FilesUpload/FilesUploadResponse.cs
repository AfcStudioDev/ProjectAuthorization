using Microsoft.AspNetCore.Mvc;

namespace Authorization.Application.Domain.Responses.FilesUpload
{
    public class GetFilesUploadResponse : BaseResponse
    {
        public FileStreamResult? File { get; set; }
    }
}
