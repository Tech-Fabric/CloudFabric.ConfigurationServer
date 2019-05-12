using System;
using System.Threading.Tasks;
using CloudFabric.ConfigurationServer.GrainInterfaces;
using CloudFabric.ConfigurationServer.Grains;
using TechTalk.SpecFlow;
using Orleans.TestKit;
using Shouldly;
using Moq;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;
using System.Linq;

namespace CloudFabric.ConfigurationServer.Domain.Tests
{
    [Binding]
    public class ConfigurationSteps : BaseSteps
    {
        private const string AllClientNames = nameof(AllClientNames);
        private const string RemovedClientId = nameof(RemovedClientId);

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

        [Given(@"a Client (.*) was removed")]
        public async Task GivenAClientWasRemoved(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);

            await configuration.RemoveClient(name);
        }

        [When(@"I add a new Client with name (.*)")]
        public async Task WhenIAddANewClientWithName(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);

            await configuration.AddClient(name);
        }

        [When(@"I get the list of Client names")]
        public async Task WhenIGetTheListOfClientNames()
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);

            this.ScenarioContext.Set(await configuration.GetAllClientNames(), AllClientNames);
        }

        [When(@"I remove Client with name (.*)")]
        public async Task WhenIRemoveClientWithName(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);
            var state = this.Context.Silo.State(configuration);

            this.ScenarioContext.Set(state.Clients.ContainsKey(name) ? state.Clients[name] : (Guid?)null, RemovedClientId);

            await configuration.RemoveClient(name);
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
            this.ScenarioContext.TestError.ShouldBeOfType<Exception>(); // TODO: custom exception
            this.ScenarioContext.TestError.Message.ShouldContain(name);
        }

        [Then(@"the Client (.*) is removed")]
        public async Task ThenTheClientIsRemoved(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);
            var state = this.Context.Silo.State(configuration);

            state.ShouldNotBeNull();
            state.Clients.ShouldNotContainKey(name);

            var removedClientId = this.ScenarioContext.Get<Guid>(RemovedClientId);
            var clientConfiguration = this.Context.Silo.GrainFactory.GetGrain<IClientConfiguration>(removedClientId);

            var clientConfigurationMock = Mock.Get(clientConfiguration);

            clientConfigurationMock.Verify(x => x.Delete(), Times.Once());
        }

        [Then(@"removal succeeds while Client (.*) is not actully removed")]
        public async Task ThenRemovalSucceedsWhileClientIsNotActullyRemoved(string name)
        {
            var configuration = await this.Context.GetGrain<Configuration>(0);
            var state = this.Context.Silo.State(configuration);

            state.ShouldNotBeNull();
            state.Clients.ShouldNotContainKey(name);

            var removedClientId = this.ScenarioContext.Get<Guid?>(RemovedClientId);
            removedClientId.HasValue.ShouldBeFalse();
        }

        [Then(@"the following names should be returned")]
        public void ThenTheFollowingNamesShouldBeReturned(IEnumerable<ClientName> table)
        {
            var clientNames = this.ScenarioContext.Get<string[]>(AllClientNames);

            clientNames.ShouldBe(table.Select(x => x.Name), ignoreOrder: true);
        }

        public class ClientName
        {
            public string Name { get; set; }
        }

        [StepArgumentTransformation]
        public IEnumerable<ClientName> GetClientNames(Table table) => table.CreateSet<ClientName>();
    }
}
