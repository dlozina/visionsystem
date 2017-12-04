using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;

namespace VizijskiSustavWPF 
{
    
      

    public class Regulator
    {

        // Memorija regulatora
        private double x1, x2, vx1, vx2;

        public double T { get; set; }
        public double r0 { get; set; }
        public double s0 { get; set; }
        public double s1 { get; set; }
        public double s2 { get; set; }

        // Konstruktor
        public Regulator()
        {
            T = 0.6782;
            r0 = 0.3426;
            s0 = 12.2006;
            s1 = -13.0834;
            s2 = 1.5610;
            x1 = 0;
            x2 = 0;
            vx1 = 0;
            vx2 = 0;
        }

        public void ResetRegulator()
        {
            x1 = 0;
            x2 = 0;
            vx1 = 0;
            vx2 = 0;
        }

        public double Run(double reference, double actualValue)
        {
            /*
            Funkcija koja vraća upravljačku veličinu (brzinu) na temelju referentne 
            i stvarne vrijednosti pozicije.
            */

            //double vx = (1 - r0) * vx1 + r0 * vx2 + T * reference - s0 * actualValue - s1 * x1 - s2 * x2;

            double vx = ((reference - actualValue) / 1000.0f) * 50;
            if (vx == double.NaN)
            {
                vx = 0;
            }

            // ogranicavanje izlaza iz regulatora
            if (vx > 100)
                vx = 100;
            if (vx < -100)
                vx = -100;

            x2 = x1;
            x1 = actualValue;
            vx2 = vx1;
            vx1 = vx;

            return vx;
        }
    }

    public class OutReadyEventArgs
    {
        private float velocityX;

        public float VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }
        private float velocityY;

        public float VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

