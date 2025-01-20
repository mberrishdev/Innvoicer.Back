using Common.Repository.Repository;
using Innvoicer.Application.Exceptions;
using Innvoicer.Application.Helpers;
using Innvoicer.Domain.Entities.Users;
using Innvoicer.Domain.Entities.Users.Commands;
using MediatR;

namespace Innvoicer.Application.Features.Users.Commands;

public class UpdateUserPasswordCommandHandler(IRepository<User> repository) : IRequestHandler<UpdateUserPasswordCommand>
{
    public async Task Handle(UpdateUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.GetForUpdateAsync(x => x.Id == command.Id, cancellationToken: cancellationToken)
                   ?? throw new ObjectNotFoundException(nameof(User), nameof(User.Id), command.Id);

        if (user.Password != HashHelper.Hash(command.CurrentPassword))
            throw new InvalidCredentialsException("Current password is not correct");

        command.NewPasswordHash = HashHelper.Hash(command.NewPassword);

        user.UpdatePassword(command);

        await repository.UpdateAsync(user, cancellationToken);
    }
}