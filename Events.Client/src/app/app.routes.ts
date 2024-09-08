import { Routes } from '@angular/router';
import { LiveEventListComponent } from './features/liveEvent/live-event-list/live-event-list.component';
import { AddLiveEventComponent } from './features/liveEvent/add-live-event/add-live-event.component';
import { EditLiveEventComponent } from './features/liveEvent/edit-live-event/edit-live-event.component';
import { LoginComponent } from './features/login/login.component';
import { UserPageComponent } from './features/user/user-page/user-page.component';
import { RoleGuard } from './features/login/services/role.guard';
import { RegisterComponent } from './features/register/register/register.component';
import { LiveEventInfoComponent } from './features/liveEvent/live-event-info/live-event-info.component';


export const routes: Routes = [
  {
    path: 'liveEvents',
    component: LiveEventListComponent,
    canActivate: [RoleGuard]
  },
  {
    path: 'liveEvents/add',
    data: {
      role: 'Admin'
    },
    component: AddLiveEventComponent,
    canActivate: [RoleGuard]
  },
  {
    path: 'liveEvents/:id',
    component: EditLiveEventComponent,
    canActivate: [RoleGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: '',
    canActivate: [RoleGuard],
    component: UserPageComponent
  },
  {
    path: 'liveEvents/info/:id',
    canActivate: [RoleGuard],
    component: LiveEventInfoComponent
  }
];
