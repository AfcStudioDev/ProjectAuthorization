using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Authorization.Application.Domain.Responses.FilesUpload
{
    public class GetFilesUploadResponse : BaseResponse
    {
        public FileStreamResult? File { get; set; }
    }
}
