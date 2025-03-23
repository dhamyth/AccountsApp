import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FileUploadService } from '../_services/file-upload.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css'
})
export class FileUploadComponent {
  private toastr = inject(ToastrService);
  private router = inject(Router);

  selectedFile: File | null = null;
  uploadResult: any = null;
  errorMessage: string | null = null;
  fileUploadService = inject(FileUploadService);

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  uploadFile(): void {
    if (!this.selectedFile) {
      this.errorMessage = 'Please select a file first.';
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile, this.selectedFile.name);

    this.fileUploadService.upload(formData).subscribe({
      next: response => {
        this.toastr.success("File upload successful");
        this.router.navigateByUrl('/current-balance');
      },
      error: error => this.toastr.error(error.error)
    });
  }

}
