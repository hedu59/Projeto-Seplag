import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetornoServidor } from '../models/retorno-servidor-model';
import { RetornoTramitacaoModel } from '../models/retorno-tramitacao.model';
import { Item, ServidoresModel } from '../models/servidor-model';
import { BaseService } from './base.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ServidorService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }
   
   public ListarServidores(page?:number) {
     var pagina=0; 
     if(page != null || page != undefined){
       pagina = page;
     }

    return this.http.get<ServidoresModel>
    (`${super.getHostApi()}Servidor?pageIndex=${pagina}&pageSize=${5}` , super.getHeader()).pipe(
      catchError(super.serviceError));
  }
  
  public DetalhesServidores(Id:any) {
    let servidores: ServidoresModel;
    return this.http.get<Item>(`${super.getHostApi()}Servidor/${Id}` , super.getHeader()).pipe(
      catchError(super.serviceError));;
  }

  public SalvarServidores(dados) {

    let form = {
      nome: dados.Nome,
      cpf: dados.CPF,
      orgao: dados.Orgao,
      matricula: dados.Matricula,
    }
    var retorno = this.http.post<RetornoServidor>(super.getHostApi() + 'Servidor', form, super.getHeader()).pipe(
      catchError(super.serviceError));;
    return retorno;
  }
  
  public RealizarTramitacao(documentoId:any, SetorDestino:number){
    
    let form = {
      servidorId: localStorage.getItem("IdServidor"),
      tramitacao: SetorDestino
    };
  
    return this.http.put<RetornoTramitacaoModel>(`${super.getHostApi()}Servidor`, form, super.getHeader()).pipe(
      catchError(super.serviceError));   
  }
}
