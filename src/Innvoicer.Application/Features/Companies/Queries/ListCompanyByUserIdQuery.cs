using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Repository.Repository;
using Innvoicer.Application.Features.Companies.Queries.Models;
using Innvoicer.Domain.Companies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Innvoicer.Application.Features.Companies.Queries;

public sealed record ListCompanyByUserIdQuery(long UserId) : IRequest<List<CompanyModel>>;

public class ListCompanyByUserIdQueryHandler
    (IQueryRepository<Company> repository) : IRequestHandler<ListCompanyByUserIdQuery, List<CompanyModel>>
{
    public async Task<List<CompanyModel>> Handle(ListCompanyByUserIdQuery request, CancellationToken cancellationToken)
    {
        var rp = new List<Func<IQueryable<Company>, IIncludableQueryable<Company, object>>>
        {
            x => x.Include(company => company.CompanyBankAccounts)
        };

        var companies = await repository.GetListAsync(rp, x => x.UserId == request.UserId,
            cancellationToken: cancellationToken);


        return companies.Select(c => new CompanyModel(c)).ToList();
    }
}