namespace Renderings.Tests.Mocks
{
    [RenderingDocumentAlias("test")]
    public class MockRendering : ISidebar, IRendering
    {
        public MockRendering(MockSource mockSource, IService service)
        {
            Mock = mockSource;
            Service = service;
        }

        public MockSource Mock { get; }

        public IService Service { get; }

        [RenderingPropertyAlias("testName")]
        public string Name { get; }

        public string UnmappedProperty { get; }

        public bool IsFullPage => true;

        public string GetPartialView(string renderTag = null)
        {
            return "TestView";
        }
    }
}
