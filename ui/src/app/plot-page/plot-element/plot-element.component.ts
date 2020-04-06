import { Component, OnInit, Input, ElementRef, ViewChild } from '@angular/core';

import * as d3 from 'd3';

import { ComparingPairs } from '@models/analytics';

@Component({
  selector: 'plot-element',
  templateUrl: './plot-element.component.html',
  styleUrls: ['./plot-element.component.scss']
})
export class PlotElementComponent implements OnInit {
  @ViewChild('chart', { static: true }) chart: ElementRef;

  @Input() data: ComparingPairs[];

  @Input() width: number;
  @Input() height: number;

  private chartProps: any;

  constructor() { }

  ngOnInit() {
    this.buildChart();
  }

  buildChart() {
    const discreteColor = "#BF1736";
    const approxColor = "#0D1440";

    this.chartProps = {};
  
    let margin = { top: 30, right: 20, bottom: 30, left: 50 }, 
      _width = this.width - margin.left - margin.right, 
      _height = this.height - margin.top - margin.bottom;
  
    this.chartProps.x = d3.scaleLinear().range([0, _width]);
    this.chartProps.y = d3.scaleLinear().range([_height, 0]);
  
    let xAxis = d3.axisBottom(this.chartProps.x);
    let yAxis = d3.axisLeft(this.chartProps.y).ticks(5);
  
    let discreteLine = d3.line<ComparingPairs>()
      .x((record) => this.chartProps.x(record.argument))
      .y((record) => this.chartProps.y(record.discrete));

    let approxLine = d3.line<ComparingPairs>()
      .x((record) => this.chartProps.x(record.argument))
      .y((record) => this.chartProps.y(record.approximate));
  
    let svg = d3.select(this.chart.nativeElement)
      .append('svg')
      .attr('width', _width + margin.left + margin.right)
      .attr('height', _height + margin.top + margin.bottom)
      .append('g')
      .attr('transform', `translate(${margin.left},${margin.top})`);
  
    this.chartProps.x.domain([
      d3.min(this.data, (record) => record.argument), 
      d3.max(this.data, (record) => record.argument)]);
    this.chartProps.y.domain([
      d3.min(this.data, (record) => Math.min(record.approximate, record.argument)), 
      d3.max(this.data, (record) => Math.max(record.approximate, record.argument))]);
  
    svg.append('path')
    .attr('class', 'line line2')
    .style('stroke', discreteColor)
    .style('fill', 'none')
    .attr("stroke-width", 5)
    .attr('d', discreteLine(this.data));

  svg.append('path')
    .attr('class', 'line line1')
    .style('stroke', approxColor)
    .style('fill', 'none')
    .attr("stroke-width", 5)
    .attr('d', approxLine(this.data));
  
    svg.append('g')
      .attr('class', 'x axis')
      .attr('transform', `translate(0,${_height})`)
      .call(xAxis);
  
    svg.append('g')
      .attr('class', 'y axis')
      .call(yAxis);

    svg.append("circle").attr("cx", 30).attr("cy", 15).attr("r", 6).style("fill", discreteColor);
    svg.append("circle").attr("cx", 30).attr("cy", 45).attr("r", 6).style("fill", approxColor);
    svg.append("text").attr("x", 50).attr("y", 15).text("variable A").style("fill", discreteColor).style("font-size", "15px").attr("alignment-baseline","middle");
    svg.append("text").attr("x", 50).attr("y", 45).text("variable B").style("fill", approxColor).style("font-size", "15px").attr("alignment-baseline","middle");

    this.chartProps.svg = svg;
    this.chartProps.valueline = discreteLine;
    this.chartProps.valueline2 = approxLine;
    this.chartProps.xAxis = xAxis;
    this.chartProps.yAxis = yAxis;
  }
}
