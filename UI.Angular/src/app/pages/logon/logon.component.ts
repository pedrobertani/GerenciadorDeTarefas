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
  protected loginModel: LoginModel = { userName: '', password: '' };
  protected errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  protected onLogin() {
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

  protected navigateToRegister() {
    this.router.navigate(['/cadastro']);
  }
}
