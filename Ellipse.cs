using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mgmk_ellipse {
    internal class Ellipse {

        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }

        public Ellipse() {
            this.a = 0;
            this.b = 0;
            this.c = 0;
        }

        public Ellipse(double a, double b, double c) {
            this.a = a;
            this.b = b;
            this.c = c;
        }

    }
}
