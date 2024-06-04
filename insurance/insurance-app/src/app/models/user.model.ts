import { InsurancePolicy } from './insurance-policy.model';

export class User {

  id: number;
  name: string;
  email: string;
  insurancePolicies: InsurancePolicy[];

  constructor() {
    this.id = 0;
    this.name = '';
    this.email = '';
    this.insurancePolicies = [];
  }
}
