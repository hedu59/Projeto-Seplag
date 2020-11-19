import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  protected getHostApi() {
    return 'http://localhost:5001/api/';
  }

  protected getHeader() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
        'Access-Control-Allow-Headers': 'Origin, Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers'
      })
    };
  }
  
  protected serviceError(error: Response | any){
    if(error.status == 401) {
        sessionStorage.removeItem("token");
        window.location.reload();
    }
    return throwError(error.status);
}

}
