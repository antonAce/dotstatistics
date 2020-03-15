import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'tools-navigator',
  templateUrl: './tools-navigator.component.html',
  styleUrls: ['./tools-navigator.component.scss']
})
export class ToolsNavigatorComponent{
  modelId: string;

  constructor(private activateRoute: ActivatedRoute) {
    this.modelId = this.activateRoute.snapshot.params['id'];
  }
}
