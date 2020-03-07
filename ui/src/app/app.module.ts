import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopHeaderComponent } from './top-header/top-header.component';
import { ModelTooltipComponent } from './model-tooltip/model-tooltip.component';
import { StatisticsTableComponent } from './statistics-table/statistics-table.component';
import { StatisticsCellComponent } from './statistics-table/statistics-cell/statistics-cell.component';
import { StatisticsPageComponent } from './statistics-page/statistics-page.component';
import { ToolsNavigatorComponent } from './tools-navigator/tools-navigator.component';

import { ModelSerializationService } from '@services/model-serialization.service';

@NgModule({
  declarations: [
    AppComponent,
    TopHeaderComponent,
    ModelTooltipComponent,
    StatisticsTableComponent,
    StatisticsCellComponent,
    StatisticsPageComponent,
    ToolsNavigatorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [ModelSerializationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
