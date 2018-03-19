namespace VizijskiSustavWPF.PLC_interface
{
    public class Control
    {
        public horizontalnaOs HorizontalnaOs { get; set; } = new horizontalnaOs();
        public vertikalnaOs VertikalnaOs { get; set; } = new vertikalnaOs();
        public rotacijskaOs RotacijskaOs { get; set; } = new rotacijskaOs();
        public ticalo Ticalo { get; set; } = new ticalo();
        public mjerenjeticalom MjerenjeTicalom { get; set; } = new mjerenjeticalom();
        public mjerenjeporoznosti MjerenjePoroznosti { get; set; } = new mjerenjeporoznosti();
        public citanjestringa CitanjeStringa { get; set; } = new citanjestringa();
        public mjerenjediametara MjerenjeDiametara { get; set; } = new mjerenjediametara ();
        public kamere Kamere { get; set; } = new kamere();
        public cilindri Cilindri { get; set; } = new cilindri();
        public robot Robot { get; set; } = new robot();
        public ucenjereal UcenjeReal { get; set; } = new ucenjereal();
        public ucenjebool UcenjeBool { get; set; } = new ucenjebool();
        public automatika Automatika { get; set; } = new automatika();
        public prekret Prekret { get; set; } = new prekret();
        public uzimanjekomada UzimanjeKomada { get; set; } = new uzimanjekomada();
        public tolerance Tolerance { get; set; } = new tolerance();

        public class horizontalnaOs
        {
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 1), false);
            public plcTag IdiUPozicijuTicala { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(2, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(6, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(6, 1), false);
            public plcTag IdiURub1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(8, 0), 0.0f);
            public plcTag IdiURub2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(12, 0), 0.0f);
            public plcTag AutomatikaStart { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(16, 0), false);

        }
        public class vertikalnaOs
        {
            
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 1), false);
            public plcTag IdiUSafePoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(20, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(24, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(24, 1), false);
        }
        public class rotacijskaOs
        {
            
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 1), false);
            public plcTag Referenciraj { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(28, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(32, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(32, 1), false);
        }
        public class ticalo
        {
            public plcTag TicaloGore { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 0), false);
            public plcTag TicaloDolje { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 1), false);
            public plcTag Nuliraj { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 2), false);
            public plcTag PostaviNaVrijednost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 3), false);
        }

        public class mjerenjeticalom
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 3), false);
        }

        public class mjerenjeporoznosti
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 3), false);
            public plcTag ZadanaPovrsina { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(40, 0), 0.0f);
        }

        public class mjerenjediametara
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 3), false);
        }

        public class citanjestringa
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 3), false);
        }

        public class kamere
        {
            public plcTag RasvjetaZaString { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 0), false);
            public plcTag RasvjetaZaDimenzije { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 1), false);
            public plcTag RasvjetaZaPoroznost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 2), false);
            public plcTag RasvjetaZaD2Lijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 3), false);
            public plcTag RasvjetaZaD2Desno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 4), false);
            public plcTag CAM4zahtjevZaAnalizomS1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 5), false);
            public plcTag CAM4zahtjevZaAnalizomS2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 6), false);
        }

        public class cilindri
        {
            public plcTag PPOtvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 0), false);
            public plcTag PPZatvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 1), false);
            public plcTag StegaOtvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 2), false);
            public plcTag StegaZatvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 3), false);
        }

        public class robot
        {
            public plcTag ZahtijevZaKomadom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 0), false);
            public plcTag KomadOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 1), false);
            public plcTag KomadNok { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 2), false);
            public plcTag ResetPaleteNovi { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 3), false);
            public plcTag ResetPaleteOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 4), false);
            public plcTag ResetPaleteNok { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 5), false);
            public plcTag ResetPaleteLimova { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 6), false);
        }

        public class ucenjereal
        {
            public plcTag ZadavanjeDijametara1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(54, 0), 0.0f);
            public plcTag ZadavanjeDijametara2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(58, 0), 0.0f);
            public plcTag ZadavanjeDijametara3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(62, 0), 0.0f);
            public plcTag ZadavanjeDijametara4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(66, 0), 0.0f);
            public plcTag ZadavanjeDijametara5 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(70, 0), 0.0f);
            public plcTag ZadavanjeVisineV1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(74, 0), 0.0f);
            public plcTag ZadavanjeVisineV2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(78, 0), 0.0f);
            public plcTag ZadavanjeVisineV3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(82, 0), 0.0f);
            public plcTag ZadavanjeVisineBaze { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(86, 0), 0.0f);
        }

        public class ucenjebool
        {
            public plcTag NoviNalog { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 0), false);
            public plcTag ResetUcenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 1), false);
            public plcTag IdiUD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 2), false);
            public plcTag IdiUD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 3), false);
            public plcTag IdiUD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 4), false);
            public plcTag IdiUD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 5), false);
            public plcTag IdiUD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 6), false);
            public plcTag IdiUD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 7), false);
            public plcTag IdiUD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 0), false);
            public plcTag IdiUD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 1), false);
            public plcTag IdiUD5S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 2), false);
            public plcTag IdiUD5S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 3), false);
            public plcTag IdiNaV1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 4), false);
            public plcTag IdiNaV2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 5), false);
            public plcTag IdiNaV3 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 6), false);
            public plcTag NauciD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(91, 7), false);
            public plcTag NauciD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 0), false);
            public plcTag NauciD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 1), false);
            public plcTag NauciD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 2), false);
            public plcTag NauciD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 3), false);
            public plcTag NauciD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 4), false);
            public plcTag NauciD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 5), false);
            public plcTag NauciD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 6), false);
            public plcTag NauciD5S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(92, 7), false);
            public plcTag NauciD5S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(93, 0), false);
            // Diametar za poroznost
            public plcTag PoroznostHorPozicija { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(93, 1), false);
            public plcTag PoroznostVerPozicija { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(93, 2), false);
            // Nauci pozicije poroznosti
            public plcTag NauciPoroznost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(93, 3), false);
        }

        public class automatika
        {
            public plcTag ResetSvihMjerenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(94, 0), false);
        }

        public class prekret
        {
            public plcTag PrekretLijevo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(96, 0), false);
            public plcTag PrekretDesno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(96, 1), false);
            public plcTag PrekretOtvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(96, 2), false);
            public plcTag PrekretZatvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(96, 3), false);
        }

        public class uzimanjekomada
        {
            public plcTag BrojSlojevaUlaznaPaletaLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(98, 0), 0.0f);
            public plcTag BrojSlojevaUlaznaPaletaDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(102, 0), 0.0f);
            public plcTag BrojSlojevaKomadiOKPaletaLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(106, 0), 0.0f);
            public plcTag BrojSlojevaKomadiOKPaletaDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(110, 0), 0.0f);
            public plcTag BrojSlojevaKomadiNOKPaletaLijevo { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(114, 0), 0.0f);
            public plcTag BrojSlojevaKomadiNOKPaletaDesno { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(118, 0), 0.0f);
            public plcTag BrojLimovaUPozicijiOdlaganja { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(122, 0), 0.0f);
            public plcTag DebljinaLimova { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(126, 0), 0.0f);
        }

        public class tolerance
        {
            public plcTag Dijametar1DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(130, 0), 0.0f);
            public plcTag Dijametar1DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(134, 0), 0.0f);
            public plcTag Dijametar2DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(138, 0), 0.0f);
            public plcTag Dijametar2DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(142, 0), 0.0f);
            public plcTag Dijametar3DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(146, 0), 0.0f);
            public plcTag Dijametar3DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(150, 0), 0.0f);
            public plcTag Dijametar4DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(154, 0), 0.0f);
            public plcTag Dijametar4DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(158, 0), 0.0f);
            public plcTag Dijametar5DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(162, 0), 0.0f);
            public plcTag Dijametar5DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(166, 0), 0.0f);
            public plcTag Visina1DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(170, 0), 0.0f);
            public plcTag Visina1DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(174, 0), 0.0f);
            public plcTag Visina2DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(178, 0), 0.0f);
            public plcTag Visina2DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(182, 0), 0.0f);
            public plcTag Visina3DeltaMinus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(186, 0), 0.0f);
            public plcTag Visina3DeltaPlus { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(190, 0), 0.0f);
        }
    }
}