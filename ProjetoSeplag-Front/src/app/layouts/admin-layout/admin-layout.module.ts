import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserComponent }            from '../../pages/user/user.component';
import { IconsComponent }           from '../../pages/icons/icons.component';
import { UpgradeComponent }         from '../../pages/upgrade/upgrade.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AdminLayoutRoutes   } from 'app/app.routing';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    NgbModule
  ],
  declarations: [
    UserComponent,
    UpgradeComponent,
    IconsComponent
  ]
})

export class AdminLayoutModule {}
