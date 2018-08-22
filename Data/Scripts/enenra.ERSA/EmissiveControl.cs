using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRage.Game.ModAPI;
using VRageMath;

namespace enenra.EmissiveControl
{
	[MyEntityComponentDescriptor(typeof(MyObjectBuilder_CargoContainer), false,
		new string[]
		{
			"ERSA_LG_DataStorage"
		}
	)]
	public class Container : MyGameLogicComponent
	{
		IMyCubeBlock m_block;

		private string EMISSIVE_MATERIAL_NAME = "Emissive";
		private Color GREEN = new Color(0, 240, 0);
		private Color RED = new Color(240, 0, 0);


        bool IsWorking
		{
			get
		{
			return m_block.IsWorking;
		}
		}

			public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false)
		{
			return Container.Entity.GetObjectBuilder(copy);
		}

		public override void Init(MyObjectBuilder_EntityBase objectBuilder)
		{
			NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;

			m_block = (IMyCubeBlock)Entity;
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
				m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, GREEN, 1f);
			else
				m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, RED, 1f);

		}
	}
}