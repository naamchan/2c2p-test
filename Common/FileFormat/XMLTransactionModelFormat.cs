#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using _2c2p_test.Model;
using _2c2p_test.Model.Factory;

namespace _2c2p_test.Common.FileFormat
{
    public class XMLTransactionModelFormat : XMLFormat<TransactionModel>
    {
        protected override Task<(bool, List<TransactionModel>)> ParseModel(XmlReader xmlReader)
        {
            List<TransactionModel> models = new();
            List<string> failedParse = new();

            while (!xmlReader.EOF)
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Transaction")
                {
                    var element = XElement.ReadFrom(xmlReader) as XElement;
                    if (element is null)
                    {
                        continue;
                    }

                    var id = element.Attribute("id")?.Value;
                    var transactionDate = element.Element("TransactionDate")?.Value;
                    var paymentDetails = element.Element("PaymentDetails");
                    var amount = paymentDetails?.Element("Amount")?.Value;
                    var currencyCode = paymentDetails?.Element("CurrencyCode")?.Value;
                    var status = element.Element("Status")?.Value;

                    var model = XMLTransactionModelFactory.Create(
                        id,
                        amount,
                        currencyCode,
                        transactionDate,
                        status);
                    if (model is null)
                    {
                        failedParse.Add(element.ToString());
                    }
                    else
                    {
                        models.Add(model);
                    }
                }
                else
                {
                    xmlReader.Read();
                }
            }
            return Task.FromResult((!failedParse.Any(), models));
        }
    }
}