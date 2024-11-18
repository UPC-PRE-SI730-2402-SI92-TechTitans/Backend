using Domain.Security.Model.Entities;
using Domain.Shared;

namespace Domain.Security.Repositories;

public interface IUserRepository : IBaseRepository<User>
{ 
    Task<User> FindByusermail(String email);

}