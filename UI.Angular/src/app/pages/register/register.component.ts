import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  protected user: User = { userName: '', password: '', confirmPassword: '' };
  protected errorMessage: string = ''; // Para mostrar mensagem de erro na interface
  protected successMessage: string = ''; // Para mostrar mensagem de sucesso na interface
  protected formSubmitted: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {}

  protected onRegister() {
    this.formSubmitted = true; // Indica que o formulário foi submetido

    // Verifica se os campos estão preenchidos
    if (
      !this.user.userName ||
      !this.user.password ||
      !this.user.confirmPassword
    ) {
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios.';
      this.successMessage = '';

      // Exibe o SnackBar de erro
      this.snackBar.open(this.errorMessage, 'Fechar', {
        duration: 3000,
        panelClass: ['snackbar-error'],
      });

      this.formSubmitted = false; // Permite que o formulário seja submetido novamente
      return; // Interrompe a execução se algum campo estiver vazio
    }

    // Verifica se as senhas coincidem
    if (this.user.password !== this.user.confirmPassword) {
      this.errorMessage = 'As senhas não coincidem.';
      this.successMessage = '';

      // Exibe o SnackBar de erro
      this.snackBar.open(this.errorMessage, 'Fechar', {
        duration: 3000,
        panelClass: ['snackbar-error'],
      });

      this.formSubmitted = false; // Permite nova submissão após erro
      return; // Interrompe a execução se as senhas não coincidirem
    }

    // Verifica se a senha tem pelo menos 6 caracteres
    if (this.user.password.length <= 5) {
      this.errorMessage = 'A senha deve conter no mínimo 6 caracteres.';
      this.successMessage = '';

      // Exibe o SnackBar de erro
      this.snackBar.open(this.errorMessage, 'Fechar', {
        duration: 3000,
        panelClass: ['snackbar-error'],
      });

      this.formSubmitted = false;
      return;
    }

    // Chama o serviço de registro
    this.userService.register(this.user).subscribe({
      next: (response) => {
        this.successMessage = 'Usuário registrado com sucesso!';
        this.errorMessage = '';

        this.snackBar.open(this.successMessage, 'Fechar', {
          duration: 3000,
          panelClass: ['snackbar-success'],
        });

        // Redireciona para a página inicial após sucesso
        this.router.navigate(['/']);
      },
      error: (error) => {
        debugger;
        const errorMessage = error.error.split('\r\n')[0];

        if (errorMessage.includes('Usuário já cadastrado')) {
          this.errorMessage = 'Usuário já cadastrado';
        } else {
          this.errorMessage = 'Erro desconhecido.';
        }

        this.successMessage = '';

        this.snackBar.open(this.errorMessage, 'Fechar', {
          duration: 3000,
          panelClass: ['snackbar-error'],
        });

        this.formSubmitted = false; // Permite nova submissão após erro
      },
    });
  }

  protected onCancel() {
    this.user = { userName: '', password: '', confirmPassword: '' }; // Limpa os campos
    this.successMessage = '';
    this.errorMessage = '';

    // Exibe um SnackBar informando o cancelamento
    this.snackBar.open('Operação cancelada.', 'Fechar', {
      duration: 3000,
      panelClass: ['snackbar-info'],
    });

    // Redireciona para a página inicial
    this.router.navigate(['/']);
  }
}
