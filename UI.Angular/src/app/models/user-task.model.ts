export interface UserTask {
  taskId: number;
  title: string;
  description: string;
  completed: boolean;
  dateCreated: Date;
  userId: number; // Opcional, dependendo de como você quer estruturar
}
