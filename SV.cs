using System;

namespace BT_NET {
    class SV : Object {
        public int MSSV { get; set; }
        public string NameSV { get; set; }
        public double DTB { get; set; }

        // public virtual void Show() {
        //     Console.Write("MSSV = {0}, Name = {1}, DTB = {2}, ", MSSV, NameSV, DTB);
        // }

        public override string ToString() {
            return "MSSV = " + MSSV + ", Name = "
            + NameSV + ", DTB = " + DTB.ToString();;
        }

        public override bool Equals(Object obj) {
          // If the passed object is null
            if (obj is SV other) {
                if (MSSV == other.MSSV && (NameSV.CompareTo(other.NameSV) == 0) && DTB == other.DTB)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return MSSV.GetHashCode()^
                NameSV.GetHashCode()^
                DTB.GetHashCode();
        }
    }
}