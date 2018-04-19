using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace Logic
{
    public class GebruikerLogic
    {
        private GebruikerEngine _gebruikerEngine = new GebruikerEngine();
        public string KlantToevoegen(Klant klant)
        {
            return _gebruikerEngine.KlantToevoegen(klant);
        }

        public object Inloggen(string gebruikersnaam, string wachtwoord)
        {
            return _gebruikerEngine.Inloggen(gebruikersnaam, wachtwoord);
        }
        public List<Product> WinkelwagenProducten(int KlantID)
        {
            return _gebruikerEngine.WinkelwagenProducten(KlantID);
        }

        public void VoegToeAanWinkelwagen(int KlantID, int KeycodeID)
        {
            _gebruikerEngine.VoegToeAanWinkelwagen(KlantID, KeycodeID);
        }

        public void VerwijderUitWinkelwagen(int KeycodeID)
        {
            _gebruikerEngine.VerwijderUitWinkelwagen(KeycodeID);
        }
        public void SaldoToevoegen(decimal Saldotoevoegen, int id)
        {
             _gebruikerEngine.SaldoToevoegen(Saldotoevoegen,id);
        }

        public object KlantgegevensZonderSaldo(int id, string gebruikersnaam)
        {
            return _gebruikerEngine.KlantgegevensZonderSaldo(id, gebruikersnaam);
        }

        public object KlantgegevensVolledig(int id, string gebruikersnaam)
        {
            return _gebruikerEngine.KlantgegevensVolledig(id, gebruikersnaam);
        }
    }
}
