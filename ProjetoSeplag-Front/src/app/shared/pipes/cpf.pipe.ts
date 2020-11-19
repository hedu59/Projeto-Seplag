import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'CPF'
})
export class CPFPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    var res = value.substr(0, 3);

 
      var cpf = value.substr(0, 3) + '.' + value.substr(3, 3) + '.' + value.substr(6, 3) + '-' + value.substr(9, 2)

    
    return cpf;

  }

}
