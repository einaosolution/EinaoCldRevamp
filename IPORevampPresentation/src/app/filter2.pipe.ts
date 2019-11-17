import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'filter2'
})
export class FilterPipe2 implements PipeTransform {
  transform(items: any[]): any[] {

    if(!items) return [];
   // if(!searchText) return items;
//searchText = searchText;
return items.filter( it => {

      return it.parentId == "0" ;
    });
   }
}
