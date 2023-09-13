import { TrolleyProductReadDTO } from "./TrolleyProductReadDTO";

export interface TrolleyReadDTO {

    trolleyId: string;
    userId: number;
    total: number;
    discountedTotal: number;
    savedTotal: number;
    trolleyProducts: TrolleyProductReadDTO[];
}