namespace Innvoicer.Domain.Primitives;

public class UserModel
{
    public long UserId { get; set; }
    public required string? Email { get; set; }
}
