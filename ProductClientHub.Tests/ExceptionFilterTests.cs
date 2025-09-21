using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using ProductClientHub.API.Filters;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using Xunit;

public class ExceptionFilterTests
{
    [Fact]
    public void OnException_ErrorOnValidationException_DeveRetornarBadRequest()
    {
        // Criação do contexto "fake" completo
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var modelState = new ModelStateDictionary();

        var context = new ExceptionContext(
            new ActionContext(httpContext, routeData, actionDescriptor, modelState),
            new List<IFilterMetadata>()
        )
        {
            Exception = new ErrorOnValidationException(new List<string> { "Erro de teste" })
        };

        var filter = new ExceptionFilter();
        filter.OnException(context);

        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(400, context.HttpContext.Response.StatusCode);

        var response = Assert.IsType<ResponseErrorMessagesJson>(result.Value);
        Assert.Contains("Erro de teste", response.Errors);
    }



    [Fact]
    public void OnException_ExceptionDesconhecida_DeveRetornarErro500()
    {
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var modelState = new ModelStateDictionary();

        var context = new ExceptionContext(
            new ActionContext(httpContext, routeData, actionDescriptor, modelState),
            new List<IFilterMetadata>()
        )
        {
            Exception = new Exception("Falha inesperada")
        };

        var filter = new ExceptionFilter();
        filter.OnException(context);

        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(500, context.HttpContext.Response.StatusCode);

        var response = Assert.IsType<ResponseErrorMessagesJson>(result.Value);
        Assert.Contains("ERRO DESCONHECIDO", response.Errors);
    }
}
