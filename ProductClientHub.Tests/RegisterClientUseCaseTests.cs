using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;
using Xunit;

public class RegisterClientUseCaseTests
{
    private RegisterClientUseCase CriarUseCase(string dbName)
    {
        var options = new DbContextOptionsBuilder<ProductClientHubDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new ProductClientHubDbContext(options);
        return new RegisterClientUseCase(context);
    }

    [Fact]
    public void Execute_EmailInvalido_DeveLancarExcecao()
    {
        var useCase = CriarUseCase("Db_EmailInvalido");
        var request = new RequestClientJson { Name = "Teste", Email = "invalido" };

        var ex = Assert.Throws<ErrorOnValidationException>(() => useCase.Execute(request));
        Assert.Contains("E-mail inválido.", ex.GetErrors());
    }

    [Fact]
    public void Execute_RequestValido_DeveRetornarResponseComMesmoNome()
    {
        var useCase = CriarUseCase("Db_RequestValido");
        var request = new RequestClientJson { Name = "Cliente Teste", Email = "teste@teste.com" };

        var response = useCase.Execute(request);

        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
    }

    [Fact]
    public void Execute_NomeVazio_DeveLancarExcecao()
    {
        var useCase = CriarUseCase("Db_NomeVazio");
        var request = new RequestClientJson { Name = "", Email = "teste@teste.com" };

        var ex = Assert.Throws<ErrorOnValidationException>(() => useCase.Execute(request));
        Assert.Contains("O nome não pode ser vazio.", ex.GetErrors());
    }

}
