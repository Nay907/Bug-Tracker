import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BugsComponent } from './bugs/bugs.component';
import { UsersComponent } from './users/users.component';



@NgModule({
  declarations: [
    BugsComponent,
    UsersComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
