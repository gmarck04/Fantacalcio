using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fantacalcio
{
    /**
     * \class fantaallenatore
     * \brief La classe fantaallenatore chiede in entrata un nome del fantaallenatore, i crediti del fantaallenatore.
     */
    [Serializable]
    class fantaallenatore
    {
        //Attributi
        [JsonProperty]
        /// \brief Nome del fantaallenatore
        protected string nome;
        /// \brief Numero di portieri posseduti dal fantaallenatore
        public int N_portieri;
        /// \brief Numero di difensori posseduti dal fantaallenatore
        public int N_difensori;
        /// \brief Numero di centrocampisti posseduti dal fantaallenatore
        public int N_centrocampisti;
        /// \brief Numero di attacanti posseduti dal fantaallenatore
        public int N_attacanti;
        [JsonProperty]
        /// \brief Numero di punti posseduti dal fantaallenatore
        protected double Fantaallenatore_punti;
        [JsonProperty]
        /// \brief Numero di crediti posseduti dal fantaallenatore
        protected int Fantaallenatore_crediti;
        [JsonProperty]
        /// \brief Lista di tipo fantacalciatore
        List<fantacalciatore> Lista_Calciatori = new List<fantacalciatore>();

        /**
        * \brief Metodo costruttore, riceve in input la stringa nome e la variabile di tipo int Fantaallenatore_crediti e pone la variabile di tipo int Fantaallenatore_punti uguale a 0.
        */
        public fantaallenatore(string nome, int Fantaallenatore_crediti)
        {
            this.nome = nome;
            this.Fantaallenatore_crediti = Fantaallenatore_crediti;
            this.Fantaallenatore_punti = 0;
        }

        /**
         * \fn      public void Diminuisci_credito(int credito)
         * \param   int credito: credito da inserire, quando la funzione viene chiamata
         * \brief   Diminuisce la variablie di tipo int Fantaallenatore_crediti per un valore pari alla variabile di tipo int credito
         */
        public void Diminuisci_credito(int credito)
        {
            Fantaallenatore_crediti -= credito;
        }

        /**
         * \fn      public void Aumenenta_credito(int credito)
         * \param   int credito: credito da inserire, quando la funzione viene chiamata
         * \brief   Aumenenta la variablie di tipo int Fantaallenatore_crediti per un valore pari alla variabile di tipo int credito
         */
        public void Aumenenta_credito(int credito)
        {
            Fantaallenatore_crediti += credito;
        }

        /**
         * \fn      public int Visualizza_credito()
         * \brief   Ritorna la variabile di tipo int Fantaallenatore_crediti.
         * \return  Fantaallenatore_crediti: Numero di crediti posseduti dal fantaallenatore
         */
        public int Visualizza_credito()
        {
            return Fantaallenatore_crediti;
        }

        /**
         * \fn      public void Inserisci_punti(double punti)
         * \param   double punti: punti da inserire, quando la funzione viene chiamata
         * \brief   Aumenenta la variablie di tipo int Fantaallenatore_punti per un valore pari alla variabile di tipo double punti
         */
        public void Inserisci_punti(double punti)
        {
            Fantaallenatore_punti += punti;
        }

        /**
         * \fn      public List<fantacalciatore> Mostra_Calciatori()
         * \brief   Ritorna la lista Lista_Calciatori.
         * \return  Lista_Calciatori: Lista di tipo fantacalciatore
         */
        public List<fantacalciatore> Mostra_Calciatori()
        {
            return Lista_Calciatori;
        }

        /**
         * \fn      public void Aggiungi_Calciatore(fantacalciatore calciatori)
         * \param   fantacalciatore calciatori: istanza calciatori di tipo fantacalciatore, nello specifico bisogna inviare una stringa per nome, per il cognome, per il ruolo e una variabile di tipo int prezzo
         * \brief   Con la funzione lista.Add aggiunge l'istanza calciatori di tipo fantacalciatore alla lista Lista_calciatori.
         */
        public void Aggiungi_Calciatore(fantacalciatore calciatori)
        {
            Lista_Calciatori.Add(calciatori);
        }

        /**
         * \fn      public void Rimuovi_Calciatore(string nome)
         * \param   string nome: nome da rimuovere dalla lista
         * \param   int i: viene posta uguale a 0
         * \brief   Rimuovo il nome inserito in ingresso dalla lista di calciatori
         * \details Con un ciclo for, che continua fino a quando la variabile i (la variabile i viene incrementata di uno ad ogni ciclo) non è maggiore della lunghezza della lista, 
         * con un if, che controlla quando il nome preso dalla lista è uguale al nome dato in entrata alla funzione e con la funzione lista.Remove 
         * rimuovo dalla lista Lista_calciatori il posto occupato dall'istanza posizionata sulla posizione i.
         */
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

        /**
         * \fn      public override string ToString() 
         * \brief   Ritorna la stringa nome.
         * \return  nome: Nome del fantaallenatore
         */
        public override string ToString()
        {
            return nome;
        }

        /**
         * \fn      public double Get_Fantaallenatore_punti()
         * \brief   Ritorna la variabile di tipo double Fantaallenatore_punti.
         * \return  Fantaallenatore_punti: Numero di punti posseduti dal fantaallenatore 
         */
        public double Get_Fantaallenatore_punti()
        {
            return Fantaallenatore_punti;
        }

        /**
         * \fn      public int Get_Fantaallenatore_crediti()
         * \brief   Ritorna la variabile di tipo int Fantaallenatore_crediti.
         * \return  Fantaallenatore_crediti: Cognome del calciatore.
         */
        public int Get_Fantaallenatore_crediti()
        {
            return Fantaallenatore_crediti;
        }

        /**
         * \fn      public int CompareTo(fantaallenatore squadra2)
         * \param   double datoSquadra1: viene posta uguale a 0
         * \param   double datoSquadra2: viene posta uguale a 0
         * \brief   Metodo che serve per comparare i punti di due diverse squadre
         * \details Pongo la variablie datoSquadra1 pari al valore restituito dal metodo Calcola_punti di questa classe, pongo la variablie datoSquadra2 pari al valore restituito dal metodo della squadra 2 Calcola_punti,
         * se il valore di datoSquadra1 è maggiore del valore di datoSquadra2 allora ritorna 1, se il valore di datoSquadra1 è uguale del valore di datoSquadra2 allora ritorna 0 oppure ritorna -1.
         */
        public int CompareTo(fantaallenatore squadra2)
        {
            double datoSquadra1 = 0, datoSquadra2 = 0;
            datoSquadra1 = this.Get_Fantaallenatore_punti();
            datoSquadra2 = squadra2.Get_Fantaallenatore_punti();

            if (datoSquadra1 > datoSquadra2)
            {
                return 1;
            }
            else if (datoSquadra1 == datoSquadra2)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
