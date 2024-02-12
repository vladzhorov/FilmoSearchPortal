using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Entites
{
    public class ActorEntity : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ActorStatus ActorStatus { get; set; }

    }
}

