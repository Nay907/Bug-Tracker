import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BugsService } from 'src/app/shared/services/bugs.service';
import { Bugs } from 'src/app/shared/models/Bugs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{
  displayedBugs: Bugs[] = [];
 filteredBugs: Bugs[] = [];
 searchQuery: string = '';
 filterStatus: string = '';
 filterSeverity: string = '';
 filterTesterName: string = '';
 filterDeveloperName: string = '';
 filterBugId: string = '';
 selectedBugId: number | null = null;

  constructor(private BugsService: BugsService, private router:Router) {}

  ngOnInit(): void {
    this.fetchBugs();
  }

  fetchBugs(): void {
    this.BugsService.getBugs().subscribe(
      (bugs: Bugs[]) => {
        this.displayedBugs = bugs;
        this.applyFilters();
      },
      (error) => {
        console.error('Error fetching bugs', error);
      }
    );
  }

  createBug(): void {
    // handle logic to create a new bug
    /*const newBug: Bugs = {
      bugId: 0, // Set as per your requirement
      title: '', // Set the bug title
      description: '', // Set the bug description
      severity: '', // Set the bug severity
      stepsToReproduce: '', // Set the steps to reproduce the bug
      status: '', // Set the bug status
      expectedResult: '', // Set the expected result
      actualResult: '', // Set the actual result
      filePath: '', // Set the file path
      testerName: '', // Set the tester ID as per your requirement
      developerName: '', // Set the developer ID as per your requirement
      createdAt: '', // Set the created at timestamp
      updatedAt: '', // Set the updated at timestamp
      completedAt: '' // Set the completed at timestamp
    };
  
    this.BugsService.createBug(newBug).subscribe(
      (bug: Bugs) => {
        // Handle the created bug object
        console.log('Bug created', bug);
        // Add the created bug to the displayedBugs array
        this.displayedBugs.push(bug);
        // Apply filters again to update the filteredBugs array
        this.applyFilters();
      },
      (error) => {
        console.error('Error creating bug', error);
      }
    );*/
    this.router.navigate(['/create']);
  }

  deleteSelectedBugs(): void {
    // handle logic to delete selected bugs
    if (this.selectedBugId) {
      this.BugsService.deleteBug(this.selectedBugId).subscribe(
        () => {
          console.log('Bug deleted');
          this.fetchBugs(); // Refresh the list after deletion
        },
        (error) => {
          console.error('Error deleting bug', error);
        }
      );
    }
  }

  applyFilters(): void {
    this.filteredBugs = this.displayedBugs.filter(bug => {
      const statusMatch = this.filterStatus ? bug.status.toLowerCase() === this.filterStatus.toLowerCase() : true;
      const severityMatch = this.filterSeverity ? bug.severity.toLowerCase() === this.filterSeverity.toLowerCase() : true;
      const testerIdMatch = this.filterTesterName ? bug.testerName.toString() === this.filterTesterName : true;
      const developerIdMatch = this.filterDeveloperName ? bug.developerName.toString() === this.filterDeveloperName : true;
      const bugIdMatch = this.filterBugId ? bug.bugId.toString() === this.filterBugId : true;
      const titleMatch = this.searchQuery ? bug.title.toLowerCase().includes(this.searchQuery.toLowerCase()) : true;

      return statusMatch && severityMatch && testerIdMatch && developerIdMatch && bugIdMatch && titleMatch;
    });
  }
  

}
