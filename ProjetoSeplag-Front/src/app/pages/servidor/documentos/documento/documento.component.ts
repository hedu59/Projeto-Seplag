import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Arquivos } from 'app/shared/models/file-list-model';
import { FileToUpload } from 'app/shared/models/file-upload-model';
import { Tramitacao } from 'app/shared/models/servidor-model';
import { DocumentoService } from 'app/shared/services/documento.service';
import { ServidorService } from 'app/shared/services/servidor.service';
import { PDFProgressData } from 'ng2-pdf-viewer';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { promise } from 'protractor';

@Component({
  selector: 'app-documento',
  templateUrl: './documento.component.html',
  styleUrls: ['./documento.component.css']
})
export class DocumentoComponent implements OnInit {

  @Output() respostaFamilia = new EventEmitter();
  @Input() dadosPai;

  //#region variáveis

  pdfSrc: any;

  public arquivos: Arquivos[]
  public formArquivo: FormGroup;
  public formTramite: FormGroup;
  public files: any[] = [];
  public theFile: any[] = [];
  public fileUpload: FileToUpload;
  public arquivoAnexado: boolean = false;
  closeResult: string;
  public BASE64_MARKER = ';base64,';
  public NomeDocumento;
  public DocumentoId: any;
  public active = 1;
  public setorAtualDescricao: string;
  public setorAtualId: number;
  private contentModal: any;
  public fileName = '';
  public tramitacao: Tramitacao[];

  //#endregion

