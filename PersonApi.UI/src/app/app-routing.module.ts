import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PersonListComponent} from './person-list/person-list.component';
import {PersonCreateComponent} from './person-create/person-create.component';
import {PersonEditComponent} from './person-edit/person-edit.component';

const routes: Routes = [
  {path: '', redirectTo: '/people', pathMatch: 'full'},
  {path: 'people', component: PersonListComponent},
  {path: 'people/create', component: PersonCreateComponent},
  {path: 'people/edit/:id', component: PersonEditComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
