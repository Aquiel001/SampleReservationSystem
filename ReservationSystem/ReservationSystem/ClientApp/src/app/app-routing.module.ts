import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ListReservationComponent} from './list-reservation/list-reservation.component';
import {FormReservationComponent} from './form-reservation/form-reservation.component';

const routes: Routes = [
  {path: '', component: ListReservationComponent, pathMatch: 'full'},
  {path: 'form', component: FormReservationComponent},
  {path: 'form/:id', component: FormReservationComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
