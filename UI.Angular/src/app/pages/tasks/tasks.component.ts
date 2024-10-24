import { AuthService } from './../../services/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { UserTask } from '../../models/user-task.model';
import { MatDialog } from '@angular/material/dialog';
import { EditTaskDialogComponent } from '../edit-task-dialog/edit-task-dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { Route, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  protected tasks: UserTask[] = [];
  protected errorMessage: string = '';
  protected displayedColumns: string[] = [
    'title',
    'description',
    'completed',
    'actions',
  ];
  protected userName: string = '';

  // Variáveis para paginação
  protected currentPage: number = 0;
  protected pageSize: number = 10;
  protected totalTasks: number = 0;

  constructor(
    public dialog: MatDialog,
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.userName = localStorage.getItem('userName') || 'Usuário';

    this.loadTasks();
  }

  ngOnInit(): void {
    this.loadTasks();
  }

  // Abrir modal para editar tarefa existente
  protected editTask(task: UserTask): void {
    const dialogRef = this.dialog.open(EditTaskDialogComponent, {
      width: '400px',
      height: 'auto',
      data: {
        task,
        isEditMode: true,
      },
      position: { left: '50vw' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.taskService.updateTask(result).subscribe(
          (updatedTask) => {
            const index = this.tasks.findIndex(
              (t) => t.taskId === updatedTask.taskId,
            );
            if (index > -1) {
              this.tasks[index] = updatedTask;
            }

            // Show success toast
            this.snackBar.open('Tarefa atualizada com sucesso!', 'Fechar', {
              duration: 3000,
            });
          },
          (error) => {
            console.error('Erro ao atualizar tarefa', error);
            this.errorMessage = 'Erro ao atualizar tarefa.';
            this.snackBar.open('Erro ao atualizar tarefa.', 'Fechar', {
              duration: 3000,
            });
          },
        );
      }
      setTimeout(() => this.loadTasks(), 100);
    });
  }

  // Abrir modal para adicionar nova tarefa
  protected addTask(): void {
    const dialogRef = this.dialog.open(EditTaskDialogComponent, {
      width: '400px',
      height: 'auto',
      data: {
        task: null,
        isEditMode: false,
      },
      position: { left: '50vw' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const userTask: UserTask = {
          taskId: 0,
          title: result.title,
          description: result.description,
          completed: result.completed,
          dateCreated: new Date(),
          userId: 0,
        };

        this.taskService.addTask(userTask).subscribe(
          (newTask) => {
            this.tasks.push(newTask);
            this.loadTasks(); // Update task list if necessary

            // Show success toast
            this.snackBar.open('Tarefa adicionada com sucesso!', 'Fechar', {
              duration: 3000, // Duration in milliseconds
            });
          },
          (error) => {
            console.error('Erro ao adicionar tarefa', error);
            this.errorMessage =
              error.error.errors.tarefaDto?.[0] || 'Erro desconhecido';
            this.snackBar.open('Erro ao adicionar tarefa.', 'Fechar', {
              duration: 3000,
            });
          },
        );
      }
    });
  }

  protected loadTasks() {
    this.taskService.getTasks(this.currentPage + 1, this.pageSize).subscribe({
      next: (response) => {
        this.tasks = response.tasks; // Armazena as tarefas
        this.totalTasks = response.totalCount; // Armazena o total de tarefas
      },
      error: (error) => {
        console.error('Erro ao carregar as tarefas', error);
        this.errorMessage = 'Erro ao carregar as tarefas.';
      },
    });
  }

  // Quando a página muda, chamamos loadTasks para carregar novas tarefas
  protected pageChanged(event: any) {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadTasks();
  }

  protected markAsCompleted(taskId: number): void {
    debugger
    this.taskService.completeTask(taskId).subscribe(
      (success) => {
        if (success) {
        } else {
          console.error('Tarefa não encontrada ou não pode ser concluída.');
        }
      },
      (error) => {
        console.error('Erro ao marcar a tarefa como concluída', error);
        this.errorMessage =
          'Erro ao marcar a tarefa como concluída. Tente novamente.';
      },
    );
    setTimeout(() => this.loadTasks(), 100);
  }

  protected deleteTask(taskId: number): void {
    this.taskService.deleteTask(taskId).subscribe(
      (success) => {
        if (success) {
          console.log(`Tarefa com ID ${taskId} removida com sucesso.`);
        }
      },
      (error) => {
        console.error('Erro ao remover a tarefa', error);
        this.errorMessage = 'Erro ao remover a tarefa. Tente novamente.';
      },
    );
    setTimeout(() => this.loadTasks(), 1000);
  }

  protected logoff() {
    this.authService.logout();
    this.navigateToLogon();
  }

  protected navigateToLogon() {
    this.router.navigate(['/']);
  }
}
