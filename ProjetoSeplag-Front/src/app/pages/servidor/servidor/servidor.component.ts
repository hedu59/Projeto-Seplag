import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RetornoServidor } from 'app/shared/models/retorno-servidor-model';
import { Item, Tramitacao } from 'app/shared/models/servidor-model';
import { ServidorService } from 'app/shared/services/servidor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-servidor',
  templateUrl: './servidor.component.html',
  styleUrls: ['./servidor.component.css']
})
export class ServidorComponent implements OnInit {

  //#region VARIÁVEIS
  active = 1;
  page = 1;
  pageSize = 5;
  closeResult = '';
  public formServidor: FormGroup;
  moduleLoading: boolean = false;
  retornoServidor: RetornoServidor;
  length: any;
  servidores: Item[];
  dados: any;
  public loading = false;
  public tramitacao: Tramitacao[];
  //#endregion

  constructor(
    private service: ServidorService,
    private modalService: NgbModal,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private routerLinkd: Router,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.ObterServidores();
    this.validarForm();
  }

  validarForm() {
    this.formServidor = this.fb.group({
      Nome: ['', [Validators.maxLength(80), Validators.required]],
      CPF: ['', [Validators.maxLength(11), Validators.minLength(11), Validators.required]],
      Orgao: ['', [Validators.required]],
      Matricula: ['', [Validators.required]],
    });
  }

  ObterServidores(page?: number) {
      
    console.log("SERVIDOR")
    this.spinner.show();
    this.service.ListarServidores(page).subscribe(res => {
 
      if (res.Items.length > 0) {
        
        this.servidores = res.Items;
        this.page = res.PageIndex+1;
        this.pageSize = res.PageSize;
        this.length = res.TotalCount;
  
      }
      else{
        
        this.toastr.info("Nenhum Processo/Benefício Cadastrado", "Nenhum Servidor", {progressBar:true})
      }
      this.spinner.hide();

    })
  }

  CriarServidor() {

    this.spinner.show();
    this.service.SalvarServidores(this.formServidor.value).subscribe(res => {
      this.modalService.dismissAll();
      this.spinner.show();
      this.retornoServidor = res;
      this.formServidor.reset();

      if (res.Success == true) {
        this.ObterServidores();

        this.spinner.hide();
        this.toastr.success("SALVO", "SALVO COM SUCESSO", { progressBar: true });
        return;
      }
      else {

        this.toastr.success(res.Data[0].Message, res.Data[0].Message.Property, { progressBar: true });
        this.spinner.hide();
      }
    }, err => {

      this.spinner.hide();
    })


  }

  DetalhesServidor(Id: any) {
    this.spinner.show();
    this.service.DetalhesServidores(Id).subscribe(res => {

      this.tramitacao = res.Tramitacoes;
      this.dados = Id;
      this.routerLinkd.navigate(['servidor/detalhes/', Id]);
      localStorage.setItem('IdServidor', Id.toString());

    })

  }

  AbrirModalCadastroServidor(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
  }

}
