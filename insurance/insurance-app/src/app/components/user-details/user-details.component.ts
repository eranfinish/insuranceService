import { Component, Input, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../models/user.model';
import { InsurancePolicy } from '../../models/insurance-policy.model';
import { UserService } from '../../services/user/user.service';
import { InsurancePolicyService } from '../../services/insurance-policy/insurance-policy.service';
import { InsurancePolicyFormComponent } from '../insurance-policy-form/insurance-policy-form.component';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  @Input() user: User = new User();
  @ViewChild(InsurancePolicyFormComponent) policyFormComponent: InsurancePolicyFormComponent | undefined;

  insurancePolicies: InsurancePolicy[] = [];
  showAddPolicyForm = false;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private insurancePolicyService: InsurancePolicyService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const userId = params['id'];
      this.getUserDetails(userId);
    });
  }

  getUserDetails(id: number): void {
    this.userService.getUserById(id).subscribe({
      next: (data: User) => {
        this.user = this.transformUserData(data);
        this.insurancePolicies = data.insurancePolicies;
      },
      error: (error) => {
        console.error('Error fetching user details:', error);
      }
    });
  }

  transformUserData(data: any): User {
    if (data.insurancePolicies && data.insurancePolicies.$values) {
      data.insurancePolicies = data.insurancePolicies.$values;
    }
    return data as User;
  }

  addPolicyToUser(newPolicy: InsurancePolicy): void {
    if (!this.user?.insurancePolicies) {
      this.user.insurancePolicies = [];
    }
    this.user.insurancePolicies.push(newPolicy);
    this.showAddPolicyForm = false;
  }

  editPolicy(policy: InsurancePolicy): void {
    this.showAddPolicyForm = true;
    setTimeout(() => {
      if (this.policyFormComponent) {
        this.policyFormComponent.editPolicy(policy);
      }
    }, 0); // Allow time for the form to be rendered
  }

  refreshData(): void {
    const userId = this.route.snapshot.params['id'];
    this.getUserDetails(userId);
  }
}
