
    export interface Arquivos {
        ServidorId: string;
        Servidor?: any;
        Tramitacao: Tramitacao[];
        FileName: string;
        FileSize: string;
        FileType: string;
        LastModifiedDate: Date;
        FileAsBase64: string;
        FileAsByteArray: string;
        Categoria: number;
        SetorTramitacao: number;
        Id: string;
        RegisterDate: Date;
        Active: boolean;
        SetorDescricao: string;
        CategoriaDescicao: string;
    }

    export interface Files {
        PageSize: number;
        IndexFrom: number;
        PageIndex: number;
        TotalCount: number;
        TotalPages: number;
        HasNextPage: boolean;
        HasPreviousPage: boolean;
        Items: Arquivos[];
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
    }
    

