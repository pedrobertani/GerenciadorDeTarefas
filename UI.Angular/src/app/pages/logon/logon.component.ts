import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-logon',
  templateUrl: './logon.component.html',
  styleUrls: ['./logon.component.css'],
})
export class LogonComponent {
  protected loginModel: LoginModel = { userName: '', password: '' };
  protected errorMessage: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar, // Injetar o MatSnackBar
  ) {}

  protected onLogin() {
    this.authService.login(this.loginModel).subscribe({
      next: (response) => {
        debugger;
        // Armazenar o token e o nome de usuário no localStorage
        localStorage.setItem('token', response.token);
        localStorage.setItem('userName', response.userName); // Armazenar o nome de usuário

        // Exibe o SnackBar de sucesso
        this.snackBar.open('Login realizado com sucesso!', 'Fechar', {
          duration: 3000, 
          panelClass: ['snackbar-success'], 
        });

        this.router.navigate(['/tarefas']);
      },
      error: (error) => {
        this.errorMessage = 'Login falhou. Verifique suas credenciais.';

        // Exibe o SnackBar de erro
        this.snackBar.open(this.errorMessage, 'Fechar', {
          duration: 3000, 
          panelClass: ['snackbar-error'], 
        });
      },
    });
  }

  protected navigateToRegister() {
    this.router.navigate(['/cadastro']);
  }
}
