using System;
using TrueCraft.Core.Logic.Blocks;
using TrueCraft.Core.Networking;
using TrueCraft.Core.Server;
using TrueCraft.Core.World;

namespace TrueCraft.Core.Logic.Items
{
    public class SugarCanesItem : ItemProvider
    {
        public static readonly short ItemID = 0x152;

        public override short ID { get { return 0x152; } }

        public override Tuple<int, int> GetIconTexture(byte metadata)
        {
            return new Tuple<int, int>(11, 1);
        }

        public override string GetDisplayName(short metadata)
        {
            return "Sugar Canes";
        }

        public override void ItemUsedOnBlock(GlobalVoxelCoordinates coordinates, ItemStack item, BlockFace face, IDimension dimension   , IRemoteClient user)
        {
            ServerOnly.Assert();

            coordinates += MathHelper.BlockFaceToCoordinates(face);
            if (SugarcaneBlock.ValidPlacement(new BlockDescriptor { Coordinates = coordinates }, dimension))
            {
                dimension.SetBlockID(coordinates, SugarcaneBlock.BlockID);
                item.Count--;
                user.Hotbar[user.SelectedSlot].Item = item;
                dimension.BlockRepository.GetBlockProvider(SugarcaneBlock.BlockID).BlockPlaced(
                    new BlockDescriptor { Coordinates = coordinates }, face, dimension, user);
            }
        }
    }
}