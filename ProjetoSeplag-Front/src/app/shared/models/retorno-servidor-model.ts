

    export interface Data {
        Nome: string;
        CPF: string;
        Orgao: string;
        Matricula: string;
        Notifications: any[];
        Invalid: boolean;
        Valid: boolean;
       
    }

    export interface RetornoServidor {
        Data: Data;
        Success: boolean;
        Message: string;
    }
    
   
