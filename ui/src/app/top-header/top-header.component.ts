import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { TooltipMediatorService } from '@services/tooltip-mediator.service';

@Component({
  selector: 'top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.scss']
})
export class TopHeaderComponent {
  datasetConfirmForm : FormGroup = new FormGroup({ 
    "datasetName": new FormControl("", [Validators.required, Validators.maxLength(25)]),
  });

  private headerState: HeaderState = HeaderState.Idle;
  private fileImporterMessage: string = "Choose data file...";

  private uploadedFile: any = null;

  get HeaderState(): HeaderState {
    return this.headerState;
  }

  get FileImporterMessage(): string {
    return this.fileImporterMessage;
  }

  constructor(private mediator: TooltipMediatorService) { }

  onNewDatasetCreated() {
    this.headerState = HeaderState.Dialog;
  }

  onDatasetChangesSaved() {
    this.mediator.datasetSaveChanges.emit();
  }

  onFileImported() {
    this.headerState = HeaderState.Dialog;
  }

  onDialogCanceled() {
    this.headerState = HeaderState.Idle;
  }

  onFileUploaded(event: any) {
    if (event.target.files.length > 0) {
      this.uploadedFile = event.target.files[0];
      this.fileImporterMessage = this.uploadedFile.name;
    }
  }

  onDialogConfirmed() {
    this.mediator.datasetCreation.emit(this.datasetConfirmForm.controls['datasetName'].value as string);
    this.headerState = HeaderState.Idle;
  }
}

enum HeaderState {
  Idle = 0,
  Dialog = 1
}
