import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ServidorRoutingModule } from './servidor-routing.module';
import { ServidorComponent } from './servidor/servidor.component';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServidorDetalhesComponent } from './servidor-detalhes/servidor-detalhes.component';
import { CPFPipe } from '../../shared/pipes/cpf.pipe';
import { ImportPipeModule } from 'app/import-pipe/importe.pipe.module';
import { DocumentoComponent } from './documentos/documento/documento.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { NgxSpinnerModule } from 'ngx-spinner';




@NgModule({
  declarations: [ServidorComponent, ServidorDetalhesComponent, CPFPipe, DocumentoComponent],
  imports: [
    CommonModule,
    ServidorRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    NgxExtendedPdfViewerModule,
    ImportPipeModule,
    PdfViewerModule,
    NgxSpinnerModule
  ],
  exports:[]
})
export class ServidorModule { }
