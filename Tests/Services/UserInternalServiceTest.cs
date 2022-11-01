using Entities.Entities;

namespace Tests.Services;
public class UserInternalServiceTest
{
    //Arrange Global
    static UserInternal user = new UserInternal
    {
        Id = 1,
        Email = "email@gmail.com",
        Name = "Name",
        Username = "Username",
        Password = "Password",
        ConfirmPassword = "Password"
    };

    private readonly Mock<IRUserInternal>_rUserInternal = new();
    private UserInternalService GetUserInternalService()
    {
        return new UserInternalService(_rUserInternal.Object);
    }

    [Fact]
    public void Create_ValidateUserName()
    {
        //Act
        var service = GetUserInternalService();
        var userResponse = service.Create(user,true).Result;

        //Assert
        Assert.False(userResponse.HasError);
    }
    [Fact]
    public void Login_ValidateUserName()
    {
        //Act
        var service = GetUserInternalService();
        var userResponse = service.Login(user, true).Result;

        //Action
        Assert.False(userResponse.HasError);
    }

    [Fact]
    public void ValidatePasswordConfirm_ValidatePassword()
    {
        //Act
        var service = GetUserInternalService();
        var response = service.ValidatePasswordConfirm(user.Password, true).Result;

        //Action
        Assert.True(response);
    }


}
