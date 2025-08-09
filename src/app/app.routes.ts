import { Routes } from '@angular/router';
import { PeopleListComponent } from './people-list/people-list.component';
import { AddPersonComponent } from './add-person/add-person.component';

export const routes: Routes = [
  { path: '', component: PeopleListComponent },
  { path: 'add-person', component: AddPersonComponent }
];