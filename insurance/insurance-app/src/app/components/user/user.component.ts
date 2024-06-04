// src/app/components/user/user.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  userForm: FormGroup;
  userId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.userId) {
      this.userService.getUserById(this.userId).subscribe(user => {
        this.userForm.patchValue(user);
      });
    }
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      const userData: User = this.userForm.value;
      if (this.userId) {
        this.userService.updateUser({ ...userData, id: this.userId }).subscribe(() => {
          this.router.navigate(['/']);
        });
      } else {
        this.userService.addUser(userData).subscribe(() => {
          this.router.navigate(['/']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/']);
  }
}
