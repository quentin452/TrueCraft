using System;
using TrueCraft.Core.Logic.Items;
using TrueCraft.Core.World;

namespace TrueCraft.Core.Logic.Blocks
{
    public class SnowBlock : BlockProvider
    {
        public static readonly byte BlockID = 0x50;
        
        public override byte ID { get { return 0x50; } }
        
        public override double BlastResistance { get { return 1; } }

        public override double Hardness { get { return 0.2; } }

        public override byte Luminance { get { return 0; } }
        
        public override string GetDisplayName(short metadata)
        {
            return "Snow Block";
        }

        public override SoundEffectClass SoundEffect
        {
            get
            {
                return SoundEffectClass.Snow;
            }
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(2, 4);
        }
    }

    public class SnowfallBlock : BlockProvider
    {
        public static readonly byte BlockID = 0x4E;

        public override byte ID { get { return 0x4E; } }

        public override double BlastResistance { get { return 0.5; } }

        public override double Hardness { get { return 0.6; } }

        public override byte Luminance { get { return 0; } }

        public override bool RenderOpaque { get { return true; } }

        public override bool Opaque { get { return false; } }

        public override string GetDisplayName(short metadata)
        {
            return "Snow";
        }

        public override BoundingBox? BoundingBox { get { return null; } }

        public override SoundEffectClass SoundEffect
        {
            get
            {
                return SoundEffectClass.Snow;
            }
        }

        public override BoundingBox? InteractiveBoundingBox
        {
            get
            {
                // TODO: This is metadata-aware
                return new BoundingBox(Vector3.Zero, new Vector3(1, 1 / 16.0, 1));
            }
        }

        public override Vector3i GetSupportDirection(BlockDescriptor descriptor)
        {
            return Vector3i.Down;
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(2, 4);
        }

        public override ToolType EffectiveTools
        {
            get
            {
                return ToolType.Shovel;
            }
        }

        protected override ItemStack[] GetDrop(BlockDescriptor descriptor, ItemStack item)
        {
            return new[] { new ItemStack(SnowballItem.ItemID) };
        }
    }
}
