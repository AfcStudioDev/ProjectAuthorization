import { DownloadDistrService } from '../http/downloadDistr.service';
import { GetFilesUploadRequest } from '../../requests/Download/GetFilesUploadRequest';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [],
  imports: [],
  providers: [DownloadDistrService],
  bootstrap: [],
})
export class DownloadModule {
  constructor(private downloadDistrService: DownloadDistrService) {
  }

  public OnDownLoadClick(platformType: number) {
    let request = new GetFilesUploadRequest(platformType);
    this.downloadDistrService.GetDistr(request);
  }
}