using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Fantacalcio
{
    /**
     * \brief La classe fantaallenatore chiede in entrata un nome del fantaallenatore, i crediti del fantaallenatore.
     * \param string nome: nome del calciatore
     * \param string ruolo: ruolo del calciatore
     * \param int prezzo: prezzo del calciatore
     */
    [Serializable]
    class fantaallenatore
    {
        //Attributi
        [JsonProperty]
        public string nome;
        public int N_portieri, N_difensori, N_centrocampisti, N_attacanti;
        [JsonProperty]
        protected double Fantaallenatore_punti;
        [JsonProperty]
        protected int Fantaallenatore_crediti;
        [JsonProperty]
        List<fantacalciatore> Lista_Calciatori = new List<fantacalciatore>();

        //Costruttore
        public fantaallenatore(string nome, int Fantaallenatore_crediti)
        {
            this.nome = nome;
            this.Fantaallenatore_crediti = Fantaallenatore_crediti;
            this.Fantaallenatore_punti = 0;
        }

        //Metodi
        public void Diminuisci_credito(int credito)
        {
            Fantaallenatore_crediti -= credito;
        }

        public void Aumenenta_credito(int credito)
        {
            Fantaallenatore_crediti += credito;
        }

        public int Visualizza_credito()
        {
            return Fantaallenatore_crediti;
        }

        public void Inserisci_punti(double punti)
        {
            Fantaallenatore_punti += punti;
        }

        public double Mostra_punti()
        {
            return Fantaallenatore_punti;
        }

        public List<fantacalciatore> Mostra_Calciatori() //Metodo che ritorna la Lista_calciatori.
        {
            return Lista_Calciatori;
        }

        public void Aggiungi_Calciatore(fantacalciatore calciatori) //Metodo che aggiunge i calciatori alla lista Lista_calciatori.
        {
            Lista_Calciatori.Add(calciatori);
        }

        public void Rimuovi_Calciatore(string nome) //Metodo che rimuove i calciatori dalla Lista_calciatori.
        {
            for (int i = 0; i < Lista_Calciatori.Count; i++)
            {
                if (Lista_Calciatori[i].Get_nome() == nome)
                {
                    Lista_Calciatori.Remove(Lista_Calciatori[i]);
                }
            }
        }

        public override string ToString() //Metodo ToString, che ritorna la stringa nome.
        {
            return nome;
        }

        public double Get_Fantaallenatore_punti()
        {
            return Fantaallenatore_punti;
        }

        public int Get_Fantaallenatore_crediti()
        {
            return Fantaallenatore_crediti;
        }

        public int CompareTo(fantaallenatore squadra2) //Metodo che serve per comparare i gol fatti, gol subiti o i punti in base al codice inserito.
        {
            double datoSquadra1 = 0, datoSquadra2 = 0; //Inizializzo le variabili di tipo int datoSquadra1 e datoSquadra2 e le pongo pari a 0.
            datoSquadra1 = this.Get_Fantaallenatore_punti(); //Pongo la variablie datoSquadra1 pari al valore restituito dal metodo Calcola_punti di questa classe.
            datoSquadra2 = squadra2.Get_Fantaallenatore_punti(); //Pongo la variablie datoSquadra2 pari al valore restituito dal metodo della squadra 2 Calcola_punti.

            if (datoSquadra1 > datoSquadra2) //Se il valore di datoSquadra1 è maggiore del valore di datoSquadra2 allora...
            {
                return 1; //Ritorna 1.
            }
            else if (datoSquadra1 == datoSquadra2) //Se il valore di datoSquadra1 è uguale del valore di datoSquadra2 allora...
            {
                return 0; //Ritorna 0.
            }
            else //Oppure...
            {
                return -1; //Ritorna -1.
            }
        }
    }
}
