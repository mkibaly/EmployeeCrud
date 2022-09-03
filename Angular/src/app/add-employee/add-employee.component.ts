import { formatDate } from '@angular/common';
import { Component, OnInit, enableProdMode } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Contracts } from 'src/api/ContractApi';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent implements OnInit {
  selectedEmployee!: Contracts.Employee;
  employeeId: string | null | undefined;
  employeeForm!: FormGroup;
  constructor(
    private client: Contracts.Client,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.initForm();

    const routeParams = this.route.snapshot.paramMap;
    this.employeeId = routeParams.get('id');

    if (this.employeeId) {
      this.getById(this.employeeId);
    }
  }

  initForm() {
    this.employeeForm = this.fb.group({
      id: [undefined],
      name: [undefined, Validators.required],
      joinDate: [undefined, Validators.required],
      salary: [undefined, Validators.required],
    });
  }

  getById(id: string) {
    this.client.idGET(id).subscribe((result) => {
      this.selectedEmployee = result;
      this.employeeForm?.patchValue(result);
      this.employeeForm
        ?.get('joinDate')
        ?.setValue(
          formatDate(result.joinDate ?? new Date(), 'yyyy-MM-dd', 'en')
        );
    });
  }

  createOrUpdate() {
    if(this.employeeForm.invalid){
      alert("Invalid values!, " + this.employeeForm.valid);
    }

    if (this.employeeId) this.update(this.employeeId);
    else this.create();
  }

  create() {
    this.client
      .employees(Contracts.Employee.fromJS(this.employeeForm.value))
      .subscribe((result) => {
        alert('Created Successfully.');
        this.router.navigate(['/']);
      });
  }

  update(id: string) {
    this.client
      .idPUT(id, Contracts.Employee.fromJS(this.employeeForm.value))
      .subscribe((result) => {
        alert('Updated Successfully.');
        this.router.navigate(['/']);
      });
  }
}
