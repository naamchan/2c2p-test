import React, { useState } from "react";
import axios from "axios";
import "./App.css";
import SelectByCurrency from "./SelectByCurrency";
import { TransactionModel } from "./Models/transaction_model";
import SelectResultTable from "./SelectResultTable";
import SelectByStatus from "./SelectByStatus";
import SelectByTransactionDate from "./SelectByTransactionDate";

function App() {
  const [file, setFile] = useState<FileList | null>(null);
  const [message, setMessage] = useState<string>("");
  const [selectResult, setSelectResult] = useState<TransactionModel[] | null>(
    null
  );

  const upload = async () => {
    if (file === null) {
      return;
    }

    const formData = new FormData();
    formData.append("file", file[0]);
    try {
      const uploadRequest = await axios.request({
        url: "/upload",
        method: "POST",
        data: formData,
      });

      setMessage("Upload completed");
      console.log(uploadRequest.data);
    } catch (e) {
      setMessage(e.response.data);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <div className="distinguish-border">
          <input type="file" onChange={(e) => setFile(e.target.files)}></input>
          <button onClick={upload}>Upload</button>
        </div>
        <SelectByCurrency setResult={setSelectResult} setError={setMessage} />
        <SelectByStatus setResult={setSelectResult} setError={setMessage} />
        <SelectByTransactionDate
          setResult={setSelectResult}
          setError={setMessage}
        />
        <p>{message}</p>
        <SelectResultTable selectResult={selectResult} />
      </header>
    </div>
  );
}

export default App;
