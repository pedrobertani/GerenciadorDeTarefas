import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LogonComponent } from './pages/logon/logon.component';
import { TasksComponent } from './pages/tasks/tasks.component';

const routes: Routes = [
  { path: 'cadastro', component: RegisterComponent },
  { path: '', component: LogonComponent },
  { path: 'tarefas', component: TasksComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