  //#region parametrosDocumento
  error: any;
  page = 1;
  rotation = 0;
  zoom = 1.0;
  zoomScale = 'page-width';
  originalSize = false;
  pdf: any;
  renderText = true;
  progressData: PDFProgressData;
  isLoaded = false;
  stickToPage = false;
  showAll = true;
  autoresize = true;
  fitToPage = false;
  outline: any[];
  isOutlineShown = false;
  pdfQuery = '';
  public tamanhoVertical = "0px;"
  setorId: number;
  //#endregion

  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private service: DocumentoService,
    private modalService: NgbModal,
    private spinner: NgxSpinnerService,
    private servidorService: ServidorService
  ) { }

  ngOnInit(): void {
    
    console.log("PASSEI AQUI")
    this.spinner.show();
    this.ValidarForm();
    this.ValidarFormTramite();
    this.ObterAquivos();
    
  }

  feedbackPai(Id: any) {
    this.respostaFamilia.emit(Id);

  }

  public ObterAquivos() {
    var page = 0;
    this.service.ObterArquivos(page).subscribe(res => {
      this.arquivos = res.Items;
      this.spinner.hide();

    }, err => {
      this.toastr.error("Favor, atualizar a página", "ERRO AO ABTER ARQUIVOS", { progressBar: true })
    });
  }

  //#region comandosModalArquivo
  rotate(angle: number) {
    this.rotation += angle;
  }

  incrementZoom(amount: number) {
    this.zoom += amount;
  }
  //#endregion

  ValidarForm() {

    this.formArquivo = this.fb.group({
      Categoria: ['', Validators.required],
      Arquivo: ['', Validators.required],
    });
  }

  ValidarFormTramite() {
    this.formTramite = this.fb.group({
      SetorDestino: ['', Validators.required]
    });
  }

  private CarregarArquivos(theFile: any) {
    let file = new FileToUpload();

    file.fileName = theFile.name;
    file.fileSize = theFile.size;
    file.fileType = theFile.type;
    file.lastModifiedTime = theFile.lastModified;
    file.lastModifiedDate = theFile.lastModifiedDate;

    let reader = new FileReader();

    reader.onload = () => {
      file.fileAsBase64 = reader.result.toString();
      this.files.push(file);
    }

    reader.readAsDataURL(theFile);
     
  }

  //Validar tipo e tamanho do arquivo
  public ValidarArquivos(event) {
    const filesName = [];
    this.theFile = [];
    document.getElementById("file").innerHTML = ("");
    var extPermitidas = ['pdf'];


    for (let i = 0; i < event.target.files.length; i++) {
      if (event.target.files[i].size <= 20971520) {
        this.arquivoAnexado = true;
        var extensao = event.target.files[i].name.split('.').pop();
        if (typeof extPermitidas.find(ext => extensao == ext) == 'undefined') {
          this.toastr.warning('Extensão "' + extensao + '" não permitida!', 'ARQUIVO INVALIDO!', { progressBar: true });
          this.theFile = [];
          return;
        }

        filesName.push(event.target.files[i].name);
        this.theFile.push(event.target.files[i]);
        this.CarregarArquivos(event.target.files[i]);
      } else {
        this.toastr.warning('Arquivo maior que o permitido! Tamanho máximo: 20Mb', 'TAMANHO EXCEDIDO!', { progressBar: true });
      }

    }
    document.getElementById("file").innerHTML = filesName.join(', ');
    console.log(this.theFile);

  }

  //Salvar Arquivos na base
  public SalvarAnexos() {

    this.spinner.show();
    this.fileUpload = this.files[0];
    this.fileName = this.files[0].fileName;   

    this.service.UpdateFile(this.formArquivo.value, this.fileUpload).subscribe(res => {

        this.toastr.success('Upload realizado com sucesso', 'SUCESSO!', { progressBar: true });
        this.formArquivo.reset();
        this.modalService.dismissAll();
        this.spinner.hide();
        this.files = [];
        this.ObterAquivos();

    }, err => {

      this.toastr.warning('Falha salvar arquivo!', 'FALHA NO UPLOAD!', { progressBar: true });
      this.spinner.hide();
    });

  }

  public AbrirModalAnexoDocumento(content) {

    this.fileName = null;
    this.formArquivo.reset();
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
  }

  public AbrirModalDocumento(content, base64: string, nome: string) {


    this.spinner.show();
    this.contentModal = content;
    this.NomeDocumento = nome;
    fetch(base64).then(res => res.blob()).then(res => this.pdfSrc = window.URL.createObjectURL(res));

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
    this.spinner.hide();
  }

  public AbrirModalTramitacao(content) {

    var Id = localStorage.getItem("IdServidor");
      this.spinner.show();
      this.servidorService.DetalhesServidores(Id).subscribe(res => {
    
      this.setorAtualDescricao = res.SetorDescricao;
      this.setorId = res.SetorTramitacao;
      this.tramitacao = res.Tramitacoes;

    }, err => {
      this.toastr.error('Por favor, tente novamente', 'ERRO AO BUSCAR TRAMITAÇÕES', { progressBar: true })
    })

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
    this.spinner.hide();

  }

  public VisualizarDocumento(base64: string) {
    this.tamanhoVertical = '400px';
    fetch(base64).then(res => res.blob()).then(res => this.pdfSrc = window.URL.createObjectURL(res));
  }

  public AbrirAbaTramitacao() {
    this.active = 2;
  }

  public ConfirmarTramitacao() {

    var setorDestino = this.formTramite.value.SetorDestino;
    var setor = this.setorId;
    var Id = localStorage.getItem("IdServidor");
    
    if (setorDestino == setor) {
      this.toastr.warning('Setor de origem e destino não podem ser o mesmo', 'SETOR DE DESTINO INVÁLIDO', { progressBar: true })
      return;
      
    }

      this.servidorService.RealizarTramitacao(Id, setorDestino).subscribe(res => {
      if (res.Success) {
        
        this.toastr.success('Alterado com sucesso', 'SETOR ALTERADO', { progressBar: true })
          var Id = localStorage.getItem("IdServidor");
          this.servidorService.DetalhesServidores(Id).subscribe(res => {

          this.tramitacao = res.Tramitacoes;
          this.DocumentoId = res.Id;
          this.setorAtualDescricao = res.SetorDescricao;
          this.setorId = res.SetorTramitacao;
          
          this.active = 1;
          this.formTramite.reset();

        });

      } else {

        this.toastr.warning('Não foi possível alterar o setor', 'ERRO AO ALTERAR SETOR', { progressBar: true })
      }

    }, err => {

      this.toastr.error('Não foi possível alterar o setor', 'ERRO AO ALTERAR SETOR', { progressBar: true })
    });
     
  }

}