        public OutReadyEventArgs(float velX, float velY)
        {
            VelocityX = velX;
            VelocityY = velY;
        }
    }


    public class Algoritmi
    {
    

        public delegate void OutReadyHandler(Algoritmi sender, OutReadyEventArgs e);
        public event OutReadyHandler OutReady;

        // marker da je kamera izgubila objekt iz vidokruga
        private bool objectLost;
        public bool ObjectLost
        {
            get { return objectLost; }
            set { objectLost = value; }
        }

        //definicija regulatora pozicije po X-u i po Y-u
        Regulator regX = new Regulator();
        Regulator regY = new Regulator();

        // vrijeme diskretizacije [ms]
        int td;
        public int Td
        {
            get { return td; }
            set { td = value; }
        }

        // širina ekrana
        double xdim;
        public double Xdim
        {
            get { return xdim; }
            set { xdim = value; }
        }

        // visina ekrana
        double ydim;
        public double Ydim
        {
            get { return ydim; }
            set { ydim = value; }
        }

        // vidljive tocke s ekrana
        // -> POVRATNA VEZA ZA SVA UPRAVLJANJA - POTREBNO OSVJEŽAVATI
        List<PointF> vidljiveTocke;
        public List<PointF> VidljiveTocke
        {
            get
            {
                return vidljiveTocke;
            }

            set
            {
                vidljiveTocke = value;
            }
        }

        // brzine koje se šalju na motore
        // -> UPRAVLJAČKA VELIČINA ZA SVA UPRAVLJANJA - POTREBNO SLATI NA MOTORE
        double vx;
        public double Vx
        {
            get
            {
                return vx;
            }

            set
            {
                vx = value;
            }
        }

        double vy;
        public double Vy
        {
            get
            {
                return vy;
            }

            set
            {
                vy = value;
            }
        }

        // tolerancija za uspoređivanje brojeva
        double compTol;
        public double CompTol
        {
            get
            {
                return compTol;
            }

            set
            {
                compTol = value;
            }
        }

        // tolerancija za određivanje kraja nekog pozicioniranja
        double posTol;
        public double PosTol
        {
            get
            {
                return posTol;
            }

            set
            {
                posTol = value;
            }
        }

        // broj vrhova lima, koristi se za definiranje kraja algoritma
        int brojVrhova;
        public int BrojVrhova
        {
            get
            {
                return brojVrhova;
            }

            set
            {
                brojVrhova = value;
            }
        }


        // Konstruktor
        public Algoritmi()
        {
            td = 100;
            xdim = 200;
            ydim = 200;
            vidljiveTocke = new List<PointF>();
            vx = 0.0;
            vy = 0.0;
            compTol = 1;
            posTol = 1;
            brojVrhova = 5;
        }


        private double myAtan(double y, double x)
        {
            /*
            myAtan koji vraca kut izmedu [0,2pi]
            */

            double v = 0;

            if (x > 0.0)
                v = Math.Atan(y / x);

            if ((y >= 0.0) && (x < 0.0))
                v = Math.PI + Math.Atan(y / x);

            if ((y < 0.0) && (x < 0.0))
                v = -Math.PI + Math.Atan(y / x);

            if ((y > 0.0) && (x == 0.0))
                v = (Math.PI) / 2;

            if ((y < 0.0) && (x == 0.0))
                v = -(Math.PI) / 2;

            if (v < 0.0)
                v += 2 * (Math.PI);

            return v;
        }


        private List<double> fSmjerovi(List<PointF> listaTocaka)
        {
            /* 
            funkcija vraca dva moguca smjera kretanja
             - izlaz je lista od dva moguca smjera
             - ulaz je lista od 3 tocke koje se vide na ekranu
            */

            List<double> smjerovi = new List<double>();

            // pronalazenje vrha

            PointF tockaVrh= new PointF(0,0);

            foreach (PointF t in listaTocaka)
            {
                // tocka je vrh ako nije ni na jednom rubu ekrana
                if ((Math.Abs(t.X - 0.0) > CompTol) && (Math.Abs(t.X - xdim) > CompTol) &&
                    (Math.Abs(t.Y - 0.0) > CompTol) && (Math.Abs(t.Y - ydim) > CompTol))
                {
                    tockaVrh = t;
                    break;
                }
            }

            // racunanje smjerova
            foreach (PointF t in listaTocaka)
            {
                // smjer se racuna za tocke koje nisu tockaVrh
                if (Math.Sqrt(Math.Pow(t.X - tockaVrh.X, 2) + Math.Pow(t.Y - tockaVrh.Y, 2)) > CompTol)
                {
                    smjerovi.Add(myAtan(t.Y - tockaVrh.Y, t.X - tockaVrh.X));
                }
            }

            return smjerovi;

        }


        private void centriranje(double tol)
        {
            /*
            Procedura na temelju informacije s kamere (karakteristicnih tocaka)
            odreduje koja je od tri tocke vrh te centrira kameru iznad nje

            tol - tolerancija regulacijskog odstupanja koja određuje kraj
            centriranja (ciklusa upravljanja)
            
            NAPOMENA: vrlo je važno da nema nadvišenja u upravljanju pozicijom zbog
            opasnosti od preuranjenog prekidanja ciklusa zbog zadovoljenja
            tolerancije u while petlji
            */

            // referenca pozicije vrha kojeg treba centrirati
            double refX = xdim / 2;
            double refY = ydim / 2;

            // pronalazenje vrha

            PointF tockaVrh = new PointF(0, 0);
            

            foreach (PointF t in vidljiveTocke)
            {
                // tocka je vrh ako nije ni na jednom rubu ekrana
                if ((Math.Abs(t.X - 0.0) > CompTol) && (Math.Abs(t.X - xdim) > CompTol) &&
                    (Math.Abs(t.Y - 0.0) > CompTol) && (Math.Abs(t.Y - ydim) > CompTol))
                {
                    tockaVrh = t;
                    break;
                }
            }

            double error = Math.Sqrt(Math.Pow(refX - tockaVrh.X, 2) + Math.Pow(refY - tockaVrh.Y, 2));

            regX.ResetRegulator();
            regY.ResetRegulator();

            vx = 0.0;
            vy = 0.0;

            // UPRAVLJAČKA PETLJA
            while (error > tol)
            {

                vx = -regX.Run(refX, tockaVrh.X);
                vy = -regY.Run(refY, tockaVrh.Y);

                OutReady(this, new OutReadyEventArgs((float)vx/40, (float)vy/40));

                // osvježavanje
                foreach (PointF t in vidljiveTocke)
                {
                    // tocka je vrh ako nije ni na jednom rubu ekrana
                    if ((Math.Abs(t.X - 0.0) > CompTol) && (Math.Abs(t.X - xdim) > CompTol) &&
                        (Math.Abs(t.Y - 0.0) > CompTol) && (Math.Abs(t.Y - ydim) > CompTol))
                    {
                        tockaVrh = t;
                        break;
                    }
                }

                error = Math.Sqrt(Math.Pow(refX - tockaVrh.X, 2) + Math.Pow(refY - tockaVrh.Y, 2));

                Thread.Sleep(Td);
            }

            vx = 0.0;
            vy = 0.0;

        }


        private void pratiBrid(double smjer)
        {

            /*
            Procedura na temelju informacije s kamere (karakteristicnih tocaka)
            ide po bridu od jednog vrha do drugog

            smjer - okvirni smjer kretanja - određen u prethodnom koraku (nakon centriranja)
            */

            // okvirne brzine potrebne da bi se drzao zeljeni smjer
            double constX = 100 * Math.Cos(smjer);
            double constY = 100 * Math.Sin(smjer);

            // zastavica:
            // 0 - vide se 3 točke na ekranu, praćenje brida kreće/traje
            // 1 - vide se 2 točke na ekranu, praćenje brida traje
            // 2 - vide se po drugi put 3 točke na ekranu, praćenje brida staje
            int zastavica = 0;

            int n = vidljiveTocke.Count();

            // pronalazenje vrha

            PointF tockaVrh = new PointF(0,0);
           

            foreach (PointF t in vidljiveTocke)
            {
                // tocka je vrh ako nije ni na jednom rubu ekrana
                if ((Math.Abs(t.X - 0.0) > CompTol) && (Math.Abs(t.X - xdim) > CompTol) &&
                    (Math.Abs(t.Y - 0.0) > CompTol) && (Math.Abs(t.Y - ydim) > CompTol))
                {
                    tockaVrh = t;
                    break;
                }
            }

            // odredivanje referentne pozicije točki koju je potrebno regulirati (prema zadanom smjeru)
            PointF tockaRef = new PointF(0,0);
           

            foreach (PointF t in vidljiveTocke)
            {
                // smjer se racuna za tocke koje nisu tockaVrh
                if (Math.Sqrt(Math.Pow(t.X - tockaVrh.X, 2) + Math.Pow(t.Y - tockaVrh.Y, 2)) > CompTol)
                {
                    double smjerTemp = myAtan(t.Y - tockaVrh.Y, t.X - tockaVrh.X);
                    if (Math.Abs(smjerTemp - smjer) < 0.1)
                        tockaRef = t;
                }
            }

            // tolerancija kuta za pronalazak tocke kojom se upravlja
            double fiTol = 15 * Math.PI / 180;

            regX.ResetRegulator();
            regY.ResetRegulator();

            vx = 0.0;
            vy = 0.0;

            PointF tockaReal=new PointF(0,0);
     

            // UPRAVLJAČKA PETLJA
            while (zastavica != 2)
            {
                // odredivanje tocke koju se prati
                foreach (PointF t in vidljiveTocke)
                {
                    // smjer se racuna za tocke koje nisu tockaVrh
                    if (Math.Sqrt(Math.Pow(t.X - tockaVrh.X, 2) + Math.Pow(t.Y - tockaVrh.Y, 2)) > CompTol)
                    {
                        double smjerTemp = myAtan(t.Y - tockaVrh.Y, t.X - tockaVrh.X);
                        // ovih 2 * pi je zbog slucaja kad je zadani smjer 0 odnosno 2 * pi pa
                        // stvarni smjer skace s 0 na 2 * pi
                        if (Math.Abs(smjerTemp - smjer) < fiTol ||
                            Math.Abs(smjerTemp - smjer + 2 * Math.PI) < fiTol ||
                            Math.Abs(smjerTemp - smjer - 2 * Math.PI) < fiTol)
                            tockaReal = t;
                    }
                }

                vx = constX - regX.Run(tockaRef.X, tockaReal.X);
                vy = constY - regY.Run(tockaRef.Y, tockaReal.Y);

                OutReady(this, new OutReadyEventArgs((float)vx/5, (float)vy/5));

                n = vidljiveTocke.Count();

                //prelazak između faza
                if (zastavica == 1 && ((n == 3)||(objectLost)))
                    // to znaci da je ciklus bio u fazi kad se vide samo dvije tocke na
                    // ekranu i pojavila se treća
                    zastavica = 2;
                if (zastavica == 0 && n == 2)
                    zastavica = 1;

                Thread.Sleep(Td);
            }

            vx = 0.0;
            vy = 0.0;
        }


        //  G L A V N A   F U N K C I J A

        public void Algoritam()
        {
            /*
            Ova procedura inicijalizira algoritam praćenja rubova lima te ga vrti do kraja.
            Tu se pozivaju sve prethodno definirane funkcije/procedure klase algoritmi.
            Ideja je da se prije poziva ovog algoritma kamera pozicionira iznad nekog vrha lima
            (npr. donjeg lijevog).
            */

            // centriranje kamere
            centriranje(posTol);

            // moguci smjerovi kretanja nakon centriranja - odabire se veci
            List<double> smjerovi = new List<double>();
            smjerovi = fSmjerovi(vidljiveTocke);
            double smjer;
            smjer = smjerovi.Max();

            double smjerProsli = double.NaN, maxDelta;
            int index, counter = 0;
            
            // PETLJA ALGORITMA
            // -> tu se izmjenjuju pratiBrid i centriranje dok se ne ispuni trazeni broj vrhova
            while (true)
            {
                pratiBrid(smjer);
                //while (vidljiveTocke.Count==3)
                centriranje(posTol);

                // Prosli smjer treba promijeniti u suprotan jer je ce isti taj u analizi na
                // iducem vrhu biti suprotan u odnosu na analizu na proslom vrhu.
                if (smjer < Math.PI)
                    smjerProsli = smjer + Math.PI;
                if (smjer >= Math.PI)
                    smjerProsli = smjer - Math.PI;

                smjerovi = fSmjerovi(vidljiveTocke);

                // trazenje novog smjera
                index = 0;
                maxDelta = Math.Abs(smjerovi.ElementAt(0) - smjerProsli);
                if (Math.Abs(smjerovi.ElementAt(1) - smjerProsli) > maxDelta)
                    index = 1;
                smjer = smjerovi.ElementAt(index);

                counter = counter + 1;

                // uvjet za prekid petlje, kraj algoritma
                if (counter == brojVrhova)
                    break;
            }
        }

        // Traženje odgovarajućih točaka limova (u automatskom mjerenju nije poznato od koje je točke praćenje bridova započelo)
        public List<System.Drawing.PointF> FindMatchingPoints(List<float> referenceAngles, List<System.Drawing.PointF> points)
        {
            if (referenceAngles.Count != points.Count || points.Count < 3) return null;
            
            // Uz svaku točku pohranjuje se i kut
            List<Tuple<System.Drawing.PointF, float>> pointsWithAngle = new List<Tuple<System.Drawing.PointF, float>>();
            for (int i = 0; i < points.Count; i++)
            {
                // Računaj kut uz točku points[i] u stupnjevima
                int prev_i = (i - 1 >= 0 ? i - 1 : i - 1 + points.Count);
                int next_i = (i + 1 < points.Count ? i + 1 : i + 1 - points.Count);

                var dx1 = points[prev_i].X - points[i].X;
                var dy1 = points[prev_i].Y - points[i].Y;
                var dx2 = points[next_i].X - points[i].X;
                var dy2 = points[next_i].Y - points[i].Y;
                var p1l2 = dx1 * dx1 + dy1 * dy1;
                var p2l2 = dx2 * dx2 + dy2 * dy2;
                float kut = (float)(Math.Acos((dx1 * dx2 + dy1 * dy2) / Math.Sqrt(p1l2 * p2l2)) * 180 / Math.PI);
                pointsWithAngle.Add(new Tuple<PointF, float>(points[i], kut));
            }

            // Rotiraj točke dok se redoslijed kutova ne poklopi s referentnim
            bool matchFound = false;
            for (int i = 0; i < pointsWithAngle.Count; i++)
            {
                for (int j = 0; j < pointsWithAngle.Count; j++)
                {
                    if ((pointsWithAngle[j].Item2 < referenceAngles[j] - 5) || (pointsWithAngle[j].Item2 > referenceAngles[j] + 5)) // kut ne odgovara
                    {
                        matchFound = false;
                        break;
                    }
                    matchFound = true;
                }
                if (matchFound) break;
                else
                {
                    // Rotacija točaka i novi pokušaj ([0] ide na kraj liste)
                    var temp = pointsWithAngle[0];
                    pointsWithAngle.RemoveAt(0);
                    pointsWithAngle.Add(temp);
                }
            }

            // Ako redoslijed nije pronađen probaj u drugom smjeru (možda je kamera išla u suprotnom smjeru)
            if (!matchFound)
            {
                pointsWithAngle.Reverse();
                for (int i = 0; i < pointsWithAngle.Count; i++)
                {
                    for (int j = 0; j < pointsWithAngle.Count; j++)
                    {
                        if ((pointsWithAngle[j].Item2 < referenceAngles[j] - 5) || (pointsWithAngle[j].Item2 > referenceAngles[j] + 5)) // kut ne odgovara
                        {
                            matchFound = false;
                            break;
                        }
                        matchFound = true;
                    }
                    if (matchFound) break;
                    else
                    {
                        // Rotacija točaka i novi pokušaj ([0] ide na kraj liste)
                        var temp = pointsWithAngle[0];
                        pointsWithAngle.RemoveAt(0);
                        pointsWithAngle.Add(temp);
                    }
                }
            }

            if (matchFound)
            {
                List<System.Drawing.PointF> matchingPoints = new System.Collections.Generic.List<PointF>(pointsWithAngle.Count);
                for (int i = 0; i < pointsWithAngle.Count; i++)
                {
                    matchingPoints[i] = pointsWithAngle[i].Item1;
                }
                return matchingPoints;
            }
            else
            {
                return null;
            }
        }

    }

    

}
