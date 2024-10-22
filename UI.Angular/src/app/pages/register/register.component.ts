import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  protected user: User = { userName: '', password: '', confirmPassword: '' }; 
  protected errorMessage: string = '';
  protected successMessage: string = '';
  protected formSubmitted: boolean = false; 

  constructor(private userService: UserService, private router: Router) {}

  protected onRegister() {
    this.formSubmitted = true; 

    // Verifica se os campos estão preenchidos
    if (!this.user.userName || !this.user.password || !this.user.confirmPassword) {
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios.';
      this.successMessage = '';
      return; // Interrompe a execução se algum campo estiver vazio
    }

    // Verifica se as senhas coincidem
    if (this.user.password !== this.user.confirmPassword) {
      this.errorMessage = 'As senhas não coincidem.';
      this.successMessage = '';
      return; // Interrompe a execução se as senhas não coincidirem
    }

    // Chama o serviço de registro
    this.userService.register(this.user).subscribe({
      next: (response) => {
        this.successMessage = 'Usuário registrado com sucesso!';
        this.errorMessage = '';
        this.router.navigate(['/']); // Navega para a página inicial após o registro
      },
      error: (error) => {
        this.errorMessage = 'Falha ao registrar o usuário.';
        this.successMessage = '';
      }
    });
  }

  protected onCancel() {
    this.user = { userName: '', password: '', confirmPassword: '' }; // Limpa os campos
    this.successMessage = '';
    this.errorMessage = '';
    this.router.navigate(['/']);
  }
}
