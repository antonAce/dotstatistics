import { Component, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { TooltipMediatorService } from '@services/tooltip-mediator.service';
import { FileUploadService } from '@services/file-upload.service';

@Component({
  selector: 'top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.scss']
})
export class TopHeaderComponent implements OnDestroy {
  datasetConfirmForm : FormGroup = new FormGroup({ 
    "datasetName": new FormControl("", [Validators.required, Validators.maxLength(25)]),
  });

  private readonly defaultImporterMessage: string = "Choose data file...";

  private headerState: HeaderState = HeaderState.Idle;
  private fileImporterMessage: string = this.defaultImporterMessage;

  private uploadedFile: any = null;

  private fileUpload$ = new Subscription();

  get HeaderState(): HeaderState {
    return this.headerState;
  }

  get FileImporterMessage(): string {
    return this.fileImporterMessage;
  }

  constructor(private mediator: TooltipMediatorService,
              private fileUploader: FileUploadService) { }

  onNewDatasetCreated() {
    this.headerState = HeaderState.Dialog;
  }

  onDatasetChangesSaved() {
    this.mediator.datasetSaveChanges.emit();
  }

  onFileImported() {
    this.headerState = HeaderState.FileImport;
  }

  onDialogCanceled() {
    if (this.headerState == HeaderState.FileImport) {
      this.fileImporterMessage = this.defaultImporterMessage;
      this.uploadedFile = null;
    }

    this.headerState = HeaderState.Idle;
  }

  onFileUploaded(event: any) {
    if (event.target.files.length > 0) {
      this.uploadedFile = event.target.files[0];
      this.fileImporterMessage = this.uploadedFile.name;
    }
  }

  onDialogConfirmed() {
    if (this.headerState == HeaderState.Dialog) {
      this.mediator.datasetCreation.emit(this.datasetConfirmForm.controls['datasetName'].value as string);
    } else if (this.headerState == HeaderState.FileImport) {
      this.fileUpload$ = this.fileUploader.uploadFile(this.uploadedFile)
        .subscribe(
          (value) => this.mediator.fileUploaded.emit(value)
        );
      this.fileImporterMessage = this.defaultImporterMessage;
      this.uploadedFile = null;
    }
    this.headerState = HeaderState.Idle;
  }

  ngOnDestroy() {
    this.fileUpload$.unsubscribe();
  }
}

enum HeaderState {
  Idle = 0,
  Dialog = 1,
  FileImport = 2
}
