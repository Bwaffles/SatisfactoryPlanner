export type Node = {
    id: string;
    purity: string;
    biome: string;
    number: number;
    mapPositionX: number;
    mapPositionY: number;
    mapPositionZ: number;
    isTapped: boolean;
    amountToExtract: number;
    totalResources: number;
    resourceId: string;
    resourceName: string;
};

export type NodeDetails = {
    id: string;
    purity: string;
    biome: string;
    number: number;
    isTapped: boolean;
    amountToExtract: number;
    resourceId: string;
    resourceName: string;
};
