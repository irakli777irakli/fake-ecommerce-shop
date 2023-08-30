import { Component, OnInit, ViewChild, ElementRef, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input', { static: true }) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;

  constructor(@Self() public controlDir: NgControl) {
    /*
  The constructor of the component takes
   an argument annotated with @Self() public controlDir: NgControl.
  NgControl is part of Angular's forms
  module and is used to manage form control directives.

  Inside the constructor,
   it sets the valueAccessor property of the controlDir to this.
  This establishes a connection between
   this custom input component and the form control it's associated with.

    */
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    /*
In the ngOnInit method,
 the code retrieves the control associated with this component using this.controlDir.control.

It checks if the control has
 validators (synchronous and asynchronous) and creates arrays of these validators.

Then, it sets these validators
 on the control using control.setValidators(validators) and control.setAsyncValidators(asyncValidators).
  This is essential for form validation.

Finally, it calls control.updateValueAndValidity(), 
which triggers the validation of the control.
    */
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  /*
onChange and onTouched Methods:

These are placeholder methods
 for the registerOnChange and registerOnTouched functions.
 They can be overridden with
  custom behavior when the form control's value changes or when it's touched.
  */

  onChange(event) { }

  onTouched() { }


  /*
ControlValueAccessor Methods:

This component implements
 the ControlValueAccessor interface, which is used
  to interact with Angular forms. 
  It provides methods
   for reading and writing values to and from the form control.

The writeValue(obj: any) method is
 used to write a value from the model to the view.
  In this case,
   it sets the value of the <input> element's value property
    based on the provided obj or an empty string if obj is falsy.

The registerOnChange(fn: any) method is used to
 register a callback function that
  should be called when the value of the input changes.
   It sets the component's onChange method to the provided function.

The registerOnTouched(fn: any) method is used to register
 a callback function that
  should be called when the input is touched. It sets the component's 
  onTouched method to the provided function.
  */
  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

}