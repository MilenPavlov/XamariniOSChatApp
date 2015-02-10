using System;
using System.Threading.Tasks;
using NUnit.Framework;
using XamChat.Core;
using XamChat.Core.Abstract;
using XamChat.Core.ViewModels;

namespace XamChat.Tests
{
	public class LoginViewModelTests
	{
	    private LoginViewModel loginViewModel;
	    private ISettings settings;

	    [SetUp]
	    public void SetUp()
	    {
	        Test.SetUp();

	        settings = ServiceContainer.Resolve<ISettings>();
            loginViewModel = new LoginViewModel();
	    }

	    [Test]
	    public async Task LogInSuccessffuly()
	    {
	        loginViewModel.Username = "testuser";
	        loginViewModel.Password = "password";
	        await loginViewModel.Login();

            Assert.That(settings.User, Is.Not.Null);
	    }

	    [Test, ExpectedException(typeof(AggregateException))]
	    public async Task LoginWithNoUsernameOrPassword()
	    {
	        await loginViewModel.Login();
	    }
	}
}

