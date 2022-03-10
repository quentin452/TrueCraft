using System;
using TrueCraft.Core.Server;
using TrueCraft.Core.World;
using TrueCraft.Core.Logic.Items;
using TrueCraft.Core.Networking;

namespace TrueCraft.Core.Logic.Blocks
{
    public abstract class DoorBlock : BlockProvider
    {
        public abstract short ItemID { get; }

        public override void BlockUpdate(BlockDescriptor descriptor, BlockDescriptor source, IMultiplayerServer server, IDimension world)
        {
            bool upper = ((DoorItem.DoorFlags)descriptor.Metadata & DoorItem.DoorFlags.Upper) == DoorItem.DoorFlags.Upper;
            var other = upper ? Vector3i.Down : Vector3i.Up;
            if (world.GetBlockID(descriptor.Coordinates + other) != ID)
                world.SetBlockID(descriptor.Coordinates, 0);
        }

        protected override ItemStack[] GetDrop(BlockDescriptor descriptor, ItemStack item)
        {
            return new[] { new ItemStack(ItemID) };
        }
    }

    public class WoodenDoorBlock : DoorBlock
    {
        public static readonly byte BlockID = 0x40;

        public override short ItemID { get { return WoodenDoorItem.ItemID; } }

        public override byte ID { get { return 0x40; } }
        
        public override double BlastResistance { get { return 15; } }

        public override double Hardness { get { return 3; } }

        public override byte Luminance { get { return 0; } }

        public override bool Opaque { get { return false; } }
        
        public override string GetDisplayName(short metadata)
        {
            return "Wooden Door";
        }

        public override SoundEffectClass SoundEffect
        {
            get
            {
                return SoundEffectClass.Wood;
            }
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(1, 6);
        }

        public override void BlockLeftClicked(BlockDescriptor descriptor, BlockFace face, IDimension world, IRemoteClient user)
        {
            BlockRightClicked(descriptor, face, world, user);
        }

        public override bool BlockRightClicked(BlockDescriptor descriptor, BlockFace face, IDimension world, IRemoteClient user)
        {
            bool upper = ((DoorItem.DoorFlags)descriptor.Metadata & DoorItem.DoorFlags.Upper) == DoorItem.DoorFlags.Upper;
            var other = upper ? Vector3i.Down : Vector3i.Up;
            var otherMeta = world.GetMetadata(descriptor.Coordinates + other);
            world.SetMetadata(descriptor.Coordinates, (byte)(descriptor.Metadata ^ (byte)DoorItem.DoorFlags.Open));
            world.SetMetadata(descriptor.Coordinates + other, (byte)(otherMeta ^ (byte)DoorItem.DoorFlags.Open));
            return false;
        }
    }

    public class IronDoorBlock : DoorBlock
    {
        public static readonly byte BlockID = 0x47;

        public override short ItemID { get { return IronDoorItem.ItemID; } }
        
        public override byte ID { get { return 0x47; } }
        
        public override double BlastResistance { get { return 25; } }

        public override double Hardness { get { return 5; } }

        public override byte Luminance { get { return 0; } }

        public override bool Opaque { get { return false; } }
        
        public override string GetDisplayName(short metadata)
        {
            return "Iron Door";
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(1, 6);
        }
    }
}