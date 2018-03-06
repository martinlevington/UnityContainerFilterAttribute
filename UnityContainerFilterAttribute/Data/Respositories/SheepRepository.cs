using System;
using UnityContainerFilterAttribute.Domain;

namespace UnityContainerFilterAttribute.Data.Respositories
{
    public class SheepRepository : IRepository
    {

        public Sheep GetId(int id)
        {
           if(id == 0) { throw new ArgumentException("id cannot be 0");}
            
            return new Sheep(id);
        }

    }
}