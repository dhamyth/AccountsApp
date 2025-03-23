import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  upload(formData: FormData){
    return this.http.post(this.baseUrl +  'FileUpload/upload', formData);
  }
}
