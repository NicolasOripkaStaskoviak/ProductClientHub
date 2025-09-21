using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterClientUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public RegisterClientUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseClientJson Execute(RequestClientJson request)
        {
            var validator = new RegisterClientValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }

            var client = new Client { Name = request.Name, Email = request.Email };
            _context.Clients.Add(client);
            _context.SaveChanges();

            return new ResponseClientJson { id = client.Id, Name = client.Name };
        }
    }
}
