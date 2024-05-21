export type ProductionLine = {
  id: string;
  name: string;
};

export type ProductionLineDetails = {
  id: string;
  name: string;
};

export type ItemToProcess = {
  id: string;
  name: string;
  category: ItemCategory;
};

export type ItemCategory = {
  id: string;
  name: string;
};

export type Recipe = {
  id: string;
  name: string;
  type: RecipeType;
  ingredients: Ingredient[];
  products: Product[];
};

export enum RecipeType {
  Standard,
  Alternate,
}

export type RecipeItem = {
  itemId: string;
  itemName: string;
  amount: Amount;
};

export interface Ingredient extends RecipeItem {}
export interface Product extends RecipeItem {}

export type Amount = {
  amountPerCycle: number;
  amountPerMinute: number;
};
