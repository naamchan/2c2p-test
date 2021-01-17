import { useState } from "react";
import axios from "axios";
import { isArrayOf } from "./types";
import {
  isTransactionModel,
  TransactionModel,
} from "./Models/transaction_model";

export default function SelectByTransactionDate(props: {
  setResult: React.Dispatch<React.SetStateAction<TransactionModel[] | null>>;
  setError: React.Dispatch<React.SetStateAction<string>>;
}) {
  const [startDate, setStartDate] = useState<string>(new Date().toISOString());
  const [endDate, setEndDate] = useState<string>(new Date().toISOString());

  const selectByTransactionDate = async () => {
    try {
      const uploadRequest = await axios.request({
        url: `/select/transaction_date`,
        method: "GET",
        params: {
          start: startDate,
          end: endDate,
        },
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

  const validateDateString = (str: string) => {
    return isFinite(Date.parse(str));
  };

  const getValidationIcon = () => {
    return isValidDate() ? "✅" : "❎";
  };

  const isValidDate = () => {
    return validateDateString(startDate) && validateDateString(endDate);
  };

  return (
    <div className="distinguish-border">
      <span>Start</span>
      <input
        type="text"
        value={startDate}
        onChange={(e) => setStartDate(e.target.value)}
      />
      <span>End</span>
      <input
        type="text"
        value={endDate}
        onChange={(e) => setEndDate(e.target.value)}
      />
      <button onClick={selectByTransactionDate} disabled={!isValidDate()}>
        {`${getValidationIcon()} Show All Transaction with between ${startDate} and ${endDate}`}
      </button>
    </div>
  );
}
