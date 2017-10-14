namespace Renderings.Tests.Mocks
{
    [RenderingDocumentAlias("test")]
    public class MockRendering
    {
        public MockRendering(MockSource mockSource, IService service)
        {
            Mock = mockSource;
            Service = service;
        }

        public  MockSource Mock { get; }

        public IService Service { get; }

        [RenderingPropertyAlias("testName")]
        public string Name { get; }

        public string UnmappedProperty { get; }
    }
}
