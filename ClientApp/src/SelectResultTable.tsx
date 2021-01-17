import { TransactionModel } from "./Models/transaction_model";

export default function SelectResultTable(props: {
  selectResult: TransactionModel[] | null;
}) {
  const renderTable = () => {
    if (props.selectResult === null) {
      return <></>;
    }

    return props.selectResult.map((model) => {
      return (
        <tr>
          <td>{model.id}</td>
          <td>{model.payment}</td>
          <td>{model.status}</td>
        </tr>
      );
    });
  };

  if (props.selectResult === null) {
    return <></>;
  }

  return (
    <table>
      <thead>
        <th>Transaction ID</th>
        <th>Payment</th>
        <th>Status</th>
      </thead>
      {renderTable()}
    </table>
  );
}
