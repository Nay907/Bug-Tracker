import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { BugsService } from 'src/app/shared/services/bugs.service'; // Adjust the path as necessary
import { Bugs } from 'src/app/shared/models/Bugs'; // Adjust the path as necessary
import { Router, Event } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent implements OnInit {
  bugForm: FormGroup;
  base64Image: string = ''; // Variable to store the base64 image
  bugId: number;
  bugList: Bugs[];
  editForm: FormGroup;
  newBug: Bugs;
  inputData: Bugs;

  constructor(
    private bugService: BugsService,
    private form: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bugForm = this.form.group({
      bugId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      severity: ['', Validators.required],
      stepsToReproduce: ['', Validators.required],
      status: ['', Validators.required],
      expectedResult: ['', Validators.required],
      actualResult: ['', Validators.required],
      filePath: ['', Validators.required],
      testerName: ['', Validators.required],
      developerName: ['', Validators.required],
      createdAt: ['', Validators.required],
      updatedAt: ['', Validators.required],
      completedAt: ['', Validators.required]
    });
    this.valueChanges();
  }

  handleFileSelect(evt) {
    var files = evt.target.files;
    var file = files[0];
    if (files && file) {
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsDataURL(file);
    }
  }

  valueChanges(): void {
    this.bugForm.valueChanges.subscribe((newData: Bugs) => {
      this.newBug = newData;
    });
  }

  _handleReaderLoaded(readerEvt) {
    this.base64Image = readerEvt.target.result;
    console.log(this.base64Image);
  }

  onSubmit(event: FormGroup): void {
    let projId = parseInt(sessionStorage.getItem('projId'));
    console.log(projId);
    
    this.inputData = {
      projectId: projId,
      bugId: 0,
      title: event.value.title,
      description: event.value.description,
      severity: event.value.severity,
      stepsToReproduce: '',
      status: event.value.status,
      expectedResult: event.value.expectedResult,
      actualResult: event.value.actualResult,
      filePath: event.value.filePath,
      testerName: event.value.testerName,
      developerName: event.value.developerName,
      createdAt: '',
      updatedAt: '',
      completedAt: ''
    }

    this.inputData.createdAt = new Date().toString();

    this.bugService.createBug(this.inputData).subscribe({
      next: (res: { message: string }) => {
        alert(res.message);
        this.router.navigateByUrl(`/dashboard/${projId}`);
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
        console.log(this.newBug);
      },
      complete: () => {
        console.log('Complete!');
      },
    });
  }
}
