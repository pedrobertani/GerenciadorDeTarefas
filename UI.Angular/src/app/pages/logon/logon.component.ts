import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logon',
  templateUrl: './logon.component.html',
  styleUrls: ['./logon.component.css']
})
export class LogonComponent {
  loginModel: LoginModel = { userName: '', password: '' };
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.authService.login(this.loginModel).subscribe({
      next: (response) => {
        // Armazenar o token e o userId no localStorage
        localStorage.setItem('token', response.token);

        this.router.navigate(['/tarefas']);
      },
      error: (error) => {
        this.errorMessage = 'Login falhou. Verifique suas credenciais.';
      }
    });
  }

  navigateToRegister() {
    this.router.navigate(['/cadastro']);
  }
}
