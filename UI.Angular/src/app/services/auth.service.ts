import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from '../models/login.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7222/api/auth';

  constructor(private http: HttpClient) {}

  login(loginModel: LoginModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginModel);
  }

  // Método para armazenar token e userId no localStorage
  storeAuthData(token: string, userId: string): void {
    localStorage.setItem('token', token);
    localStorage.setItem('userId', userId);
  }

  // Recupera o token do localStorage
  private getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Recupera o userId do localStorage
  getUserId(): string | null {
    return localStorage.getItem('userId');
  }

  // Método para obter dados protegidos
  getProtectedData(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getToken()}`,
    });

    return this.http.get<any>(`${this.apiUrl}/protected`, { headers });
  }

  // Método para fazer logout
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
  }
}
