

export class InsurancePolicy {
  id: number;
  policyNumber: string ;
  insuranceAmount: number;
  startDate: Date;
  endDate: Date;
  userId: number|undefined;

  constructor() {
    this.id = 0;
    this.policyNumber = '';
    this.insuranceAmount = 0;
    this.startDate = new Date();
    this.endDate = new Date();
    this.userId = 0;
  }
}
