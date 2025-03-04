﻿using System;
using System.Linq;
using TrueCraft.Core;
using TrueCraft.Core.Logic;
using TrueCraft.Core.Logic.Blocks;
using TrueCraft.Core.World;
using TrueCraft.TerrainGen.Noise;
using TrueCraft.World;

namespace TrueCraft.TerrainGen.Decorators
{
    public class PlantDecorator : IChunkDecorator
    {
        public void Decorate(int seed, IChunk chunk, IBlockRepository _, IBiomeRepository biomes)
        {
            var noise = new Perlin(seed);
            var chanceNoise = new ClampNoise(noise);
            chanceNoise.MaxValue = 2;
            for (int x = 0; x < Chunk.Width; x++)
            {
                for (int z = 0; z < Chunk.Depth; z++)
                {
                    IBiomeProvider biome = biomes.GetBiome(chunk.GetBiome(x, z));
                    var blockX = MathHelper.ChunkToBlockX(x, chunk.Coordinates.X);
                    var blockZ = MathHelper.ChunkToBlockZ(z, chunk.Coordinates.Z);
                    int height = chunk.GetHeight(x, z);
                    if (noise.Value2D(blockX, blockZ) > 0)
                    {
                        LocalVoxelCoordinates blockLocation = new LocalVoxelCoordinates(x, height, z);
                        LocalVoxelCoordinates plantPosition = new LocalVoxelCoordinates(blockLocation.X, blockLocation.Y + 1, blockLocation.Z);
                        if (chunk.GetBlockID(blockLocation) == biome.SurfaceBlock && plantPosition.Y < Chunk.Height)
                        {
                            var chance = chanceNoise.Value2D(blockX, blockZ);
                            if (chance < 1)
                            {
                                var bushNoise = chanceNoise.Value2D(blockX * 0.7, blockZ * 0.7);
                                var grassNoise = chanceNoise.Value2D(blockX * 0.3, blockZ * 0.3);
                                if (biome.Plants.Contains(PlantSpecies.Deadbush) && bushNoise > 1 && chunk.GetBlockID(blockLocation) == SandBlock.BlockID)
                                {
                                    GenerateDeadBush(chunk, plantPosition);
                                    continue;
                                }
                                
                                if (biome.Plants.Contains(PlantSpecies.TallGrass) && grassNoise > 0.3 && grassNoise < 0.95)
                                {
                                    byte meta = (grassNoise > 0.3 && grassNoise < 0.45 && biome.Plants.Contains(PlantSpecies.Fern)) ? (byte)0x2 : (byte)0x1;
                                    GenerateTallGrass(chunk, plantPosition, meta);
                                    continue;
                                }
                            }
                            else
                            {
                                var flowerTypeNoise = chanceNoise.Value2D(blockX * 1.2, blockZ * 1.2);
                                if (biome.Plants.Contains(PlantSpecies.Rose) && flowerTypeNoise > 0.8 && flowerTypeNoise < 1.5)
                                {
                                    GenerateRose(chunk, plantPosition);
                                }
                                else if (biome.Plants.Contains(PlantSpecies.Dandelion) && flowerTypeNoise <= 0.8)
                                {
                                    GenerateDandelion(chunk, plantPosition);
                                }
                            }
                        }
                    }
                }
            }
        }

        void GenerateRose(IChunk chunk, LocalVoxelCoordinates location)
        {
            chunk.SetBlockID(location, RoseBlock.BlockID);
        }

        void GenerateDandelion(IChunk chunk, LocalVoxelCoordinates location)
        {
            chunk.SetBlockID(location, DandelionBlock.BlockID);
        }

        void GenerateTallGrass(IChunk chunk, LocalVoxelCoordinates location, byte meta)
        {
            chunk.SetBlockID(location, TallGrassBlock.BlockID);
            chunk.SetMetadata(location, meta);
        }

        void GenerateDeadBush(IChunk chunk, LocalVoxelCoordinates location)
        {
            chunk.SetBlockID(location, DeadBushBlock.BlockID);
        }
    }
}
