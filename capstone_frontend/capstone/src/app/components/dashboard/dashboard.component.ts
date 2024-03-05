import { Component, OnInit } from '@angular/core';
import { BugsService } from 'src/app/shared/services/bugs.service';
import { Bugs } from 'src/app/shared/models/Bugs';
import { Router } from '@angular/router';
import {FormGroup, FormControl,Validators} from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  displayedBugs: Bugs[] = [];
  filteredBugs: Bugs[] = [];
  searchQuery: string = '';
  filterStatus: string = '';
  filterSeverity: string = '';
  filterTesterName: string = '';
  filterDeveloperName: string = '';
  filterBugId: string = '';
  selectedBugId: number | null = null;


  searchForm = new FormGroup({
    searchQuery: new FormControl(''),
    filterStatus: new FormControl(''),
    filterSeverity: new FormControl(''),
    filterTesterName: new FormControl(''),
    filterDeveloperName: new FormControl(''),
    filterBugId: new FormControl(''),
 });

  constructor(private BugsService: BugsService, private router: Router) {}

  ngOnInit(): void {
    this.fetchBugs();
  }

  fetchBugs(): void {
    let projId = parseInt(sessionStorage.getItem('projId'));
    console.log(projId);

    this.BugsService.getBugsByID(projId).subscribe(
      (bugs: Bugs[]) => {
        this.displayedBugs = bugs;
        this.filteredBugs = bugs;
        console.log(bugs);
        
        //this.applyFilters();
      },
      (error) => {
        console.error('Error fetching bugs', error);
      }
    );
  }

  createBug(): void {
    this.router.navigate(['/create']);
  }

  deleteSelectedBugs(): void {
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

  applySearchs(): void {
    this.filteredBugs = this.displayedBugs.filter((bug) => {
       const titleMatch = this.searchQuery
         ? bug.title.toLowerCase().includes(this.searchQuery.toLowerCase())
         : true;
   
       return titleMatch;
    });
   }

  applyFilters(): void {
    this.filteredBugs = this.displayedBugs.filter((bug) => {
      const statusMatch = this.filterStatus
        ? bug.status.toLowerCase() === this.filterStatus.toLowerCase()
        : true;
      const severityMatch = this.filterSeverity
        ? bug.severity.toLowerCase() === this.filterSeverity.toLowerCase()
        : true;
      const testerIdMatch = this.filterTesterName
        ? bug.testerName.toString() === this.filterTesterName
        : true;
      const developerIdMatch = this.filterDeveloperName
        ? bug.developerName.toString() === this.filterDeveloperName
        : true;
      const bugIdMatch = this.filterBugId
        ? bug.bugId.toString() === this.filterBugId
        : true;
      const titleMatch = this.searchQuery
        ? bug.title.toLowerCase().includes(this.searchQuery.toLowerCase())
        : true;

      return (
        statusMatch &&
        severityMatch &&
        testerIdMatch &&
        developerIdMatch &&
        bugIdMatch &&
        titleMatch
      );
    });
  }
}
