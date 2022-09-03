import { EmployeesComponent } from './employees/employees.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEmployeeComponent } from './add-employee/add-employee.component';

const routes: Routes = [
  {path:'', component: EmployeesComponent},
  {path:'employee', component: AddEmployeeComponent},
  {path:'employee/:id', component: AddEmployeeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
