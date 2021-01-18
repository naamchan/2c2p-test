#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace _2c2p_test.Common.FileFormat
{
    public abstract class XMLFormat<TModel> : IFileFormat
    {
        private List<TModel>? models;

        async Task<IFileFormat?> IFileFormat.Parse(Stream readStream)
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
            catch (Exception e)
            {
                return null;
            }
        }

        abstract protected Task<(bool, List<TModel>)> ParseModel(XmlReader reader);
    }
}