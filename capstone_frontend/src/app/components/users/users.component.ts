import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {
  userList: User[];
  userForm=FormGroup;

  userObj: any = {
    "userId": 0,
    "emailId": "string",
    "fullName": "string",
    "password": "string"
  }

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers() {
    this.http.get("https://freeapi.miniprojectideas.com/api/Jira/GetAllUsers").subscribe((res: any)=>{
      this.userList = res.data;
    })
  }
  onSave() {
    this.http.post("https://freeapi.miniprojectideas.com/api/Jira/CreateUser",this.userObj).subscribe((res: any)=>{
       if(res.result) {
        alert(res.message);
        this.getAllUsers();
       } else {
        alert(res.message)
       }
    })
  }

}
