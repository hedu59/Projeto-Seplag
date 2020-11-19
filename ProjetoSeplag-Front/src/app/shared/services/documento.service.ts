import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  Files } from '../models/file-list-model';
import { FileToUpload } from '../models/file-upload-model';
import { RetornoTramitacaoModel } from '../models/retorno-tramitacao.model';
import { BaseService } from './base.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DocumentoService extends BaseService {

  constructor(private http: HttpClient) 
  {   
    super();
  }
  
  public UpdateFile(dados: any, file: FileToUpload, ) { 
    let form = {
      servidorId: localStorage.getItem("IdServidor"),
      fileName: file.fileName,
      fileSize: file.fileSize,
      fileType: file.fileType,
      fileAsBase64: file.fileAsBase64,
      fileAsByteArray: "",
      Categoria: dados.Categoria
    }; 
    return this.http.post(`${super.getHostApi()}Documento`, form, super.getHeader()).pipe(
      catchError(super.serviceError));;
  }
  
  public ObterArquivos(page:any) {
    
    var pagesize=10;
    const Id = localStorage.getItem('IdServidor')   
    
    return this.http.get<Files>
    (`${super.getHostApi()}Documento?servidorId=${Id}&pageIndex=${page}&pageSize=${pagesize}`, super.getHeader()).pipe(
      catchError(super.serviceError));
  }
  
  public VisualizarArquivos() {
    
    const Id = localStorage.getItem('IdServidor')   
    return this.http.get<Files>(`${super.getHostApi()}Documento/${Id},${Id}`, super.getHeader()).pipe(
    catchError(super.serviceError));
  }
  
}
