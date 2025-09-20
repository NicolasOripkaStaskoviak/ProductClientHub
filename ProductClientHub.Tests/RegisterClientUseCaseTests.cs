using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;
using Xunit;

public class RegisterClientUseCaseTests
{
    [Fact]
    public void Execute_InvalidEmail_Throws_Exception()
    {
        var useCase = new RegisterClientUseCase();
        var request = new RequestClientJson { Name = "Teste", Email = "invalido" };

        var ex = Assert.Throws<ErrorOnValidationException>(() => useCase.Execute(request));
        Assert.Contains("E-mail inválido.", ex.GetErrors());
    }

    [Fact]
    public void Execute_EmptyName_Throws_Exception()
    {
        var useCase = new RegisterClientUseCase();
        var request = new RequestClientJson { Name = "", Email = "teste@teste.com" };

        var ex = Assert.Throws<ErrorOnValidationException>(() => useCase.Execute(request));
        Assert.Contains("O nome não pode ser vazio.", ex.GetErrors());
    }

    [Fact]
    public void Execute_ValidRequest_ReturnResponseWithSameName()
    {
        var useCase = new RegisterClientUseCase();
        var request = new RequestClientJson { Name = "Cliente Teste", Email = "teste@teste.com" };

        var response = useCase.Execute(request);

        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
    }


}
