import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'pictureUrl'
})
export class PictureUrlPipe implements PipeTransform {

  transform(text : string): string {
    return 'images/' + text.replace(/\s/g, '-').toLowerCase() + '.jpg';
  }

}
