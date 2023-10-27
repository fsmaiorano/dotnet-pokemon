import { Sprite } from './sprite.model';
import { Type } from './type.model';

export class Pokemon {
  id: number;
  name: string;
  height: number;
  weight: number;
  evolvesFrom: number;
  sprites: Sprite;
  types: Type[] = [];

  constructor(
    id: number,
    name: string,
    height: number,
    weight: number,
    evolvesFrom: number,
    sprites: Sprite
  ) {
    this.id = id;
    this.name = name;
    this.height = height;
    this.weight = weight;
    this.evolvesFrom = evolvesFrom;
    this.sprites = sprites;
  }
}
