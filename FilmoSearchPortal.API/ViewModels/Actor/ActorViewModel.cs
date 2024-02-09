using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.API.ViewModels.Actor
{
    public class ActorViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ActorStatus ActorStatus { get; set; }
    }
}
