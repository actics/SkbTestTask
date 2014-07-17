namespace SkbLib
{
    internal class PrefixWithFreq
    {
        public PrefixString Prefix;
        public int Freq;

        public PrefixWithFreq(PrefixString prefixString, int freq)
        {
            Prefix = prefixString;
            Freq = freq;
        }
    }
}