using TestWell.Environment;

namespace RestWell.Examples.Resource.Shared
{
    public static class ExampleEnvironment
    {
        public static TestWellEnvironment Start()
        {
            return new TestWellEnvironmentBuilder()
                        .AddResourceWebService<Api.Startup>()
                        .BuildEnvironment();
        }
    }
}
