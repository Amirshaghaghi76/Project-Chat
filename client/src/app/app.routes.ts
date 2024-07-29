import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { Component } from '@angular/core';
import { NoAccessComponent } from './components/no-access/no-access.component';
import { NotfoundComponent } from './components/not-found/notfound.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'no-access', component: NoAccessComponent },
    { path: '**', component: NotfoundComponent },
];
