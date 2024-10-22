import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('token');
      
        if (token) {
          const clonedRequest = req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          });
          return next.handle(clonedRequest).pipe(
            catchError(err => {
              // Aqui você pode lidar com erros globais
              console.error('Erro na requisição:', err);
              return throwError(err); // Re-throw para que outros manipuladores de erro possam lidar com ele
            })
          );
        }
      
        return next.handle(req);
      }
}