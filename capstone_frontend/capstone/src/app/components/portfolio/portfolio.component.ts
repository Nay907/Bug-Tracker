import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { BugsService } from 'src/app/shared/services/bugs.service';
import Chart, { ChartData, ChartOptions, registerables } from 'chart.js/auto';
import { Portfolio } from 'src/app/shared/models/Portfolio';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.scss']
})
export class PortfolioComponent implements AfterViewInit{
  @ViewChild('bugChartCanvas') bugChartCanvas: ElementRef<HTMLCanvasElement>;
  summary!: Portfolio;
  totalBugCount: number = 0;
  bugCountByLowSeverity: number = 0;
  bugCountByMediumSeverity: number = 0;
  bugCountByHighSeverity: number = 0;
  chart: Chart | undefined;
  chartOptions: ChartOptions = {
    responsive: true,
  };
  showChartFlag1 = false;
  constructor(private bugsService: BugsService) { }

  ngAfterViewInit(): void {
    this.getBugsData();
  }
  getBugsData(): void {
    this.bugsService.getTotalBugCount().subscribe(
      (count) => {
        this.totalBugCount = count;
        //this.createBugChart();
      },
      (error) => {
        console.log(error); // Handle error appropriately
      }
    );

    this.bugsService.getBugCountByLowSeverity().subscribe(
      (count) => {
        this.bugCountByLowSeverity = count;
        //this.createBugChart();
      },
      (error) => {
        console.log(error); // Handle error appropriately
      }
    );

    this.bugsService.getBugCountByMediumSeverity().subscribe(
      (count) => {
        this.bugCountByMediumSeverity = count;
        //this.createBugChart();
      },
      (error) => {
        console.log(error); // Handle error appropriately
      }
    );

    this.bugsService.getBugCountByHighSeverity().subscribe(
      (count) => {
        this.bugCountByHighSeverity = count;
        //this.createBugChart();
      },
      (error) => {
        console.log(error); // Handle error appropriately
      }
    );
  }

  // createBugChart(): void {
  //   if (this.totalBugCount && this.bugCountByLowSeverity && this.bugCountByMediumSeverity && this.bugCountByHighSeverity) {
  //     Chart.register(...registerables);

  //     const bugChartCanvas = this.bugChartCanvas.nativeElement.getContext('2d');
  //     new Chart(bugChartCanvas, {
  //       type: 'bar',
  //       data: {
  //         labels: ['Total', 'Low', 'Medium', 'High'],
  //         datasets: [
  //           {
  //             label: 'Bug Count',
  //             data: [this.totalBugCount, this.bugCountByLowSeverity, this.bugCountByMediumSeverity, this.bugCountByHighSeverity],
  //             backgroundColor: 'rgba(75, 192, 192, 0.2)',
  //             borderColor: 'rgba(75, 192, 192, 1)',
  //           }
  //         ]
  //       },
  //       options: {
  //         scales: {
  //           y: {
  //             beginAtZero: true
  //           }
  //         }
  //       }
  //     });
  //   }
  // }
  chartData1: ChartData = {
    labels: ['Sales', 'Purchase'],
    datasets: [
      {
        label: 'Quantity',
        data: [50, 30, 70, 40, 60, 50, 80],
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
      },
    ],
  };
  updateChartData1(summary: Portfolio): void {
    if (this.chartData1.datasets) {
      this.chartData1.datasets[0].data = [
        summary.totalBugCount,
        summary.bugCountByHighSeverity,
        summary.bugCountByMediumSeverity,
        summary.bugCountByLowSeverity
      ];
    }
  }
  showChart1(): void {
    this.showChartFlag1 = !this.showChartFlag1;
    if (this.showChartFlag1) {
      this.updateChartData1(this.summary);
      setTimeout(() => {
        this.renderChart1();
      }, 0);
    } else {
      setTimeout(()=>{this.closeChart1();},0)
    }
  }
 
  closeChart1(): void {
    this.showChartFlag1 = false;
    if (this.chart) {
      this.chart.destroy();
    }
  }
  renderChart1(): void {
    const canvas = this.bugChartCanvas.nativeElement;
    if (canvas) {
      const ctx = canvas.getContext('2d');
      if (ctx) {
        Chart.register(...registerables);
        this.chart = new Chart(ctx, {
          type: 'bar',
          data: this.chartData1,
          options: this.chartOptions,
        });
      }
    }
  }
}
