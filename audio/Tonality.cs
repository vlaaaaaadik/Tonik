using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio
{
    class Tonality
    {
        public string name { get; private set; }

        public Note[] notes { get; private set; }

        public Tonality(Note[] notes)
        {
            this.notes = notes;
        }

        public Tonality(string nameOfTonality, params string[] names)
        {
            name = nameOfTonality;

            notes = new Note[7];

            for(int i = 0; i < 7; i++)
            {
                notes[i] = new Note(names[i]);
            }
        }

        public Tonality(HarmonicType type, bool[] mask, int shift = 0)
        {
            string[] nameOfNotes = new string[] { "C", "Cs", "D", "Ds", "E", "F",
                                                 "Fs", "G", "Gs", "A", "As", "B" };


            if (type == HarmonicType.Major)
                name = nameOfNotes[shift];
            else 
                name = nameOfNotes[shift] + "m";

            notes = new Note[7];

            for(int i = 0, j = shift, notesFound = 0; i < mask.Length ; i++, j++)
            {
                if (j + 1 > mask.Length) j = 0;
                if(mask[i])
                {
                    notes[notesFound++] = new Note(nameOfNotes[j]);
                }
            }

        }

        public enum HarmonicType
        {
            Major, Minor
        }
    }

}
