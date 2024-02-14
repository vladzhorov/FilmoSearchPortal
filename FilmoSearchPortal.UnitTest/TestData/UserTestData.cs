using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class UserTestData
    {
        public static (List<UserEntity>, List<User>) TestGetAllUsers()
        {
            var userEntity1 = new UserEntity { Id = Guid.NewGuid(), Username = "User1" };
            var userEntity2 = new UserEntity { Id = Guid.NewGuid(), Username = "User2" };

            var userModel1 = new User { Id = userEntity1.Id, Username = userEntity1.Username };
            var userModel2 = new User { Id = userEntity2.Id, Username = userEntity2.Username };

            var usersEntities = new List<UserEntity> { userEntity1, userEntity2 };
            var userModels = new List<User> { userModel1, userModel2 };

            return (usersEntities, userModels);
        }

        public static (User, UserEntity) CreateUser()
        {
            var userId = Guid.NewGuid();
            var userEntity = new UserEntity { Id = userId, Username = "TestUser" };
            var userModel = new User { Id = userId, Username = userEntity.Username };

            return (userModel, userEntity);
        }

        public static (Guid, UserEntity, User) CreateExistingUser()
        {
            var userId = Guid.NewGuid();
            var existingUserEntity = new UserEntity { Id = userId, Username = "ExistingUser" };
            var updatedUserModel = new User { Id = existingUserEntity.Id, Username = "UpdatedUser" };

            return (userId, existingUserEntity, updatedUserModel);
        }
    }
}
