import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.scss']
})
export class TopHeaderComponent implements OnInit {
  datasetConfirmForm : FormGroup = new FormGroup({ 
    "datasetName": new FormControl("", [Validators.required, Validators.maxLength(25)]),
  });

  private headerState: HeaderState = HeaderState.Idle;
  private fileImporterMessage: string = "Choose data file...";

  get HeaderState(): HeaderState {
    return this.headerState;
  }

  get FileImporterMessage(): string {
    return this.fileImporterMessage;
  }

  constructor() { }

  ngOnInit() {
  }

  onNewDatasetCreated() {
    this.headerState = HeaderState.Dialog;
  }

  onDatasetChangesSaved() {

  }

  onFileImported() {
    
  }

  onDialogCanceled() {
    this.headerState = HeaderState.Idle;
  }

  onDialogConfirmed() {
    this.headerState = HeaderState.Idle;
  }
}

enum HeaderState {
  Idle = 0,
  Dialog = 1
}
