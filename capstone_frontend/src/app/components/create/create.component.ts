import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BugsService } from 'src/app/shared/services/bugs.service'; // Adjust the path as necessary
import { Bugs } from 'src/app/shared/models/Bugs'; // Adjust the path as necessary
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent implements OnInit {
  bugForm: FormGroup;
 base64Image: string = ''; // Variable to store the base64 image

 constructor(private bugService: BugsService) {}

 ngOnInit(): void {
    this.bugForm = new FormGroup({
      bugId: new FormControl(''),
      title: new FormControl(''),
      description: new FormControl(''),
      severity: new FormControl(''),
      status: new FormControl(''),
      expectedResult: new FormControl(''),
      actualResult: new FormControl(''),
      filePath: new FormControl(''), 
      testerName: new FormControl(''),
      developerName: new FormControl('')
    });
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

 _handleReaderLoaded(readerEvt) {
    this.base64Image = readerEvt.target.result;
    console.log(this.base64Image);
 }

 onSubmit() {
    if (this.bugForm.valid) {
      const newBug: Bugs = this.bugForm.value;
      newBug.filePath = this.base64Image; // Include the base64 image in the bug data
      this.bugService.createBug(newBug).subscribe(() => {
        console.log('Bug added successfully');
        // Optionally, refresh the list of bugs or navigate back
        this.ngOnInit();
      });
    }
 }
  
}
