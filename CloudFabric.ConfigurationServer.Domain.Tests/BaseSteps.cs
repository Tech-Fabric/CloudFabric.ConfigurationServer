using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace CloudFabric.ConfigurationServer.Domain.Tests
{
    public abstract class BaseSteps
    {
        protected readonly SiloContext Context;
        protected readonly ScenarioContext ScenarioContext;

        public BaseSteps(SiloContext context, ScenarioContext scenarioContext)
        {
            this.Context = context;
            this.ScenarioContext = scenarioContext;
        }

        [AfterStep("ExpectException")]
        public void ExpectException()
        {
            if (this.ScenarioContext.StepContext.StepInfo.StepDefinitionType == StepDefinitionType.When)
            {
                var scenarioExecutionStatusProperty = typeof(ScenarioContext).GetProperty(nameof(ScenarioContext.ScenarioExecutionStatus), BindingFlags.Public | BindingFlags.Instance);
                scenarioExecutionStatusProperty.SetValue(this.ScenarioContext, ScenarioExecutionStatus.OK);
            }
        }
    }
}
