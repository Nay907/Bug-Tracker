import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/Project';
@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  private apiUrl = 'https://localhost:7128/api/Project';

  constructor(private http: HttpClient) {}

  getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>('https://localhost:7128/api/Project');
  }

  createProject(projectData: Project): Observable<object> {
    console.log(projectData);

    return this.http.post<object>(
      'https://localhost:7128/addProject',
      projectData,
      { headers: { 'Content-Type': 'application/json' } }
    );
  }

  deleteProject(projectId: number): Observable<any> {
    return this.http.delete(`https://localhost:7128/api/Project/${projectId}`);
  }

  updateProject(project: Project, projectId: number): Observable<Project> {
    return this.http.put<Project>(
      `https://localhost:7128/api/Project/${projectId}`,
      project,
      { headers: { 'Content-Type': 'application/json' } }
    );
  }
}
