import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';



export const AdminLayoutRoutes: Routes = [
  {
    path: 'servidor',
    loadChildren: () => import('./pages/servidor/servidor.module').then(x => x.ServidorModule)
   },
];
export const AppRoutes: Routes = [
  {
    path: '',
    redirectTo: 'servidor',
    pathMatch: 'full',
  },
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
        {
      path: '',
      loadChildren: './layouts/admin-layout/admin-layout.module#AdminLayoutModule'
  }]},
  {
    path: '**',
    redirectTo: 'servidor'
  }
]

@NgModule({ 
  imports:[RouterModule.forRoot(AdminLayoutRoutes)],
  exports:[RouterModule]
})

export class AppRoutingModule { 
  
}
