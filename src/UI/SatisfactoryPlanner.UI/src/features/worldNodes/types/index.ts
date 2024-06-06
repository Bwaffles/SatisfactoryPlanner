export type WorldNode = {
  id: string;
  purity: string;
  biome: string;
  number: number;
  mapPositionX: number;
  mapPositionY: number;
  mapPositionZ: number;
  isTapped: boolean;
  extractionRate: number;
  maxExtractionRate: number;
  resourceId: string;
  resourceName: string;
};
