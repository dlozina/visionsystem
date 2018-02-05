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
        public ucenje Ucenje { get; set; } = new ucenje();

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
            public plcTag Robot4 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 3), false);
        }

        public class ucenje
        {
            public plcTag NoviNalog { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(54, 0), false);
            public plcTag ResetUcenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(54, 1), false);
            public plcTag ZadavanjeDijametara1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(56, 0), 0.0f);
            public plcTag ZadavanjeDijametara2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(60, 0), 0.0f);
            public plcTag ZadavanjeDijametara3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(64, 0), 0.0f);
            public plcTag ZadavanjeDijametara4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(68, 0), 0.0f);
            public plcTag ZadavanjeVisineV1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(72, 0), 0.0f);
            public plcTag ZadavanjeVisineV2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(76, 0), 0.0f);
            public plcTag ZadavanjeVisineV3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(80, 0), 0.0f);
            public plcTag IdiUD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 0), false);
            public plcTag IdiUD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 1), false);
            public plcTag IdiUD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 2), false);
            public plcTag IdiUD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 3), false);
            public plcTag IdiUD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 4), false);
            public plcTag IdiUD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 5), false);
            public plcTag IdiUD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 6), false);
            public plcTag IdiUD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 7), false);
            public plcTag IdiNaV1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 0), false);
            public plcTag IdiNaV2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 1), false);
            public plcTag NauciD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 2), false);
            public plcTag NauciD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 3), false);
            public plcTag NauciD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 4), false);
            public plcTag NauciD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 5), false);
            public plcTag NauciD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 6), false);
            public plcTag NauciD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 7), false);
            public plcTag NauciD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 0), false);
            public plcTag NauciD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 1), false);
        }

    }
}