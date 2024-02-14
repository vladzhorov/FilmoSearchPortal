using System.Text.Json.Serialization;

namespace FilmoSearchPortal.DAL.Entites
{
    public class EnumsEntity
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ActorStatus
        {
            Active,
            Retired
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Genre
        {
            Animation,
            Western,
            Eastern,
            Thriller,
            Suspense,
            Adventure,
            Horrors,
            Comedy,
            Parody,
            Drama,
            Melodrama,
            Tragicomedia,
            Mystique,
            Fantasy,
            Cyberpunk,
            Biography,
            Documentary,
            Erotica,
            Sport

        }
    }
}
