import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];


  constructor(
    private fb: FormBuilder,
    private accountSerivce: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null,[Validators.required]],
      email: [null,[
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      password: [null,[Validators.required]]
    })
  }

  onSubmit() {
    console.log(this.registerForm.value);
    this.accountSerivce.register(this.registerForm.value)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/shop')
        },
        error: err => {
          console.log(err);
          this.errors = err.errors;
        }
      }) 
  }

  validateEmailNotTaken() : AsyncValidatorFn {
    return control => {
      // debaunece
      return timer(500).pipe(
        switchMap(() => {
          if(!control.value) {
            return of(null);
          }
          return this.accountSerivce.checkEmailExists(control.value)
          .pipe(
            map(res => {
              return res ? {emailExists: true} : null;
            })
          )
        })
      )
    }
  }

}
