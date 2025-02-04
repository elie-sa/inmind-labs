namespace Lab1.Services.User;

public interface IUserService
{
    List<Models.User> GetAllUsers(string? searchEntry);
    Models.User GetUserById(int id);
    void EditUser(Models.User user);
    Task UploadPicture(IFormFile file);
    void DeleteUser(int id);
}