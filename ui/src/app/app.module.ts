import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { KatexModule } from 'ng-katex';

import { AppComponent } from './app.component';
import { TopHeaderComponent } from './top-header/top-header.component';
import { ModelTooltipComponent } from './model-tooltip/model-tooltip.component';
import { StatisticsTableComponent } from './statistics-table/statistics-table.component';
import { StatisticsCellComponent } from './statistics-table/statistics-cell/statistics-cell.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { ToolsNavigatorComponent } from './tools-navigator/tools-navigator.component';

import { ModelSerializationService } from '@services/model-serialization.service';
import { ModelAnalysisService } from '@services/model-analysis.service';

import { AnalyticsPageComponent } from './analytics-page/analytics-page.component';

@NgModule({
  declarations: [
    AppComponent,
    TopHeaderComponent,
    ModelTooltipComponent,
    StatisticsTableComponent,
    StatisticsCellComponent,
    StatisticsPageComponent,
    ToolsNavigatorComponent,
    AnalyticsPageComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    KatexModule
  ],
  providers: [
    ModelSerializationService,
    ModelAnalysisService],
  bootstrap: [AppComponent]
})
export class AppModule { }
