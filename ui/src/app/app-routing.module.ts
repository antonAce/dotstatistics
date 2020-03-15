import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { AnalyticsPageComponent } from './analytics-page/analytics-page.component';
import { ModelNotFoundPageComponent } from './model-not-found-page/model-not-found-page.component';


const routes: Routes = [
  { path: ':id', component: StatisticsPageComponent },
  { path: ':id/analytics', component: AnalyticsPageComponent },
  { path: ':id/plot', component: StatisticsPageComponent },
  { path: '**', component: ModelNotFoundPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
