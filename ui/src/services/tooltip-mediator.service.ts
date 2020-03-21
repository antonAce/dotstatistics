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

  private datasetCreationEvent: EventEmitter<string> = new EventEmitter<string>();
  private datasetSaveChangesEvent: EventEmitter<void> = new EventEmitter<void>();
}
