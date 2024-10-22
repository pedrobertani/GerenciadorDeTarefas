import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-protected',
  templateUrl: './protected.component.html',
  styleUrls: ['./protected.component.css'],
})
export class ProtectedComponent implements OnInit {
  protectedData: any;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.getProtectedData().subscribe({
      next: (data) => {
        this.protectedData = data;
      },
      error: (error) => {
        console.error('Erro ao acessar dados protegidos', error);
      },
    });
  }
}
