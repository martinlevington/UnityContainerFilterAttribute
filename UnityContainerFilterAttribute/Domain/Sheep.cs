namespace UnityContainerFilterAttribute.Domain
{
    public sealed class Sheep : Entity, ISheep
    {

        public string Name { get; set; }

        private bool _readyToShear;


        public Sheep(long id)
        {
            Id = id;
        }

        public bool Shear()
        {
            if (!_readyToShear)
            {
                return false;

            }

            _readyToShear = false;

            return true;
        }

    }
}