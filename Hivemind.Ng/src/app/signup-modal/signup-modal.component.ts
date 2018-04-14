import { UsersClient } from './../../autogenerated/clients/UsersClient';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Login } from 'autogenerated/entities/Login';
import { LoginClient } from '../clients/LoginClient';

@Component({
  selector: 'signup-modal',
  templateUrl: './signup-modal.component.html',
  styleUrls: ['./signup-modal.component.css']
})
export class SignupModalComponent {
  
  public showSignUpDialog: boolean;
  public signupForm: FormGroup;
  public signupError: string;
  public showSignupError: boolean;

  constructor(
    private _formBuilder: FormBuilder,
    private _userClient: UsersClient,
    private _loginClient: LoginClient
  ) {
    this.signupForm = _formBuilder.group({
      'email': ['', Validators.required],
      'password': ['', Validators.required],
      'confirmPassword': ['', Validators.required]
    });
  }

  public display() {
    this.showSignUpDialog = true;
  }

  public submitSignupForm() {
    const email = this.signupForm.get('email').value;
    const password = this.signupForm.get('password').value;
    const confirmPassword = this.signupForm.get('confirmPassword').value;

    if (password != confirmPassword) {
      this.signupError = 'Passwords don\'t match.';
      this.showSignupError = true;
      return;
    }

    const login = new Login({
      email: email,
      password: password
    });

    this._userClient.Register(login).subscribe(user => {
      this._loginClient.Login(email, password).subscribe(res => {
        if (!res || !res.success) {
          this.signupError = res.message;
          this.showSignupError = true;
        }

        this.showSignUpDialog = false;
      });
    },
    error => {
      this.signupError = 'Error signing up.';
      this.showSignupError = true;
    });
  }
}
