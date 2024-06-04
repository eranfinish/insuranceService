// src/app/components/user-list/user-list.component.ts
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { User } from '../../models/user.model';
import { Router } from '@angular/router';
import { InsurancePolicy } from '../../models/insurance-policy.model';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService, private router: Router) {

  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {

    this.userService.getUsers().subscribe(
      (response: any) => {
        this.users = response.$values.map((user: any) => {
          const newUser = new User();
          newUser.id = user.id;
          newUser.name = user.name;
          newUser.email = user.email;
          newUser.insurancePolicies = user.insurancePolicies.$values.map((policy: any) => {
            const newPolicy = new InsurancePolicy();
            newPolicy.id = policy.id;
            newPolicy.policyNumber = policy.policyNumber;
            newPolicy.insuranceAmount = policy.insuranceAmount;
            newPolicy.startDate = new Date(policy.startDate);
            newPolicy.endDate = new Date(policy.endDate);
            newPolicy.userId = policy.userID;
            return newPolicy;
          });
          return newUser;
        });
      },
      (error) => {
        console.error('Error fetching users', error);
      }
  );
}

  addUser(): void {
    this.router.navigate(['/user']);
  }

  editUser(id: number): void {
    this.router.navigate(['/user', id]);
  }

  deleteUser(id: number): void {
    this.userService.deleteUser(id).subscribe(() => this.loadUsers());
  }

  viewUserDetails(id: number): void {
    this.router.navigate(['/user-details', id]);
  }

}
