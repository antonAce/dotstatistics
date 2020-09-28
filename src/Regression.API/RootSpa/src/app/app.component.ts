import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  items: MenuItem[] = [
    {
      label: 'Workspace',
      items: [
        {
          label: 'New',
          icon: 'pi pi-fw pi-plus'
        },
        {
          label: 'Open',
          icon: 'pi pi-fw pi-folder-open'
        },
        {
          label: 'Save',
          icon: 'pi pi-fw pi-save',
          items: [
            {
              label: 'Save as'
            }
          ]
        },
        {
          label: 'Import',
          icon: 'pi pi-fw pi-cloud-upload'
        },
        {
          label: 'Export',
          icon: 'pi pi-fw pi-cloud-download'
        },
        {
          label: 'Delete',
          icon: 'pi pi-fw pi-trash'
        },
        {
          label: 'Refresh',
          icon: 'pi pi-fw pi-refresh'
        }
      ]
    },
    {
      label: 'Edit',
      items: [
        {
          label: 'Cut'
        },
        {
          label: 'Copy'
        },
        {
          label: 'Paste'
        }
      ]
    },
    {
      label: 'Analysis',
      items: [
        {
          label: 'Models'
        }
      ]
    }
  ];
}
