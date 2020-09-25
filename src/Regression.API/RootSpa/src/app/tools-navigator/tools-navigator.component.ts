import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'tools-navigator',
  templateUrl: './tools-navigator.component.html',
  styleUrls: ['./tools-navigator.component.scss']
})
export class ToolsNavigatorComponent{
  modelId: Observable<string>;

  constructor(private activateRoute: ActivatedRoute) {
    this.modelId = this.activateRoute.params.pipe(map(param => param['id']));
  }
}
