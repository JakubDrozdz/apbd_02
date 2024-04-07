using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTest
{
    private string _firstName = "John";
    private string _lastName = "Kowalski";
    private string _email = "kowalski@wp.pl";
    private int _clientId = 1;
    private DateTime _dateOfBirth = new DateTime(1990, 01,01);
    private UserService _userService = new UserService();
    [Fact]
    public void AddUser_Should_Return_False_When_FistName_Is_Missing()
    {
        //when
        var resultNull = _userService.AddUser(null, _lastName, _email, _dateOfBirth, _clientId);
        var resultEmpty = _userService.AddUser("", _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.False(resultNull);
        Assert.False(resultEmpty);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Missing()
    {
        //when
        var resultNull = _userService.AddUser(_firstName, null, _email, _dateOfBirth, _clientId);
        var resultEmpty = _userService.AddUser(_firstName, "", _email, _dateOfBirth, _clientId);
        //then
        Assert.False(resultNull);
        Assert.False(resultEmpty);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Dont_Contains_Dot_And_At()
    {
        //given
        _email = "kowalski";
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Lower_Than_21()
    {
        //given
        _dateOfBirth = new DateTime(2010, 01, 01);
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_21_Later_This_Year()
    {
        //given
        _dateOfBirth = new DateTime(2003, 07, 01);
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_User_Has_Credit_Limit_Below_500()
    {
        //given
        _clientId = 1; 
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_User_Is_Very_Important_Client()
    {
        //given
        _clientId = 2; 
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_User_Is_Important_Client_And_Credit_Limit_Is_Lower_Than_500()
    {
        //given
        _clientId = 3; 
        //when
        var result = _userService.AddUser(_firstName, _lastName, _email, _dateOfBirth, _clientId);
        //then
        Assert.True(result);
    }
}