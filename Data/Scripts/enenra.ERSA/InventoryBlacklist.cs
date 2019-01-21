using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRageMath;
using Sandbox.Common.ObjectBuilders;

namespace enenra.InventoryBlacklist
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Assembler), false,
        new string[]
        {
            "ERSA_LG_ResearchLab"
        }
    )]
    public class Container : MyGameLogicComponent
    {
        Sandbox.ModAPI.IMyAssembler m_block;

        public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false)
        {
            return Container.Entity.GetObjectBuilder(copy);
        }

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;

            m_block = (Sandbox.ModAPI.IMyAssembler)Entity;
        }

        public override void Close()
        {
            NeedsUpdate |= MyEntityUpdateEnum.NONE;
            m_block = null;
        }

        public override void UpdateAfterSimulation10()
        {
            if (MyAPIGateway.Session == null)
                return;

            if (MyAPIGateway.Utilities.IsDedicated && MyAPIGateway.Multiplayer.IsServer)
                return;

        }
    }
}