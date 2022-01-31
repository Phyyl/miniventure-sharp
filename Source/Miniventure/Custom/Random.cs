using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class Random
    {
        public static readonly Random Global;

        static Random()
        {
            Global = new Random();
        }

        /**
         * The internal state associated with this pseudorandom number generator.
         * (The specs for the methods in this class describe the ongoing
         * computation of this value.)
         */
        private long seed;

        private static readonly long multiplier = 0x5DEECE66DL;
        private static readonly long addend = 0xBL;
        private static readonly long mask = (1L << 48) - 1;

        /**
         * Creates a new random number generator. This constructor sets
         * the seed of the random number generator to a value very likely
         * to be distinct from any other invocation of this constructor.
         */
        public Random()
            : this(SeedUniquifier() ^ DateTime.Now.Ticks)
        {
        }

        private static long SeedUniquifier()
        {



            for (; ; )
            {
                long current = uniquifier;
                long next = current * 181783497276652981L;

                if (CompareAndSet(ref uniquifier, current, next))
                {
                    return next;
                }
            }
        }

        private static long uniquifier = 8682522807148012L;

        public Random(long seed)
        {
            if (GetType() == typeof(Random))
            {
                this.seed = InitialScramble(seed);
            }
            else
            {

                SetSeed(seed);
            }
        }

        private static long InitialScramble(long seed)
        {
            return (seed ^ multiplier) & mask;
        }

        public void SetSeed(long seed)
        {
            Interlocked.Exchange(ref this.seed, InitialScramble(seed));
            haveNextNextGaussian = false;
        }

        protected int Next(int bits)
        {
            long oldseed, nextseed;
            do
            {
                oldseed = seed;
                nextseed = ((oldseed * multiplier) + addend) & mask;
            } while (!CompareAndSet(ref seed, oldseed, nextseed));
            return (int)(nextseed >> (48 - bits));
        }

        public void NextBytes(byte[] bytes)
        {
            for (int i = 0, len = bytes.Length; i < len;)
            {
                for (int rnd = NextInt(),
                         n = Math.Min(len - i, sizeof(int));
                     n-- > 0; rnd >>= 8)
                {
                    bytes[i++] = (byte)rnd;
                }
            }
        }

        public int NextInt()
        {
            return Next(32);
        }

        public int NextInt(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("n must be positive");
            }

            if ((n & -n) == n)
            {
                return (int)((n * (long)Next(31)) >> 31);
            }

            int bits, val;
            do
            {
                bits = Next(31);
                val = bits % n;
            } while (bits - val + (n - 1) < 0);
            return val;
        }

        public long NextLong()
        {

            return ((long)Next(32) << 32) + Next(32);
        }

        public bool NextBoolean()
        {
            return Next(1) != 0;
        }

        public float NextFloat()
        {
            return Next(24) / ((float)(1 << 24));
        }

        public double NextDouble()
        {
            return (((long)Next(26) << 27) + Next(27))
                / (double)(1L << 53);
        }

        private double nextNextGaussian;
        private bool haveNextNextGaussian = false;

        public double NextGaussian()
        {

            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                do
                {
                    v1 = (2 * NextDouble()) - 1;
                    v2 = (2 * NextDouble()) - 1;
                    s = (v1 * v1) + (v2 * v2);
                } while (s >= 1 || s == 0);
                double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }

        private static bool CompareAndSet(ref long value, long expect, long update)
        {
            long rc = Interlocked.CompareExchange(ref value, update, expect);

            return rc == expect;
        }
    }
}
