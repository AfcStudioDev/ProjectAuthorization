using Authorization.Application.Domain.Enums;
using Authorization.Application.Domain.Responses.FilesUpload;

using MediatR;

namespace Authorization.Application.Domain.Requests.FilesUpload
{
    public class GetFilesUploadRequest : IRequest<GetFilesUploadResponse>
    {
        public PlatformType Platform { get; set; }
    }
}
