using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio
{
    class Note : IComparable
    {
        public string name { get; private set; }

        public double magnitude { get; set; }

        public override bool Equals(object obj)
        {
            return name.Equals(((Note)obj).name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator !=(Note left, Note right)
        {
            return !left.name.Equals(right.name);
        }

        public static bool operator ==(Note left, Note right)
        {
            return left.name.Equals(right.name);
        }
        public override string ToString()
        {
            return name;
        }

        public int CompareTo(object obj)
        {
            if (magnitude > ((Note)obj).magnitude)
                return -1;

            if (magnitude < ((Note)obj).magnitude)
                return 1;

            return 0;
        }

        public Note(int hz)
        {
            magnitude = 0;
            name = "note not found";
            if (63 <= hz && hz <= 67 ||
                127 <= hz && hz <= 134 ||
                255 <= hz && hz <= 268 ||
                507 <= hz && hz <= 536 ||
                1014 <= hz && hz <= 1072 ) 
                name = "C";

            if( 67 < hz && hz <= 72 ||
                134 < hz && hz <= 143 ||
                268 < hz && hz <= 286 || 
                536 < hz && hz <= 573 ||
                1072 < hz && hz <= 1145 )
                name = "Cs";

            if (72 < hz && hz <= 75 ||
                143 < hz && hz <= 150 ||
               286 < hz && hz <= 301 ||
               573 < hz && hz <= 602 ||
               1145 < hz && hz <= 1204 )
                name = "D";

            if (75 < hz && hz <= 80 ||
                150 < hz && hz <= 159 ||
               301 < hz && hz <= 317 ||
               602 < hz && hz <= 635 ||
               1145 < hz && hz <= 1269 )
                name = "Ds";

            if (80 < hz && hz <= 85 ||
                159 < hz && hz <= 170 ||
                317 < hz && hz <= 339 ||
                635 < hz && hz <= 678 ||
                1269 < hz && hz <= 1356 )
                name = "E";

            if (85 < hz && hz <= 90 ||
                170 < hz && hz <= 179 ||
               339 < hz && hz <= 357 ||
               678 < hz && hz <= 715 ||
               1356 < hz && hz <= 1429 )
                name = "F";

            if (90 < hz && hz <= 95 ||
                179 < hz && hz <= 190 ||
               357 < hz && hz <= 381 ||
               715 < hz && hz <= 763 ||
               1429 < hz && hz <= 1526 )
                name = "Fs";

            if (95 < hz && hz <= 100 ||
                190 < hz && hz <= 201 ||
               381 < hz && hz <= 402 ||
               763 < hz && hz <= 803 ||
               1526 < hz && hz <= 1607 )
                name = "G";

            if (100 < hz && hz <= 108 ||
                201 < hz && hz <= 215 ||
                402 < hz && hz <= 430 ||
                803 < hz && hz <= 856 ||
                1607 < hz && hz <= 1718 )
                name = "Gs";

            if (108 < hz && hz <= 113 ||
                215 < hz && hz <= 226 ||
                430 < hz && hz <= 452 ||
                856 < hz && hz <= 904 ||
                1718 < hz && hz <= 1808)
                name = "A";

            if (113 < hz && hz <= 119 ||
                226 < hz && hz <= 238 ||
                452 < hz && hz <= 476 ||
                904 < hz && hz <= 952 ||
                1808 < hz && hz <= 1904 )
                name = "As";

            if (119 < hz && hz <= 127 ||
                238 < hz && hz <= 254 ||
                476 < hz && hz <= 508 ||
                952 < hz && hz <= 1016 ||
                1904 < hz && hz <= 2036 )
                name = "B";
        }

        public Note(string name)
        {
            this.name = name;
        }
    };
}
