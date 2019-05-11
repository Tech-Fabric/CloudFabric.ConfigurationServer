using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using CloudFabric.ConfigurationServer.Grains;
using TechTalk.SpecFlow;
using Orleans.TestKit;
using Shouldly;
using Moq;

namespace CloudFabric.ConfigurationServer.Domain.Tests
{
    [Binding]
    public class ConfigurationSteps : BaseSteps
    {
        public ConfigurationSteps(SiloContext context, ScenarioContext scenarioContext)
            : base(context, scenarioContext)
        {
        }

        [Given(@"a Client with name (.*)")]
        public async Task GivenAClientWithName(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);

            await configuration.AddClient(name);
        }

        [When(@"I add a new Client with name (.*)")]
        public async Task WhenIAddANewClientWithName(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);

            await configuration.AddClient(name);
        }
        
        [Then(@"the Client configuration for (.*) is created")]
        public async Task ThenTheClientConfigurationForIsCreated(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);
            var state = this.Context.Silo.State(configuration);

            state.ShouldNotBeNull();
            state.Clients.ShouldContainKey(name);

            var clientConfiguration = this.Context.Silo.GrainFactory.GetGrain<IClientConfiguration>(state.Clients[name]);

            var clientConfigurationMock = Mock.Get(clientConfiguration);

            clientConfigurationMock.Verify(x => x.Create(It.Is<string>(a => a == name)), Times.Once());
        }

        [Then(@"adding fails as Client with name (.*) already exists")]
        public void ThenAddingFailsAsClientWithNameAlreadyExists(string name)
        {
            this.ScenarioContext.TestError.ShouldNotBeNull();
            this.ScenarioContext.TestError.ShouldBeOfType<Exception>();
            this.ScenarioContext.TestError.Message.ShouldContain(name);
        }
    }
}
