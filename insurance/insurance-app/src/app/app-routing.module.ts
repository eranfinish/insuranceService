// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { InsurancePolicyFormComponent } from './components/insurance-policy-form/insurance-policy-form.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  { path: '', component: UserListComponent },
  { path: 'user/:id', component: UserComponent },
  { path: 'user', component: UserComponent },
  { path: 'user-details/:id', component: UserDetailsComponent },
  { path: 'policy-form/:id', component: InsurancePolicyFormComponent },
  { path: 'policy-form', component: InsurancePolicyFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
