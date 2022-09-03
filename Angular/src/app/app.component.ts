import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Contracts } from '../api/ContractApi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [HttpClient]
})
export class AppComponent implements OnInit {
  employees: Contracts.Employee[] = [];
  title = 'Angular';
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

  deleteById(id: string) {
    this.client
      .idDELETE(id)
      .subscribe((result) => alert('Deleted Successfully.'));
  }
}
