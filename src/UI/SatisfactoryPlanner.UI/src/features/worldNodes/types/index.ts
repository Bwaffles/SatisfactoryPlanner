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

export type WorldNodeDetails = {
    nodeId: string;
    purity: string;
    biome: string;
    number: number;
    resourceId: string;
    resourceName: string;
    isTapped: boolean;
    extractorId: string | null;
    extractionRate: number;
    availableExtractors: AvailableExtractor[];
};

export type AvailableExtractor = {
    id: string;
    name: string;
    maxExtractionRate: number;
};
