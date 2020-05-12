using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio
{
    class TonalityCoincidence
    {
        public Tonality[] tonalities;

        public Dictionary<Tonality, float> coincidence { get; private set; }

        public TonalityCoincidence()
        {
            coincidence = new Dictionary<Tonality, float>();

            bool[] major = new bool[12] {true, false, true, false, true, true,
                                     false, true, false, true, false, true };

            bool[] minor = new bool[12] {true, false, true, true, false, true,
                                      false, true, true, false, true, false};

            tonalities = new Tonality[24];

            for (int i = 0; i < tonalities.Length / 2; i++)
            {
                tonalities[i * 2] = new Tonality(Tonality.HarmonicType.Major, major, i);
                tonalities[(i * 2) + 1] = new Tonality(Tonality.HarmonicType.Minor, minor, i);
            }
        }

        public void calculateCoincidence(Tonality tonality)
        {
            int coinc;

            for(int i = 0; i < tonalities.Length; i++)
            {
                coinc = 0;

                for(int j = 0; j < tonality.notes.Length; j++)
                {
                    if (tonality.notes.Contains(tonalities[i].notes[j]))
                        coinc++;
                }

                coincidence.Add(tonalities[i], (float)coinc / 7f);
            }
        }

    }
}
