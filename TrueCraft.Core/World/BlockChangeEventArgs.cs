﻿using System;
using TrueCraft.Core.Logic;

namespace TrueCraft.Core.World
{
    public class BlockChangeEventArgs : EventArgs
    {
        public BlockChangeEventArgs(GlobalVoxelCoordinates position, BlockDescriptor oldBlock, BlockDescriptor newBlock)
        {
            Position = position;
            OldBlock = oldBlock;
            NewBlock = newBlock;
        }

        public GlobalVoxelCoordinates Position { get; }
        public BlockDescriptor OldBlock { get; }
        public BlockDescriptor NewBlock { get; }
    }
}
