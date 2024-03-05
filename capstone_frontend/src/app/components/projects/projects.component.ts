import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { ProjectService } from 'src/app/shared/services/project.service';
import { Project } from 'src/app/shared/models/Project';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
})
export class ProjectsComponent implements OnInit {
  projectId: number;
  projectList: Project[];
  projectForm: FormGroup;
  project: Project;
  editForm: FormGroup;
  newProject: Project;
  editData: Project;

  constructor(
    private projectService: ProjectService,
    private form: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getAllProjects();
    this.projectForm = this.form.group({
      projectId: this.form.control(0),
      shortName: this.form.control(''),
      projectName: this.form.control(''),
      createdDate: this.form.control(''),
    });
    this.editForm = this.form.group({
      shortName: this.form.control(''),
      projectName: this.form.control(''),
    });
    this.valueChanges();
  }

  getAllProjects(): void {
    this.projectService.getAllProjects().subscribe((projects: Project[]) => {
      this.projectList = projects;
    });
  }

  valueChanges(): void {
    this.projectForm.valueChanges.subscribe((newData: Project) => {
      this.newProject = newData;
      console.log(this.newProject);
    });
  }

  onSave(): void {
    this.project = this.projectForm.value;
    this.project.createdDate = new Date().toString();
    this.projectService.createProject(this.project).subscribe({
      next: (res: { message: string }) => {
        alert(res.message);
        this.getAllProjects();
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
        console.log(this.newProject);
      },
      complete: () => {
        console.log('Complete!');
      },
    });
  }

  storeID(id: number): void{
    this.projectId = id;
  }

  editProject(event: FormGroup): void {
    this.editData = {
      projectId: this.projectId,
      shortName: event.value.shortName,
      projectName: event.value.projectName,
      createdDate: '',
    };

    this.projectService.updateProject(this.editData, this.projectId).subscribe({
      next: (res: Project) => {
        alert('Project updated!');
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
      },
      complete: () => {
        console.log('Complete!');
      },
    });
  }

  deleteProject(projectId: number): void {
    this.projectService.deleteProject(projectId).subscribe({
      next: () => {
        alert('Project deleted!');
        this.getAllProjects();
      },
      error: () => {
        console.error('error!');
      },
      complete: () => {
        console.log('Complete!');
      },
    });
  }
}
