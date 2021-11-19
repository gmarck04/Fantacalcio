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
        protected string nome;
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
            return $"nome: {nome}";
        }

        public string Get_nome()
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
    }
}
