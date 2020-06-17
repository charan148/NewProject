import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { DesignerService } from 'app/designer/designer.service';
import { FuseSidebarModule } from '@fuse/components';
import { DndListModule } from 'ngx-drag-and-drop-lists';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DesignerComponent } from './designer/designer.component';
import { TextboxComponent } from './textbox/textbox.component'
import { ContainerComponent } from './container/container.component'
import { TextareaComponent } from './textarea/textarea.component'
import { CheckboxComponent } from './checkbox/checkbox.component'
import { DropdownComponent } from './dropdown/dropdown.component'
import { LayoutpopupComponent} from '../designer/layoutpopup/layoutpopup.component'
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { AddoptionpopupComponent } from '../designer/addoptionpopup/addoptionpopup.component'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule} from '@angular/material/radio'
import { RadioComponent } from './radio/radio.component';
import { TemplateComponent } from './template/template.component';
import { AdministrationService } from '../administration/administration.service';
import { RequestTemplateComponent } from './request-template/request-template.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

const routes = [

    {
        path: 'designer',
        component: DesignerComponent,
        resolve: {
            data: DesignerService
        }

    },
    {
        path: 'requesttemplate',
        component: RequestTemplateComponent
    },
    
];

@NgModule({
    declarations: [
        
        DesignerComponent,
        TextboxComponent ,
        ContainerComponent,
        TextareaComponent,
        CheckboxComponent,
        DropdownComponent,
        LayoutpopupComponent,
        AddoptionpopupComponent,
        RadioComponent,
        TemplateComponent,
        RequestTemplateComponent

    ],
    imports: [
        RouterModule.forChild(routes),
        MatButtonModule,
        FuseSharedModule,
        FuseSidebarModule,
        MatDialogModule,
        DndListModule,
        FormsModule,
        ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),
        MatSelectModule,
        MatTabsModule,
        MatFormFieldModule,
        MatInputModule,
        MatCheckboxModule,
        MatRadioModule,
        MatSnackBarModule,
        MatChipsModule,
        MatRippleModule,
        MatExpansionModule,
        MatPaginatorModule,
        MatSortModule,
MatTableModule

    ],
    entryComponents: [LayoutpopupComponent, AddoptionpopupComponent],
    providers: [
        DesignerService, AdministrationService
    ]
})
export class DesignerModule {
}
