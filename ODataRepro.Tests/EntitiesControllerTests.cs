using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ODataRepro.Tests
{
    public class EntitiesControllerTests
    {
        [Fact]
        public async Task Shoud_throw_NullReferenceException_on_get()
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
            Assert.Equal(expectedStatusCode, actualResponse.StatusCode);
            Assert.Equal(expectedErrorMessage, actualContent);
        }

        [Fact]
        public async Task Should_throw_exception_on_post()
        {
            // arrange
            var expectedErrorMessage =
@"System.InvalidOperationException: The request must have an associated EDM model. Consider using the extension method HttpConfiguration.MapODataServiceRoute to register a route that parses the OData URI and attaches the model information.
   at Microsoft.AspNetCore.OData.Results.ResultHelpers.GenerateODataLink(HttpRequest request, Object entity, Boolean isEntityId)
   at Microsoft.AspNetCore.OData.Results.CreatedODataResult`1.GenerateLocationHeader(HttpRequest request)
   at Microsoft.AspNetCore.OData.Results.CreatedODataResult`1.ExecuteResultAsync(ActionContext context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResultFilterAsync>g__Awaited|29_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)

HEADERS
=======
Host: localhost
Content-Type: application/json; charset=utf-8
";
            var expectedStatusCode = HttpStatusCode.InternalServerError;
            var entity = new Entity { Id = Guid.NewGuid() };
            using var app = new WebApplicationFactory<Startup>();
            using var client = app.CreateDefaultClient();

            // act
            var actualResponse = await client.PostAsJsonAsync("api/entities", entity);
            var actualContent = await actualResponse.Content.ReadAsStringAsync();

            // assert
            Assert.Equal(expectedStatusCode, actualResponse.StatusCode);
            Assert.Equal(expectedErrorMessage, actualContent);
        }

    }
}