import { GenericContent } from './generic-content.model';

export class Specie {
  externalId: number | undefined;
  name: string | undefined;
  base_happiness: number | undefined;
  capture_rate: number | undefined;
  color: GenericContent | undefined;
  habitat: GenericContent | undefined;
  has_gender_differences: boolean | undefined;
  hatch_counter: number | undefined;
  is_baby: boolean | undefined;
  is_legendary: boolean | undefined;
  is_mythical: boolean | undefined;
  evolves_from_species: GenericContent | undefined;
  flavor_text_entries: FlavorTextEntries[] | undefined;
}

class FlavorTextEntries {
  flavor_text: string | undefined;
  language: GenericContent | undefined;
  version: GenericContent | undefined;
}
