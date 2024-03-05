import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bugs } from '../models/Bugs';

@Injectable({
  providedIn: 'root'
})
export class BugsService {
  private apiUrl = 'https://localhost:7128/api'; 

  constructor(private http: HttpClient) { }

  getBugs(): Observable<Bugs[]> {
    return this.http.get<Bugs[]>(`https://localhost:7128/api/Bugs`);
  }

  getBugsByID(projId: number): Observable<Bugs[]> {
    return this.http.get<Bugs[]>(`https://localhost:7128/api/Bugs/${projId}`);
  }

  createBug(bug: Bugs): Observable<object> {
    return this.http.post<object>(`https://localhost:7128/api/Bugs`, bug, { headers: { 'Content-Type': 'application/json' } });
  }

  updateBug(bug: Bugs): Observable<Bugs> {
    return this.http.put<Bugs>(`${this.apiUrl}/bugs/${bug.bugId}`, bug);
  }

  deleteBug(bugId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/bugs/${bugId}`);
  }

  getTotalBugCount() {
    return this.http.get<any>(`https://localhost:7128/api/Bugs/total-bug-count`);
  }

  getBugCountByLowSeverity() {
    return this.http.get<any>(`https://localhost:7128/api/Bugs/severity/low`);
  }

  getBugCountByMediumSeverity() {
    return this.http.get<any>(`https://localhost:7128/api/Bugs/severity/medium`);
  }

  getBugCountByHighSeverity() {
    return this.http.get<any>(`https://localhost:7128/api/Bugs/severity/high`);
  }
}
