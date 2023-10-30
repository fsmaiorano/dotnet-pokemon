export function pokemonTypeColor(types: string[]): string[] {
  const colors: string[] = [];

  if (types.length === 1) {
    types.push(types[0]);
  }

  types.forEach((type) => {
    switch (type) {
      case "normal":
        colors.push("#ff9c6a");
        break;
      case "fire":
        colors.push("#c5525a");
        break;
      case "water":
        colors.push("#3973ac");
        break;
      case "electric":
        colors.push("#debd39");
        break;
      case "grass":
        colors.push("#78C850");
        break;
      case "ice":
        colors.push("#98D8D8");
        break;
      case "fighting":
        colors.push("#C03028");
        break;
      case "poison":
        colors.push("#A040A0");
        break;
      case "ground":
        colors.push("#ffdeac");
        break;
      case "flying":
        colors.push("#A890F0");
        break;
      case "psychic":
        colors.push("#F85888");
        break;
      case "bug":
        colors.push("#A8B820");
        break;
      case "rock":
        colors.push("#B8A038");
        break;
      case "ghost":
        colors.push("#705898");
        break;
      case "dragon":
        colors.push("#7038F8");
        break;
      case "dark":
        colors.push("#705848");
        break;
      case "steel":
        colors.push("#B8B8D0");
        break;
      case "fairy":
        colors.push("#EE99AC");
        break;
      default:
        colors.push("#68A090");
        break;
    }
  });

  return colors;
}

export function pokemonColors() {
  return [
    "#ff9c6a",
    "#c5525a",
    "#3973ac",
    "#debd39",
    "#78C850",
    "#98D8D8",
    "#C03028",
    "#A040A0",
    "#ffdeac",
    "#A890F0",
    "#F85888",
    "#A8B820",
    "#B8A038",
    "#705898",
    "#7038F8",
    "#705848",
    "#B8B8D0",
    "#EE99AC",
  ];
}
