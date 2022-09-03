import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Contracts } from 'src/api/ContractApi';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employees: Contracts.Employee[] = [];
  selectedEmployee!: Contracts.Employee;
  constructor(
    private client: Contracts.Client) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.client.employeesAll().subscribe((result) => (this.employees = result));
  }

  getById(id: string) {
    this.client
      .idGET(id)
      .subscribe((result) => (this.selectedEmployee = result));
  }

  create(id: string) {
    let emp: Contracts.IEmployee = {
      name: '',
      joinDate: new Date(),
      salary: 1000,
    };

    this.client
      .employees(Contracts.Employee.fromJS(emp))
      .subscribe((result) => {
        alert('Created Successfully.');
        console.log(result);
      });
  }

  update(id: string) {
    let emp: Contracts.IEmployee = {
      id: id,
      name: '',
      joinDate: new Date(),
      salary: 1000,
    };

    this.client
      .idPUT(id, Contracts.Employee.fromJS(emp))
      .subscribe((result) => {
        alert('Created Successfully.');
        console.log(result);
      });
  }

  deleteById(id?: string) {
    this.client
      .idDELETE(id)
      .pipe(finalize(()=> this.getAll()))
      .subscribe((result) => alert('Deleted Successfully.'));
  }
}
