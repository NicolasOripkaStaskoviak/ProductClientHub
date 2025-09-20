using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductClientHub.API.Filters;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using Xunit;

public class ExceptionFilterTests
{
    [Fact]
    public void OnException_ErrorOnValidationException_DeveRetornarBadRequest()
    {
        var context = new ExceptionContext(new ActionContext(), new List<IFilterMetadata>())
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
        var context = new ExceptionContext(new ActionContext(), new List<IFilterMetadata>())
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
