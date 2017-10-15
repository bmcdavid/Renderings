using DotNetStarter.Abstractions;
using Examine.LuceneEngine;
using Examine.LuceneEngine.Providers;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using UmbracoExamine;

namespace Renderings.UmbracoCms.Search
{
    public class ContentIndexCustomizer : ApplicationEventHandler
    {
        public Import<ILocator> FallbackLocator { get; set; }
        private readonly ILocator _Locator;
        private readonly IEnumerable<IContentIndexCustomizer> _IndexCustomizers;

        public ContentIndexCustomizer() : this(unscopedLocator: null)
        {
        }

        public ContentIndexCustomizer(IEnumerable<IContentIndexCustomizer> indexCustomizers = null, ILocator unscopedLocator = null)
        {
            _Locator = unscopedLocator ?? FallbackLocator.Service;
            _IndexCustomizers = indexCustomizers ?? _Locator.GetAll<IContentIndexCustomizer>();
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            foreach (var item in Examine.ExamineManager.Instance.IndexProviderCollection)
            {
                if (item is BaseUmbracoIndexer contentIndexer)
                {
                    contentIndexer.DocumentWriting += (sender, args) => Indexer_DocumentWriting(contentIndexer, args, contentIndexer.Name);
                }
            }

            base.ApplicationStarting(umbracoApplication, applicationContext);
        }

        private void Indexer_DocumentWriting(BaseUmbracoIndexer indexer, DocumentWritingEventArgs e, string indexerName)
        {
            e.Fields.TryGetValue(Constants.PropertyAlias.NodeTypeAlias, out string documentAlias);
            var customizers = _IndexCustomizers
                                .Where(c => c.CanIndex(documentAlias ?? "", indexerName));

            foreach (var customizer in customizers)
            {
                var customizedProperties = customizer.CustomizeItems(e.Fields, indexer.DataService) ?? Enumerable.Empty<IContentIndexItem>();

                foreach (var customized in customizedProperties)
                {
                    if (string.IsNullOrWhiteSpace(customized.FieldName))
                    {
                        throw new NullReferenceException($"{customized.GetType().FullName} returned an empty value for {nameof(customized.FieldName)}!");
                    }

                    if (e.Fields.ContainsKey(customized.FieldName))
                    {
                        e.Document.RemoveField(customized.FieldName);
                    }

                    //"Examine.LuceneEngine.SearchCriteria.LuceneBooleanOperation" orderBy imple
                    //https://github.com/Shazwazza/Examine/blob/master/src/Examine/LuceneEngine/Providers/LuceneIndexer.cs#L1283

                    Field.Store store = customized.Store ? Field.Store.YES : Field.Store.NO;
                    Field.TermVector termVector = customized.Store ? Field.TermVector.YES : Field.TermVector.NO;
                    Field.Index analyzed = customized.Analyzed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED;
                    Field field = null;
                    Field sortableField = null;
                    string sortableValue = customized.Value;

                    if (customized.ValueType == typeof(string))
                    {
                        field = new Field(customized.FieldName, customized.Value, store, analyzed, termVector);
                    }
                    else if (customized.ValueType == typeof(DateTime))
                    {
                        DateTime custom = DateTime.Parse(customized.Value);
                        sortableValue = DateTools.DateToString(custom, DateTools.Resolution.MILLISECOND);
                        field = new Field(customized.FieldName, sortableValue, Field.Store.YES, Field.Index.NOT_ANALYZED);
                    }
                    else
                    {
                        throw new ArgumentException($"{customized.FieldName} has type of {customized.ValueType.FullName} which isn't supported for index customizing!");
                    }

                    e.Document.Add(field);

                    if (customized.Sortable)
                    {
                        sortableField = sortableField ??
                                            new Field
                                            (
                                                LuceneIndexer.SortedFieldNamePrefix + customized.FieldName,
                                                sortableValue,
                                                Field.Store.NO,
                                                Field.Index.NOT_ANALYZED,
                                                Field.TermVector.NO
                                            );

                        e.Document.Add(sortableField);
                    }
                }
            }
        }
    }
}