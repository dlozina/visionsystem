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
        public prekret Prekret { get; set; } = new prekret();
        public automatika Automatika { get; set; } = new automatika();
        public upisanevrijednosti Upisanevrijednosti { get; set; } = new upisanevrijednosti();
        public trenutnislojevi TrenutniSlojevi { get; set; } = new trenutnislojevi();

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
            public plcTag GotovoCAM2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(76, 1), false);
            public plcTag GotovoCAM3 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(76, 2), false);
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
            public plcTag Diametar5 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(120, 0), 0.0f);
        }

        public class cilindri
        {
            public plcTag PPOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(124, 0), false);
            public plcTag PPZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(124, 1), false);
            public plcTag StegaOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(124, 2), false);
            public plcTag StegaZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(124, 3), false);
        }

        public class kamere
        {
            public plcTag CAM1ZahtjevZaAnalizomT1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 0), false);
            public plcTag CAM1ZahtjevZaAnalizomT2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 1), false);
            public plcTag CAM2ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 2), false);
            public plcTag CAM3ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 3), false);
            public plcTag CAM4ZahtjevZaAnalizomS1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 4), false);
            public plcTag CAM4ZahtjevZaAnalizomS2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 5), false);
            public plcTag CAM1AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 6), false);
            public plcTag CAM2AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(126, 7), false);
            public plcTag CAM3AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 0), false);
            public plcTag CAM4AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 1), false);
            public plcTag CAM1AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 2), false);
            public plcTag CAM2AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 3), false);
            public plcTag CAM3AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 4), false);
            public plcTag CAM4AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(127, 5), false);
            public plcTag CAM1RezultatX { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(128, 0), 0.0f);
            public plcTag CAM1RezultatY { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(132, 0), 0.0f);
            public plcTag CAM1RezultatAngle { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(136, 0), 0.0f);
            public plcTag CAM1RezultatWorkpieceDiameter { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(140, 0), 0.0f);
            public plcTag CAM2Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(144, 0), 0.0f);
            public plcTag CAM3Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(148, 0), 0.0f);
            public plcTag CAM4Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(152, 0), 0.0f);
        }

        public class prekret
        {
            public plcTag PrekretLijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(156, 0), false);
            public plcTag PrekretDesno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(156, 1), false);
            public plcTag PrekretOtvoren { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(156, 2), false);
            public plcTag PrekretZatvoren { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(156, 3), false);
        }

        public class automatika
        {
            public plcTag AutomatskiRad { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(158, 0), false);
            public plcTag RucniRad { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(158, 1), false);
            public plcTag SnimiMjerenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(158, 2), false);
            public plcTag StopNaKrajuCiklusa { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(158, 3), false);
            public plcTag StatusUcenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(158, 4), false);
        }

        public class upisanevrijednosti
        {
            public plcTag Dijametar1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(160, 0), 0.0f);
            public plcTag Dijametar1DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(164, 0), 0.0f);
            public plcTag Dijametar1DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(168, 0), 0.0f);
            public plcTag Dijametar2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(172, 0), 0.0f);
            public plcTag Dijametar2DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(176, 0), 0.0f);
            public plcTag Dijametar2DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(180, 0), 0.0f);
            public plcTag Dijametar3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(184, 0), 0.0f);
            public plcTag Dijametar3DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(188, 0), 0.0f);
            public plcTag Dijametar3DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(192, 0), 0.0f);
            public plcTag Dijametar4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(196, 0), 0.0f);
            public plcTag Dijametar4DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(200, 0), 0.0f);
            public plcTag Dijametar4DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(204, 0), 0.0f);
            public plcTag Dijametar5 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(208, 0), 0.0f);
            public plcTag Dijametar5DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(212, 0), 0.0f);
            public plcTag Dijametar5DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(216, 0), 0.0f);
            public plcTag Visina1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(220, 0), 0.0f);
            public plcTag Visina1DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(224, 0), 0.0f);
            public plcTag Visina1DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(228, 0), 0.0f);
            public plcTag Visina2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(232, 0), 0.0f);
            public plcTag Visina2DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(236, 0), 0.0f);
            public plcTag Visina2DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(240, 0), 0.0f);
            public plcTag Visina3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(244, 0), 0.0f);
            public plcTag Visina3DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(248, 0), 0.0f);
            public plcTag Visina3DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(252, 0), 0.0f);
        }

        public class trenutnislojevi
        {
            public plcTag UlaznaLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(256, 0), 0.0f);
            public plcTag UlaznaDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(260, 0), 0.0f);
            public plcTag KomadiOKLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(264, 0), 0.0f);
            public plcTag KomadiOKDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(268, 0), 0.0f);
            public plcTag KomadiNOKLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(272, 0), 0.0f);
            public plcTag KomadiNOKDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(276, 0), 0.0f);
            public plcTag AktivnaUlaznaLijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 0), false);
            public plcTag AktivnaUlaznaDesno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 1), false);
            public plcTag AktivnaKomadiOKLijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 2), false);
            public plcTag AktivnaKomadiOKDesno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 3), false);
            public plcTag AktivnaKomadiNOKLijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 4), false);
            public plcTag AktivnaKomadiNOKDesno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(280, 5), false);
            public plcTag BrojLimovaPozicijaOdlaganja { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(282, 0), 0.0f);

        }
    }
}