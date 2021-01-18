import { useState } from "react";
import axios from "axios";
import { isArrayOf } from "./types";
import {
  isTransactionModel,
  TransactionModel,
} from "./Models/transaction_model";

interface StatusCodeWithText {
  code: string;
  text: string;
}

export default function SelectByStatus(props: {
  setResult: React.Dispatch<React.SetStateAction<TransactionModel[] | null>>;
  setError: React.Dispatch<React.SetStateAction<string>>;
}) {
  const [transactionStatusCode, setTransactionStatusCode] = useState<
    string | null
  >(null);

  const statusCodes: StatusCodeWithText[] = [
    { code: "A", text: "Accepted" },
    { code: "R", text: "Rejected" },
    { code: "D", text: "Done" },
  ];

  const selectByTransactionStatus = async () => {
    if (transactionStatusCode === null) {
      return;
    }

    try {
      const uploadRequest = await axios.request({
        url: `/select/status/${transactionStatusCode}`,
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

  const statusSelector = statusCodes.map((statusCode) => {
    return (
      <li>
        <input
          id={`status_selector_${statusCode.code}`}
          type="radio"
          name="unifiedStatusCode"
          value={statusCode.code}
          checked={transactionStatusCode === statusCode.code}
          onChange={(e) => setTransactionStatusCode(e.target.value)}
        />
        <label htmlFor={`status_selector_${statusCode.code}`}>
          {statusCode.text}
        </label>
      </li>
    );
  });

  return (
    <div className="distinguish-border">
      <ul>{statusSelector}</ul>
      <button onClick={selectByTransactionStatus}>
        {transactionStatusCode === null
          ? "Select status first"
          : `Show All Transaction with status ${transactionStatusCode}`}
      </button>
    </div>
  );
}
