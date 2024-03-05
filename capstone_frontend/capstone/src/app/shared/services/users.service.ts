import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';
@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private apiUrl = 'hhttps://localhost:7128/api/User'; // Replace with your API url

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`https://localhost:7128/api/User`);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>('https://localhost:7128/api/User', user, { headers: { 'Content-Type' : 'application/json' } });
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/users/${user.userId}`, user);
  }

  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`https://localhost:7128/api/User/${userId}`);
  }
  
  loginUser(loginObj: User): Observable<object> {
    return this.http.post<object>('https://localhost:7128/login', loginObj);
  }
}
