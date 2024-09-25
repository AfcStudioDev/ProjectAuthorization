using Authorization.Application.Domain.Requests.FilesUpload;
using Authorization.Application.Domain.Responses.FilesUpload;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Authorization.Application.Domain.Handler.FilesUpload
{
    public class GetFilesUploadHandler : IRequestHandler<GetFilesUploadRequest, GetFilesUploadResponse>
    {
        private const string _folderDistr = "distrib";
        private const string _androidApp = "base.apk";
        private const string _androidTabletApp = "tabletBase.apk";
        private const string _windowsApp = "base.exe";
        public async Task<GetFilesUploadResponse> Handle( GetFilesUploadRequest request, CancellationToken cancellationToken )
        {
            string path = _folderDistr;
            switch (request.Platform)
            {
                case Enums.PlatformType.Android:
                    path = Path.Combine( path, _androidApp );
                    break;
                case Enums.PlatformType.Windows:
                    path = Path.Combine( path, _windowsApp );
                    break;
				case Enums.PlatformType.AndroidTablet:
					path = Path.Combine( path, _androidTabletApp );
					break;
			}

            GetFilesUploadResponse response = new GetFilesUploadResponse();
            Stream? stream = null;

            if (File.Exists( path ))
            {
                stream = File.OpenRead( path );
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Файл не найден";
            }

            response.File = new FileStreamResult( stream, "application/octet-stream" )
            {
                FileDownloadName = Path.GetFileName( path )
            };

            return response;
        }
    }
}
