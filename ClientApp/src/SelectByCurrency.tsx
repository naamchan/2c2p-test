import { useState } from "react";
import axios from "axios";
import { isArrayOf } from "./types";
import {
  isTransactionModel,
  TransactionModel,
} from "./Models/transaction_model";

export default function SelectByCurrency(props: {
  setResult: React.Dispatch<React.SetStateAction<TransactionModel[] | null>>;
  setError: React.Dispatch<React.SetStateAction<string>>;
}) {
  const [currency, setCurrency] = useState<string>("USD");

  const selectByCurrency = async () => {
    if (currency === null) {
      return;
    }

    try {
      const uploadRequest = await axios.request({
        url: `/select/currency/${currency}`,
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      const models = uploadRequest.data as unknown;

      if (isArrayOf<TransactionModel>(models, isTransactionModel)) {
        props.setResult(models);
        props.setError("");
      } else {
        props.setResult(null);
        props.setError(`Cannot convert ${models} to Transaction model`);
      }
    } catch (e) {
      props.setResult(null);
      props.setError(e.response.data);
    }
  };

  return (
    <div className="distinguish-border">
      <input
        type="text"
        onChange={(e) => setCurrency(e.target.value)}
        value={currency}
        maxLength={3}
      ></input>
      <button onClick={selectByCurrency}>
        Show All Transaction with currency {currency}
      </button>
    </div>
  );
}
