namespace VizijskiSustavWPF.PLC_interface
{
    public class Status
    {
        public horizontalnaOs HorizontalnaOs { get; set; } = new horizontalnaOs();
        public vertikalnaOs VertikalnaOs { get; set; } = new vertikalnaOs();
        public rotacijskaOs RotacijskaOs { get; set; } = new rotacijskaOs();
        public ticalo Ticalo { get; set; } = new ticalo();
        public mjerenjeticalom MjerenjeTicalom { get; set; } = new mjerenjeticalom();
        public mjerenjeporoznosti MjerenjePoroznosti { get; set; } = new mjerenjeporoznosti();
        public mjerenjediametara MjerenjeDiametara { get; set; } = new mjerenjediametara();
        public cilindri Cilindri { get; set; } = new cilindri();
        public kamere Kamere { get; set; } = new kamere();


        public class horizontalnaOs
        {
           
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 1), false);
            public plcTag UPozicijiTicala { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 2), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 3), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(2, 0), 0.0f);
        
        }

        public class vertikalnaOs
        {
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 1), false);
            public plcTag USafePoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 2), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 3), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(8, 0), 0.0f);
        }
        public class rotacijskaOs
        {
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 2), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(14, 0), 0.0f);
            public plcTag Referencirana { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(18, 0), false);
        }

        public class ticalo
        {
            public plcTag TicaloDolje { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(20, 0), false);
            public plcTag TicaloGore { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(20, 1), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(22, 0), 0.0f);
            public plcTag Referencirano { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(26, 0), false);
            
        }

        public class mjerenjeticalom
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(30, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(34, 0), 0.0f);
            public plcTag RotacijskaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(38, 0), 0.0f);
            public plcTag HorizontalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(42, 0), 0.0f);
            public plcTag Visina1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(46, 0), 0.0f);
            public plcTag Visina2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(50, 0), 0.0f);
            public plcTag Visina3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(54, 0), 0.0f);
        }

        public class mjerenjeporoznosti
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(60, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(64, 0), 0.0f);
            public plcTag RotacijskaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(68, 0), 0.0f);
            public plcTag HorizontalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(72, 0), 0.0f);
            public plcTag PoroznostPronadena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(76, 0), false);
        }

        public class mjerenjediametara
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(80, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(84, 0), 0.0f);
            public plcTag VertikalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(88, 0), 0.0f);
            public plcTag HorizontalnaOsS1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(92, 0), 0.0f);
            public plcTag HorizontalnaOsS2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(96, 0), 0.0f);
            public plcTag AnaliziranaVrijednost { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(100, 0), 0.0f);
            public plcTag Diametar1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(104, 0), 0.0f);
            public plcTag Diametar2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(108, 0), 0.0f);
            public plcTag Diametar3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(112, 0), 0.0f);
            public plcTag Diametar4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(116, 0), 0.0f);
        }

        public class cilindri
        {
            public plcTag PPOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 0), false);
            public plcTag PPZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 1), false);
            public plcTag StegaOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 2), false);
            public plcTag StegaZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 3), false);
        }

        public class kamere
        {
            public plcTag CAM1ZahtjevZaAnalizomT1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 0), false);
            public plcTag CAM1ZahtjevZaAnalizomT2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 1), false);
            public plcTag CAM2ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 2), false);
            public plcTag CAM3ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 3), false);
            public plcTag CAM4ZahtjevZaAnalizomS1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 4), false);
            public plcTag CAM4ZahtjevZaAnalizomS2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 5), false);
            public plcTag CAM1AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 6), false);
            public plcTag CAM2AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 7), false);
            public plcTag CAM3AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 0), false);
            public plcTag CAM4AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 1), false);
            public plcTag CAM1AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 2), false);
            public plcTag CAM2AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 3), false);
            public plcTag CAM3AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 4), false);
            public plcTag CAM4AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 5), false);
            public plcTag CAM1RezultatX { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(124, 0), 0.0f);
            public plcTag CAM1RezultatY { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(128, 0), 0.0f);
            public plcTag CAM2Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(132, 0), 0.0f);
            public plcTag CAM3Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(136, 0), 0.0f);
            public plcTag CAM4Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(140, 0), 0.0f);
        }
    }
}