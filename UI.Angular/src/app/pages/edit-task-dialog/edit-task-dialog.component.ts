import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-task-dialog',
  templateUrl: './edit-task-dialog.component.html',
  styleUrls: ['./edit-task-dialog.component.css'],
})
export class EditTaskDialogComponent {
  editTaskForm: FormGroup;
  isEditMode: boolean;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<EditTaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { task: any; isEditMode: boolean },
  ) {
    this.isEditMode = data.isEditMode;
    this.editTaskForm = this.fb.group({
      title: [data.task?.title || ''],
      description: [data.task?.description || ''],
      completed: [data.task?.completed || false],
      taskId: [data.task?.taskId || null],
      userId: [data.task?.userId || null],
    });
  }

  onSave(): void {
    if (this.editTaskForm.valid) {
      this.dialogRef.close(this.editTaskForm.value);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
