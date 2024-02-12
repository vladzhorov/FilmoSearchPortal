using FilmoSearchPortal.API.ViewModels.User;
using FilmoSearchPortal.BLL.Models;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class UserControllerTestHelper
    {
        public static (List<User> users, List<UserViewModel> userViewModels) TestGetAll()
        {
            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            var users = new List<User>
            {
                new User { Id = userId1, Username = "User 1" },
                new User { Id = userId2, Username = "User 2" }
            };

            var userViewModels = new List<UserViewModel>
            {
                new UserViewModel { Id = userId1, Username = "User 1" },
                new UserViewModel { Id = userId2, Username = "User 2" }
            };

            return (users, userViewModels);
        }

        public static (Guid userId, User user, UserViewModel userViewModel) TestGetById()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "Test User" };
            var userViewModel = new UserViewModel { Id = userId, Username = "Test User" };
            return (userId, user, userViewModel);
        }

        public static (CreateUserViewModel viewModel, User user) TestCreate()
        {
            var userId = Guid.NewGuid();
            var viewModel = new CreateUserViewModel { Username = "Test User" };
            var user = new User { Id = userId, Username = viewModel.Username };
            return (viewModel, user);
        }

        public static (Guid userId, UpdateUserViewModel viewModel, User user) TestUpdate()
        {
            var userId = Guid.NewGuid();
            var viewModel = new UpdateUserViewModel { Username = "Updated User" };
            var user = new User { Id = userId, Username = viewModel.Username };
            return (userId, viewModel, user);
        }

        public static Guid TestDelete()
        {
            return Guid.NewGuid();
        }
    }
}
