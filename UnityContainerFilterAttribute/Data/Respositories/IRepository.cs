using UnityContainerFilterAttribute.Domain;

namespace UnityContainerFilterAttribute.Data.Respositories
{
    public interface IRepository
    {
        Sheep GetId(int id);
    }
}