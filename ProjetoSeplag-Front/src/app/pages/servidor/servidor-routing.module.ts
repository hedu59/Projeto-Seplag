import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ServidorDetalhesComponent } from './servidor-detalhes/servidor-detalhes.component';
import { ServidorComponent } from './servidor/servidor.component';

const routes: Routes = [
  { path:'', component: ServidorComponent},
  { path:'detalhes/:Id', component: ServidorDetalhesComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServidorRoutingModule { }
