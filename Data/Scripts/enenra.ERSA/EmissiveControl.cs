using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRageMath;
using Sandbox.Common.ObjectBuilders;

namespace enenra.EmissiveControl
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

        private string EMISSIVE_MATERIAL_NAME = "Emissive";
        private Color GREEN = new Color(0, 255, 0);
        private Color BLUE = new Color(0, 255, 255, 255);
        private Color RED = new Color(255, 0, 0);


        bool IsWorking
        {
            get
            {
                return m_block.IsWorking;
            }
        }

        bool IsProducing
        {
            get
            {
                return m_block.IsProducing;
            }
        }

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

            if (IsWorking)
            {
                if (IsProducing)
                    m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, BLUE, 1f);

                else
                    m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, GREEN, 1f);
            }

            else
                m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, RED, 1f);

        }
    }
}