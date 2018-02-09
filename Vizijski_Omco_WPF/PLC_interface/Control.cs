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
            
        }

        public class ucenjebool
        {
            public plcTag NoviNalog { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 0), false);
            public plcTag ResetUcenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 1), false);
            public plcTag IdiUD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 2), false);
            public plcTag IdiUD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 3), false);
            public plcTag IdiUD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 4), false);
            public plcTag IdiUD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 5), false);
            public plcTag IdiUD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 6), false);
            public plcTag IdiUD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 7), false);
            public plcTag IdiUD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 0), false);
            public plcTag IdiUD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 1), false);
            public plcTag IdiUD5S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 2), false);
            public plcTag IdiUD5S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 3), false);
            public plcTag IdiNaV1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 4), false);
            public plcTag IdiNaV2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 5), false);
            public plcTag IdiNaV3 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 6), false);
            public plcTag NauciD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(87, 7), false);
            public plcTag NauciD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 0), false);
            public plcTag NauciD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 1), false);
            public plcTag NauciD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 2), false);
            public plcTag NauciD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 3), false);
            public plcTag NauciD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 4), false);
            public plcTag NauciD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 5), false);
            public plcTag NauciD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 6), false);
            public plcTag NauciD5S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(88, 7), false);
            public plcTag NauciD5S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(89, 0), false);
            // Diametar za poroznost
            public plcTag PoroznostHorPozicija { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(89, 1), false);
            public plcTag PoroznostVerPozicija { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(89, 2), false);
            // Nauci pozicije poroznosti
            public plcTag NauciPoroznost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(89, 3), false);

        }

        public class automatika
        {
            public plcTag ResetSvihMjerenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(90, 0), false);
        }
    }
}