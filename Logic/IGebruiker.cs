using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IGebruiker
    {
            string KlantToevoegen(Klant klant);
            object Inloggen(string gebruikersnaam, string wachtwoord);
            List<Product> WinkelwagenProducten(int KlantID);
            void VoegToeAanWinkelwagen(int KlantID, int KeycodeID);

            void VerwijderUitWinkelwagen(int KeycodeID);

            void SaldoToevoegen(decimal Saldotoevoegen, int id);

            object KlantgegevensZonderSaldo(int id, string gebruikersnaam);

            object KlantgegevensVolledig(int id, string gebruikersnaam);
    }
}
