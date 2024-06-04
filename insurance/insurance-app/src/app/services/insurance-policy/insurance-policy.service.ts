// src/app/services/insurance-policy.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InsurancePolicy } from '../../models/insurance-policy.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InsurancePolicyService {
  private apiUrl = environment.apiUrl +'/api/insurancePolicies';

  constructor(private http: HttpClient) {}

  getPolicies(): Observable<InsurancePolicy[]> {
    return this.http.get<InsurancePolicy[]>(this.apiUrl);
  }

  getPolicyById(id: number): Observable<InsurancePolicy> {
    return this.http.get<InsurancePolicy>(`${this.apiUrl}/${id}`);
  }

  addPolicy(policy: InsurancePolicy): Observable<InsurancePolicy> {
    return this.http.post<InsurancePolicy>(this.apiUrl, policy);
  }

  updatePolicy(policy: InsurancePolicy): Observable<InsurancePolicy> {
    return this.http.put<InsurancePolicy>(`${this.apiUrl}/${policy.id}`, policy);
  }

  deletePolicy(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
