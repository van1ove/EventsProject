using Events.Domain.Models.User;

namespace Events.Business.UseCases.UserUseCases.GetAllUsers;

public interface IGetAllUsersUseCase
{
    public Task<IEnumerable<UserModel>> GetAllUsers();
}