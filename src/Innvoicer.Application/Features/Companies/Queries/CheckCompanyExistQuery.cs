using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Domain.Companies;
using MediatR;

namespace Innvoicer.Application.Features.Companies.Queries;

public sealed record CheckCompanyExistQuery(long CompanyId, bool ThrowEx = true) : IRequest<bool>;

public sealed class CheckCompanyExistQueryHandler(IQueryRepository<Company> repository)
    : IRequestHandler<CheckCompanyExistQuery, bool>
{
    public async Task<bool> Handle(CheckCompanyExistQuery request, CancellationToken cancellationToken)
    {
        var company = await repository.GetAsync(x => x.Id == request.CompanyId, cancellationToken: cancellationToken);
        if (company == null && request.ThrowEx)
            throw new ObjectNotFoundException(nameof(Company), nameof(Company.Id), request.CompanyId);

        return company != null;
    }
}