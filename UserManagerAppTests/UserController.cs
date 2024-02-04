using Moq;
using UserManagerApp.Interfaces;
using UserManagerApp.Models;
using UserManagerApp.Helpers;
using Assert = Xunit.Assert;

using Microsoft.AspNetCore.Mvc;
using UserManagerApp.Dto;

namespace UserManagerAppTests
{
    [TestClass]
    public class UserController
    {
        [TestMethod]
        public void GetUser()
        {
            // Arrange mocked data
            var userRepository = new Mock<IUserRepository>();

            var user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                Culture = "en-US",
                FullName = "Test",
                Password = "Password",
                UserName = "Test",
                Language = "EN",
                MobileNumber = "09090",
            };

            userRepository.Setup(_ => _.GetUser(CustomConstants.ID, "1")).Returns(user);

            var userController = new UserManagerApp.Controllers.UserController(userRepository.Object);

            // Check for user
            var getResponse1 = userController.GetUser(1);
            var okResult = getResponse1 as OkObjectResult;
            User? fetchedUser = null;
            if (okResult != null)
            {
                fetchedUser = okResult.Value as User;
            }

            var getResponse2 = userController.GetUser(2);
            var notFoundResult = getResponse2 as NotFoundResult;

            // Check if status is correct
            Assert.True(okResult != null && okResult.StatusCode == 200);
            Assert.True(notFoundResult != null && notFoundResult.StatusCode == 404);
            Assert.True(fetchedUser != null && fetchedUser.Id == 1);
        }

        //Delegate for ref user callback
        delegate void SubmitCallback(ref User user);

        [TestMethod]
        public void InsertUser()
        {
            // Arrange mocked data
            var userRepository = new Mock<IUserRepository>();

            var userDto = new UserPostDto
            {
                Email = "test@test.com",
                Culture = "en-US",
                FullName = "Test",
                Password = "Password",
                UserName = "Test",
                Language = "EN",
                MobileNumber = "09090",
            };


            userRepository
                .Setup(_ => _.AddUser(ref It.Ref<User>.IsAny))
                .Callback(new SubmitCallback((ref User user) => user.Id = 1))
                .Returns(true);

            var userController = new UserManagerApp.Controllers.UserController(userRepository.Object);

            // Insert new user
            var insertResponse = userController.InsertUser(userDto);
            var insertOkResult = insertResponse as OkObjectResult;
            // Expecting userID
            int? userId = null;
            if (insertOkResult != null)
            {
                userId = insertOkResult.Value as int?;
            }

            // Check if status is correct
            Assert.True(insertOkResult != null && insertOkResult.StatusCode == 200);
            Assert.True(userId == 1);
        }

        [TestMethod]
        public void UpdateUser()
        {
            // Arrange mocked data
            var userRepository = new Mock<IUserRepository>();

            var user = new User
            {
                Id = 1,
                Email = "test@test.com",
                Culture = "en-US",
                FullName = "Test",
                Password = "Password",
                UserName = "Test",
                Language = "EN",
                MobileNumber = "09090",
            };

            var updatedUserDto = new UserPostDto
            {
                Email = "udpate@email.com",
                Culture = "en-US",
                FullName = "Still Test",
                Password = "Password New",
                UserName = "Im not a test",
                Language = "EN",
                MobileNumber = "09090",
            };

            var updatedUserModel = new User
            {
                Email = "udpate@email.com",
                Culture = "en-US",
                FullName = "Still Test",
                Password = "Password New",
                UserName = "Im not a test",
                Language = "EN",
                MobileNumber = "09090",
            };

            userRepository.Setup(_ => _.GetUser(CustomConstants.ID, "1")).Returns(user);
            userRepository
                .Setup(_ => _.UpdateUser(It.IsAny<User>()))
                .Callback<User>((u) => {
                    user.UserName = u.UserName;
                    user.Email = u.Email;
                    user.FullName = u.FullName;
                    user.Password = u.Password;
                    user.Language = u.Language;
                    user.MobileNumber = u.MobileNumber;
                    user.Culture = u.Culture;
                })
                .Returns(true);

            var userController = new UserManagerApp.Controllers.UserController(userRepository.Object);

            // Updating user should work
            var updateOkResponse = userController.UpdateUser(1, updatedUserDto);
            var okResult = updateOkResponse as NoContentResult;

            // Updating user should fail to due to not found
            var updateFailResponse = userController.UpdateUser(2, updatedUserDto);
            var notFoundResult = updateFailResponse as NotFoundResult;

            // Check if status is correct
            Assert.True(okResult != null && okResult.StatusCode == 204);
            Assert.True(notFoundResult != null && notFoundResult.StatusCode == 404);
            // Check if user was changed
            Assert.True(user.UserName == "Im not a test");
        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange mocked data
            var userRepository = new Mock<IUserRepository>();

            var user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                Culture = "en-US",
                FullName = "Test",
                Password = "Password",
                UserName = "Test",
                Language = "EN",
                MobileNumber = "09090",
            };

            userRepository.Setup(_ => _.GetUser(CustomConstants.ID, "1")).Returns(user);
            userRepository.Setup(_ => _.DeleteUser(user)).Returns(true);

            var userController = new UserManagerApp.Controllers.UserController(userRepository.Object);

            // Delete user should work
            var deleteOkResponse = userController.DeleteUser(1);
            var okResult = deleteOkResponse as NoContentResult;

            // Delete user should fail due to not found
            var deleteFailResponse = userController.DeleteUser(2);
            var failResult = deleteFailResponse as NotFoundResult;

            // Check if status is correct
            Assert.True(okResult != null && okResult.StatusCode == 204);
            Assert.True(failResult != null && failResult.StatusCode == 404);
        }


        [TestMethod]
        public void ValidateUser()
        {
            // Arrange mocked data
            var userRepository = new Mock<IUserRepository>();

            var userValidation = new UserValidationDto()
            {
                Password = "Password",
                UserName = "Test"
            };

            var userValidationFail = new UserValidationDto()
            {
                Password = "Wrong",
                UserName = "Test"
            };

            var user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                Culture = "en-US",
                FullName = "Test",
                Password = "Password",
                UserName = "Test",
                Language = "EN",
                MobileNumber = "09090",
            };

            userRepository.Setup(_ => _.GetUser(CustomConstants.USERNAME, user.UserName)).Returns(user);
            userRepository.Setup(_ => _.ValidateUser(userValidation.Password, user.Id)).Returns(true);

            var userController = new UserManagerApp.Controllers.UserController(userRepository.Object);

            // Validate user success
            var getResponseSuccess = userController.ValidateUser(userValidation);
            var okResult = getResponseSuccess as OkObjectResult;

            // Validate for user fail
            var getResponseFail = userController.ValidateUser(userValidationFail);
            var failResult = getResponseFail as UnauthorizedObjectResult;

            // Check if status is correct
            Assert.True(okResult != null && okResult.StatusCode == 200);
            Assert.True(failResult != null && failResult.StatusCode == 401);
        }
    }
}
