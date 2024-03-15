using Game.Runtime.CustomStructs;
using Game.Runtime.Views;

namespace Game.Runtime.Components
{
    public struct Unit
    {
        public UnitView View;
        public EcsVector3 Velocity;
    }
}