import { NgModule, createComponent, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ProjectsComponent } from './components/projects/projects.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RegisterComponent } from './components/register/register.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { CreateComponent } from './components/create/create.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { UsersComponent } from './admin/users/users.component';
import { PortfolioComponent } from './components/portfolio/portfolio.component';

const routes: Routes = [
  {
    path:'',
    component:RegisterComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'sidebar',
    component:SidebarComponent
  },
  {
    path: 'dashboard/:projectId',
    component: DashboardComponent
  },
  {
    path: 'create',
    component: CreateComponent
  },
  {
    path:'navbar',
    component: NavbarComponent
  },
  {
    path: 'portfolio',
    component:PortfolioComponent
  },
  {
    path:'projects',
    component: ProjectsComponent
  },
  {
    path:'dashboard',
    component: DashboardComponent
  },{
    path:'users',
    component: UsersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
