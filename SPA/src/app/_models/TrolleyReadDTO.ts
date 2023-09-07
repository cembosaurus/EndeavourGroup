import { TrolleyProductReadDTO } from "./TrolleyProductReadDTO";

export interface TrolleyReadDTO {

    TrolleyId: string;
    UserId: number;
    Total: number;
    DiscountedTotal: number;
    SavedTotal: number;
    TrolleyProducts: TrolleyProductReadDTO[];
}