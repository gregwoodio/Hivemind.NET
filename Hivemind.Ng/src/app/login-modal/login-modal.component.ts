import { LoginClient } from './../clients/LoginClient';
import { LoginResponse } from './../clients/LoginResponse';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent {

  public showLoginDialog: boolean;
  public showLoginError: boolean;
  public isLoginLoading: boolean;
  public loginForm: FormGroup;
  public loginError: string;

  constructor(
    private _formBuilder: FormBuilder,
    private _loginClient: LoginClient
  ) {
    this.loginForm = _formBuilder.group({
      'email': ['', Validators.required],
      'password': ['', Validators.required]
    });
  }

  public display() {
    this.showLoginDialog = true;
  }

  public submitLoginForm() {
    const email = this.loginForm.get('email').value;
    const password = this.loginForm.get('password').value;

    this.isLoginLoading = true;
    this.loginError = '';

    this._loginClient.Login(email, password).subscribe((res: LoginResponse) => {
      this.isLoginLoading = false;
      if (!res || !res.success) {
        this.loginError = res.message;
        this.showLoginError = true;
        return;
      }

      // clear input fields
      this.loginError = '';
      this.loginForm.get('email').setValue('');
      this.loginForm.get('password').setValue('');

      // hide login button
      this.showLoginDialog = false;
    });
  }
}
