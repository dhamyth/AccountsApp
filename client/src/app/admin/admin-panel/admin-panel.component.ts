import { Component } from '@angular/core';
import { FileUploadComponent } from '../../file-upload/file-upload.component';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [FileUploadComponent],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {

}
