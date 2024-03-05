import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';
@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private apiUrl = 'https://api.example.com'; // Replace with your API url

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/users`);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>('https://localhost:7128/api/User', user, { headers: { 'Content-Type' : 'application/json' } });
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/users/${user.userId}`, user);
  }

  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/users/${userId}`);
  }
  
  loginUser(loginObj: User): Observable<object> {
    return this.http.post<object>('https://localhost:7128/login', loginObj);
  }
}
