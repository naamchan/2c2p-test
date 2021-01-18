#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace _2c2p_test.Common.FileFormat
{
    public abstract class XMLFormat<TModel> : IFileFormatToModel<TModel>
    {
        private List<TModel>? models;
        IEnumerable<TModel> IFileFormatToModel<TModel>.GetContents()
        {
            if (models is null)
            {
                throw new InvalidOperationException("Need to Parse before get contents");
            }
            return models; ;
        }

        async Task<IFileFormatToModel<TModel>?> IFileFormatToModel<TModel>.Parse(Stream readStream)
        {
            try
            {
                using var xmlReader = XmlReader.Create(readStream, new XmlReaderSettings()
                {
                    Async = true
                });
                await xmlReader.MoveToContentAsync();

                var (isSuccess, models) = await ParseModel(xmlReader);
                this.models = models;
                return isSuccess ? this : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        abstract protected Task<(bool, List<TModel>)> ParseModel(XmlReader reader);
    }
}