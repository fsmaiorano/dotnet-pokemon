import { Sprite } from './sprite.model';
import { Type } from './type.model';

export class Pokemon {
  externalId: number;
  name: string;
  height: number;
  weight: number;
  evolvesFrom: number;
  sprites: Sprite;
  types: Type[] = [];

  constructor(
    externalId: number,
    name: string,
    height: number,
    weight: number,
    evolvesFrom: number,
    sprites: Sprite
  ) {
    this.externalId = externalId;
    this.name = name;
    this.height = height;
    this.weight = weight;
    this.evolvesFrom = evolvesFrom;
    this.sprites = sprites;
  }
}
