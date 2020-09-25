import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TooltipMediatorService {
  get datasetCreation() {
    return this.datasetCreationEvent;
  }

  get datasetSaveChanges() {
    return this.datasetSaveChangesEvent;
  }

  get datasetDrop() {
    return this.datasetDropEvent;
  }

  get fileUploaded() {
    return this.fileUploadedEvent;
  }

  private datasetCreationEvent: EventEmitter<string> = new EventEmitter<string>();
  private fileUploadedEvent: EventEmitter<string> = new EventEmitter<string>();
  private datasetSaveChangesEvent: EventEmitter<void> = new EventEmitter<void>();
  private datasetDropEvent: EventEmitter<void> = new EventEmitter<void>();
}
