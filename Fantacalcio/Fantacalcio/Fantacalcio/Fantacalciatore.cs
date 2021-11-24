using Newtonsoft.Json;

namespace Fantacalcio
{
    /**
     * \class fantacalciatore
     * \brief La classe fantacalciatore chiede in entrata un nome, un ruolo e un prezzo per il calciatore.
     */
    class fantacalciatore
    {
        [JsonProperty]
        /// \brief Nome del calciatore
        protected string nome;
        [JsonProperty]
        /// \brief Cognome del calciatore
        protected string cognome;
        [JsonProperty]
        /// \brief Ruolo del calciatore
        protected string ruolo;
        [JsonProperty]
        /// \brief Prezzo del calciatore
        protected int prezzo;

        /**
        * \brief Metodo costruttore, riceve in input la stringa nome, cognome, ruolo e la variabile di tipo int prezzo.
        */
        public fantacalciatore(string nome, string cognome, string ruolo, int prezzo)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.ruolo = ruolo;
            this.prezzo = prezzo;
        }

        /**
         * \fn      public string Get_nome()
         * \brief   Ritorna la stringa nome.
         * \return  nome: Nome del calciatore.
         */
        public string Get_nome()
        {
            return nome;
        }

        /**
         * \fn      public string Get_cognome()
         * \brief   Ritorna la stringa cognome.
         * \return  cognome: Cognome del calciatore.
         */
        public string Get_cognome()
        {
            return cognome;
        }

        /**
         * \fn      public string Get_ruolo()
         * \brief   Ritorna la stringa ruolo.
         * \return  ruolo: Ruolo del calciatore.
         */
        public string Get_ruolo()
        {
            return ruolo;
        }

        /**
         * \fn      public int Get_prezzo()
         * \brief   Ritorna la stringa prezzo.
         * \return  prezzo: Prezzo del calciatore.
         */
        public int Get_prezzo()
        {
            return prezzo;
        }

        /**
         * \fn      public override string ToString()
         * \brief   Ritorna la stringa che comunica il nome, il ruolo ed il prezzo del fantacalciatore.
         * \return  string: Il metodo ritorna una stringa che comunica il nome, il ruolo e il prezzo del fantacalciatore.
         */
        public override string ToString()
        {
            return $"nome: {nome}, ruolo: {ruolo}, prezzo: {prezzo}";
        }
    }
}
