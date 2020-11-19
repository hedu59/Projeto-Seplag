import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Item, Tramitacao } from 'app/shared/models/servidor-model';
import { DocumentoService } from 'app/shared/services/documento.service';
import { ServidorService } from 'app/shared/services/servidor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DocumentoComponent } from '../documentos/documento/documento.component';

@Component({
  selector: 'app-servidor-detalhes',
  templateUrl: './servidor-detalhes.component.html',
  styleUrls: ['./servidor-detalhes.component.css']
})
export class ServidorDetalhesComponent implements OnInit {
  @ViewChild(DocumentoComponent) private documento: any;
  
  
  active=1;
  disabled = true;
  public famailia:any;
  public tramitacoes;
  public Id: any;
  public servidorModel: Item;
  public tramitacao: Tramitacao[];
  public setorAtualDescricao: string;
  setorId: number;
  
  constructor(
    private serviceServidor: ServidorService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
    ) { }

  ngOnInit() {
    this.Id = localStorage.getItem("IdServidor");
    this.ObterDetalhes(this.Id);  
  }
  
  ObterDetalhes(Id:any){
    
    if (!this.disabled) { this.disabled = true; }
    this.spinner.hide();  
    this.serviceServidor.DetalhesServidores(Id).subscribe(res => {
      this.tramitacao = res.Tramitacoes;
      this.servidorModel = res;  
      this.setorAtualDescricao = res.SetorDescricao;
      this.setorId = res.SetorTramitacao; 
      //this.toastr.success(this.servidorModel.Nome, "Dados", { progressBar: true });
      
    })
    
  }
   
  reciverFeedback() {
    
    this.active = 2;
    this.disabled = false;
    console.log("Resposta do filho")
    var Id= localStorage.getItem("IdServidor");
    this.ObterDetalhes(Id);
  }

}
