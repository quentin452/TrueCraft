using System;

namespace TrueCraft.Core.Logic.Blocks
{
    public class DandelionBlock : BlockProvider
    {
        public static readonly byte BlockID = 0x25;
        
        public override byte ID { get { return 0x25; } }
        
        public override double BlastResistance { get { return 0; } }

        public override double Hardness { get { return 0; } }

        public override byte Luminance { get { return 0; } }

        public override bool Opaque { get { return false; } }
        
        public override string GetDisplayName(short metadata)
        {
            return "Flower";
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
                return new BoundingBox(new Vector3(4 / 16.0, 0, 4 / 16.0), new Vector3(12 / 16.0, 8 / 16.0, 12 / 16.0));
            }
        }

        public override bool Flammable { get { return true; } }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(13, 0);
        }
    }
}