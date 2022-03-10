using System;
using TrueCraft.Core.Logic.Items;
using TrueCraft.Core.Networking;
using TrueCraft.Core.Server;
using TrueCraft.Core.World;

namespace TrueCraft.Core.Logic.Blocks
{
    public class CropsBlock : BlockProvider
    {
        public static readonly byte BlockID = 0x3B;
        
        public override byte ID { get { return 0x3B; } }
        
        public override double BlastResistance { get { return 0; } }

        public override double Hardness { get { return 0; } }

        public override byte Luminance { get { return 0; } }

        public override bool Opaque { get { return false; } }
        
        public override string GetDisplayName(short metadata)
        {
            return "Crops";
        }

        public override SoundEffectClass SoundEffect
        {
            get
            {
                return SoundEffectClass.Grass;
            }
        }

        public override BoundingBox? BoundingBox { get { return null; } }

        public override BoundingBox? InteractiveBoundingBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero, new Vector3(1, 3 / 16.0, 1));
            }
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(8, 5);
        }

        protected override ItemStack[] GetDrop(BlockDescriptor descriptor, ItemStack item)
        {
            if (descriptor.Metadata >= 7)
                return new[] { new ItemStack(WheatItem.ItemID), new ItemStack(SeedsItem.ItemID, (sbyte)MathHelper.Random.Next(3)) };
            else
                return new[] { new ItemStack(SeedsItem.ItemID) };
        }

        private void GrowBlock(IMultiplayerServer server, IDimension world, GlobalVoxelCoordinates coords)
        {
            if (world.GetBlockID(coords) != BlockID)
                return;
            var meta = world.GetMetadata(coords);
            meta++;
            world.SetMetadata(coords, meta);
            if (meta < 7)
            {
                var chunk = world.FindChunk(coords);
                server.Scheduler.ScheduleEvent("crops",
                    chunk, TimeSpan.FromSeconds(MathHelper.Random.Next(30, 60)),
                   (_server) => GrowBlock(_server, world, coords));
            }
        }

        public override void BlockUpdate(BlockDescriptor descriptor, BlockDescriptor source, IMultiplayerServer server, IDimension world)
        {
            if (world.GetBlockID(descriptor.Coordinates + Vector3i.Down) != FarmlandBlock.BlockID)
            {
                GenerateDropEntity(descriptor, world, server, ItemStack.EmptyStack);
                world.SetBlockID(descriptor.Coordinates, 0);
            }
        }

        public override void BlockPlaced(BlockDescriptor descriptor, BlockFace face, IDimension world, IRemoteClient user)
        {
            var chunk = world.FindChunk(descriptor.Coordinates);
            user.Server.Scheduler.ScheduleEvent("crops", chunk,
                TimeSpan.FromSeconds(MathHelper.Random.Next(30, 60)),
                (server) => GrowBlock(server, world, descriptor.Coordinates + MathHelper.BlockFaceToCoordinates(face)));
        }

        public override void BlockLoadedFromChunk(GlobalVoxelCoordinates coords, IMultiplayerServer server, IDimension world)
        {
            var chunk = world.FindChunk(coords);
            server.Scheduler.ScheduleEvent("crops", chunk,
                TimeSpan.FromSeconds(MathHelper.Random.Next(30, 60)),
                (s) => GrowBlock(s, world, coords));
        }
    }
}
