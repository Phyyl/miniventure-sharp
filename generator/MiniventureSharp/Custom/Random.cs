using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class Random
    {
        public static readonly Random global;

        static Random()
        {
            global = new Random();
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
            : this(seedUniquifier() ^ DateTime.Now.Ticks)
        {
        }

        private static long seedUniquifier()
        {

            // L'Ecuyer, "Tables of Linear Congruential Generators of
            // Different Sizes and Good Lattice Structure", 1999
            for (; ; )
            {
                long current = uniquifier;
                long next = current * 181783497276652981L;

                if (compareAndSet(ref uniquifier, current, next))
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
                this.seed = initialScramble(seed);
            }
            else
            {
                // subclass might have overriden setSeed
                setSeed(seed);
            }
        }

        private static long initialScramble(long seed)
        {
            return (seed ^ multiplier) & mask;
        }

        public void setSeed(long seed)
        {
            Interlocked.Exchange(ref this.seed, initialScramble(seed));
            haveNextNextGaussian = false;
        }

        protected int next(int bits)
        {
            long oldseed, nextseed;
            do
            {
                oldseed = seed;
                nextseed = (oldseed * multiplier + addend) & mask;
            } while (!compareAndSet(ref this.seed, oldseed, nextseed));
            return (int)(nextseed >> (48 - bits));
        }

        public void nextBytes(byte[] bytes)
        {
            for (int i = 0, len = bytes.Length; i < len;)
            {
                for (int rnd = nextInt(),
                         n = Math.min(len - i, sizeof(int));
                     n-- > 0; rnd >>= 8)
                {
                    bytes[i++] = (byte)rnd;
                }
            }
        }

        public int nextInt()
        {
            return next(32);
        }

        public int nextInt(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("n must be positive");
            }

            if ((n & -n) == n)  // i.e., n is a power of 2
            {
                return (int)((n * (long)next(31)) >> 31);
            }

            int bits, val;
            do
            {
                bits = next(31);
                val = bits % n;
            } while (bits - val + (n - 1) < 0);
            return val;
        }

        public long nextLong()
        {
            // it's okay that the bottom word remains signed.
            return ((long)(next(32)) << 32) + next(32);
        }

        public bool nextBoolean()
        {
            return next(1) != 0;
        }

        public float nextFloat()
        {
            return next(24) / ((float)(1 << 24));
        }

        public double nextDouble()
        {
            return (((long)(next(26)) << 27) + next(27))
                / (double)(1L << 53);
        }

        private double nextNextGaussian;
        private bool haveNextNextGaussian = false;

        public double nextGaussian()
        {
            // See Knuth, ACP, Section 3.4.1 Algorithm C.
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
                    v1 = 2 * nextDouble() - 1; // between -1 and 1
                    v2 = 2 * nextDouble() - 1; // between -1 and 1
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1 || s == 0);
                double multiplier = Math.sqrt(-2 * Math.log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }

        private static bool compareAndSet(ref long value, long expect, long update)
        {
            long rc = Interlocked.CompareExchange(ref value, update, expect);

            return rc == expect;
        }
    }
}
