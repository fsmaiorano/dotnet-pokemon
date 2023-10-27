export class Type {
  slot: number = 0;
  type: TypeObject[] = [];
}

class TypeObject {
  name: string = '';
  url: string = '';

  constructor(name: string, url: string) {
    this.name = name;
    this.url = url;
  }
}
