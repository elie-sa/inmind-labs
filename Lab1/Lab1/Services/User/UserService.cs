namespace Lab1.Services.User;

public class UserService : IUserService
{
    private static List<Models.User> _users = new List<Models.User>()
    {
        new Models.User{ Id = 1, Name = "Elie Awad", Email = "e.sawmaawad@gmail.com" },
        new Models.User{ Id = 2, Name = "Michael Keaton", Email = "michaelkeaton@gmail.com" },
        new Models.User{ Id = 3, Name = "Emma Stone", Email = "emmastone@gmail.com" }
    };
    
    public List<Models.User> GetAllUsers(string? searchEntry)
    {
        // made the searchEntry ignore case sensitivity
        return string.IsNullOrEmpty(searchEntry) ? _users : 
            _users.Where(u => u.Name.Contains(searchEntry, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }

    public Models.User GetUserById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new KeyNotFoundException("The user id provided is invalid");   
        }

        return user;
    }

    public void EditUser(Models.User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User object must be provided");
        }

        if (user.Id == 0)
        {
            throw new ArgumentException("A valid user id must be provided", nameof(user.Id));
        }

        var toChange = _users.FirstOrDefault(u => u.Id == user.Id);
        if (toChange == null)
        {
            throw new KeyNotFoundException("The user id provided is invalid");
        }

        toChange.Name = user.Name;
        toChange.Email = user.Email;
    }

    public async Task UploadPicture(IFormFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file), "File must be provided");
        }

        var filePath = Path.Combine("wwwroot", file.FileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    public void DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new KeyNotFoundException("The user id provided is invalid");

        _users.Remove(user);
    }
}