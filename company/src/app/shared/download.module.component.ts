import { DownloadDistrService } from '../http/downloadDistr.service';
import { GetFilesUploadRequest } from '../../requests/Download/GetFilesUploadRequest';
import { Component} from '@angular/core';

@Component({
  selector: 'app-download-module',
  standalone: true,
  templateUrl: './download.module.component.html',
  styleUrl: './download.module.component.css'
})
export class DownloadModuleComponent {
  constructor(private downloadDistrService: DownloadDistrService) {
  }

  public OnDownLoadClick(platformType: number) {
    let request = new GetFilesUploadRequest(platformType);
    this.downloadDistrService.GetDistr(request);
  }
}