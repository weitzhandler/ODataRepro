using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ODataRepro.Tests
{
    public class EntityControllerTests
    {
        [Fact]
        public async Task Shoud_throw_NullReferenceException()
        {
            // arrange
            var expectedErrorMessage =
@"System.ArgumentNullException: Value cannot be null. (Parameter 'edmModel')
   at Microsoft.AspNetCore.OData.Edm.EdmClrTypeMapExtensions.GetEdmType(IEdmModel edmModel, Type clrType)
   at Microsoft.AspNetCore.OData.Query.EnableQueryAttribute.GetModel(Type elementClrType, HttpRequest request, ActionDescriptor actionDescriptor)
   at Microsoft.AspNetCore.OData.Query.EnableQueryAttribute.GetODataQueryContext(Object responseValue, IQueryable singleResultCollection, ControllerActionDescriptor actionDescriptor, HttpRequest request)
   at Microsoft.AspNetCore.OData.Query.EnableQueryAttribute.GetModelBoundPageSize(ActionExecutedContext actionExecutedContext, Object responseValue, IQueryable singleResultCollection, ControllerActionDescriptor actionDescriptor, HttpRequest request)
   at Microsoft.AspNetCore.OData.Query.EnableQueryAttribute.OnActionExecuted(ActionExecutedContext actionExecutedContext, Object responseValue, IQueryable singleResultCollection, ControllerActionDescriptor actionDescriptor, HttpRequest request)
   at Microsoft.AspNetCore.OData.Query.EnableQueryAttribute.OnActionExecuted(ActionExecutedContext actionExecutedContext)
   at Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)

HEADERS
=======
Host: localhost
";
            var expectedStatusCode = HttpStatusCode.InternalServerError;
            using var app = new WebApplicationFactory<Startup>();
            using var client = app.CreateDefaultClient();

            // act
            var actualResponse = await client.GetAsync("api/entities");
            var actualContent = await actualResponse.Content.ReadAsStringAsync();

            // assert
            Assert.False(actualResponse.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, actualResponse.StatusCode);
            Assert.Equal(expectedErrorMessage, actualContent);
        }
    }
}