import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/User';


import { UsersService } from 'src/app/shared/services/users.service';
import { Validators, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit{
  displayedUsers: User[] = [];
  filteredUsers: User[] = [];
  searchQuery: string = '';
  selectedUserId: number | null = null;

  userForm: FormGroup;

  constructor(private usersService: UsersService, private router: Router) {}

  ngOnInit(): void {
    this.fetchUsers();
    this.initForm();
  }

  initForm(): void {
    this.userForm = new FormGroup({
      'searchQuery': new FormControl(this.searchQuery, Validators.required),
      'selectedUserId': new FormControl(this.selectedUserId, Validators.required)
    });
 }

  fetchUsers(): void {
    // Assuming you have a method to fetch users from your service
    this.usersService.getUsers().subscribe(
      (users: User[]) => {
        this.displayedUsers = users;
        this.filteredUsers = users;
        console.log(users);
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }

  deleteUser(userId:number): void {
      this.usersService.deleteUser(userId).subscribe({
        next: () => {
          alert('Project deleted!');
          window.location.reload();
        },
        error: () => {
          console.error('error!');
        },
        complete: () => {
          console.log('Complete!');
        },

      }
        
      );
    }
  }

