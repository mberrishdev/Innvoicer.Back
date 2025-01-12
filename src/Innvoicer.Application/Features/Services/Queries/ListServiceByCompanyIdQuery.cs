using Common.Repository.Repository;
using Innvoicer.Application.Features.Services.Queries.Models;
using Innvoicer.Domain.Entities.Services;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Services.Queries;

public sealed record ListServiceByCompanyIdQuery(long CompanyId) : IRequest<List<ServiceModel>>;

public class ListServiceByCompanyIdQueryHandler
    (IQueryRepository<Service> repository) : IRequestHandler<ListServiceByCompanyIdQuery, List<ServiceModel>>
{
    public async Task<List<ServiceModel>> Handle(ListServiceByCompanyIdQuery request,
        CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Service>, IIncludableQueryable<Service, object>>>
        {
        };

        var services = await repository.GetListAsync(rp, x => x.CompanyId == request.CompanyId,
            cancellationToken: cancellationToken);


        return services.Select(s => new ServiceModel(s)).ToList();
    }
}