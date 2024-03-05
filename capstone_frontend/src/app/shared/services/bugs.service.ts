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

  createBug(bug: Bugs): Observable<Bugs> {
    return this.http.post<Bugs>(`${this.apiUrl}/bugs`, bug);
  }

  updateBug(bug: Bugs): Observable<Bugs> {
    return this.http.put<Bugs>(`${this.apiUrl}/bugs/${bug.bugId}`, bug);
  }

  deleteBug(bugId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/bugs/${bugId}`);
  }
}
