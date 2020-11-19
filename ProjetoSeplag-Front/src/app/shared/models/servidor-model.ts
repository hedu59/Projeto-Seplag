
export interface Item {
    Nome: string;
    CPF: string;
    Orgao: string;
    Matricula: string;
    Documentos: any[];
    Id: string;
    RegisterDate: Date;
    Active: boolean;
    SetorTramitacao: number;
    SetorDescricao:string;
    Tramitacoes: Tramitacao[];
}

export interface ServidoresModel {
    PageSize: number;
    IndexFrom: number;
    PageIndex: number;
    TotalCount: number;
    TotalPages: number;
    HasNextPage: boolean;
    HasPreviousPage: boolean;
    Items: Item[];
}

export interface Tramitacao {
    DocumentoId: string;
    Documento?: any;
    DataTramitacao: Date;
    SetorOrigem: number;
    SetorDestino: number;
    UsuarioMovimentacao: string;
    Id: string;
    RegisterDate: Date;
    Active: boolean;
    SetorOrigemDescricao: string;
    SetorDestinoDescricao: string;
}


