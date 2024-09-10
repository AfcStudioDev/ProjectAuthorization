using Authorization.Application.Domain.Requests.FilesUpload;
using Authorization.Application.Domain.Responses.FilesUpload;
using MediatR;

namespace Authorization.Application.Domain.Handler.FilesUpload
{
    public class GetFilesUploadHandler : IRequestHandler<GetFilesUploadRequest, GetFilesUploadResponse>
    {
        private const string _folderDistr = "distrib";
        private const string _androidApp = "base.apk";
        private const string _iosApp = "base.ipa";
        private const string _windowsApp = "base.exe";
        public async Task<GetFilesUploadResponse> Handle(GetFilesUploadRequest request, CancellationToken cancellationToken)
        {
            string path = _folderDistr;
            switch (request.Platform)
            {
                case Enums.PlatformType.Android:
                    path = Path.Combine(path, _androidApp);
                    break;
                case Enums.PlatformType.IOS:
                    path = Path.Combine(path, _iosApp);
                    break;
                case Enums.PlatformType.Windows:
                    path = Path.Combine(path, _windowsApp);
                    break;

            }
            var response = new GetFilesUploadResponse();
            Stream? stream = null;

            if (File.Exists(path))
            {
                stream = File.OpenRead(path);
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Файл не найден";
            }

            return new GetFilesUploadResponse() { File = stream };
        }
    }
}
