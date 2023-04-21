export type Node = {
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

export type NodeDetails = {
    id: string;
    purity: string;
    biome: string;
    number: number;
    isTapped: boolean;
    extractionRate: number;
    resourceId: string;
    resourceName: string;
    availableExtractors: AvailableExtractor[];
};

export type AvailableExtractor = {
    id: string;
    name: string;
    maxExtractionRate: number;
};
