import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InsurancePolicyService } from '../../services/insurance-policy/insurance-policy.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InsurancePolicy } from '../../models/insurance-policy.model';

@Component({
  selector: 'app-insurance-policy-form',
  templateUrl: './insurance-policy-form.component.html',
  styleUrls: ['./insurance-policy-form.component.css']
})
export class InsurancePolicyFormComponent implements OnInit {
  @Input() policy: InsurancePolicy | null = null;
  @Input() policyId: number | null = null;
  @Output() addPolicy: EventEmitter<InsurancePolicy> = new EventEmitter<InsurancePolicy>();

  policyForm: FormGroup = new FormGroup({});
  userId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private policyService: InsurancePolicyService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userId = Number(id);
    this.policyForm = this.fb.group({
      policyNumber: [this.policy?.policyNumber || '', Validators.required],
      insuranceAmount: [this.policy?.insuranceAmount || '', Validators.required],
      startDate: [this.policy?.startDate || '', Validators.required],
      endDate: [this.policy?.endDate || '', Validators.required],
      userId: [Number(id), Validators.required]
    });

    if (this.policyId) {
      this.policyService.getPolicyById(this.policyId).subscribe(policy => {
        this.policyForm.patchValue(policy);
      });
    }
  }

  onSubmit(): void {
    if (this.policyForm.valid) {
      const policyData: InsurancePolicy = this.policyForm.value;
      if (this.policyId) {
        this.policyService.updatePolicy({ ...policyData, id: this.policyId }).subscribe(() => {
          this.router.navigate(['/user-details', policyData.userId]);
        });
      } else {
        this.policyService.addPolicy(policyData).subscribe((newPolicy: InsurancePolicy) => {
          this.addPolicy.emit(newPolicy);
          this.router.navigate(['/user-details', policyData.userId]);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/user-details', this.policyForm.value.userId]);
  }

  editPolicy(policy: InsurancePolicy): void {
    this.policy = policy;
    this.policyId = policy.id;
    this.policyForm.patchValue(policy);
  }
}
