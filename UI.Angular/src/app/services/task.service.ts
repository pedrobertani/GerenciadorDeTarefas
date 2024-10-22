import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserTask } from '../models/user-task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = 'https://localhost:7222/api/Task';

  constructor(private http: HttpClient) {}

  // Obter lista de tarefas
  getTasks(pageNumber: number, pageSize: number): Observable<any> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get(this.apiUrl, { params });
  }

  // Adicionar nova tarefa
  addTask(task: UserTask): Observable<UserTask> {
    return this.http.post<UserTask>(`${this.apiUrl}`, task);
  }

  // Atualizar tarefa existente
  updateTask(task: UserTask): Observable<UserTask> {
    debugger;
    return this.http.put<UserTask>(`${this.apiUrl}`, task);
  }

  // Serviço TaskService
  completeTask(taskId: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${taskId}/complete`, null);
  }

  // Remover tarefa
  deleteTask(taskId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${taskId}`);
  }
}
