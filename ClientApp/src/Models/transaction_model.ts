export interface TransactionModel {
    id: string;
    payment: string;
    status: string;
}

export function isTransactionModel(obj: unknown): obj is TransactionModel {
    const asTransactionModel = obj as TransactionModel;
    return typeof asTransactionModel.id === "string" &&
        typeof asTransactionModel.payment === "string" &&
        typeof asTransactionModel.status === "string";
}
