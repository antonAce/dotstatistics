import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { AnalyticsPageComponent } from './analytics-page/analytics-page.component';


const routes: Routes = [
  { path: 'statistics', component: StatisticsPageComponent },
  { path: 'analytics', component: AnalyticsPageComponent },
  { path: 'plot', component: StatisticsPageComponent },
  { path: '', redirectTo: '/statistics', pathMatch: 'full' },
  { path: '**', redirectTo: '/statistics', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
