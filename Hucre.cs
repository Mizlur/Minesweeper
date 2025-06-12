using System;

namespace MayinTarlasi
{
    class Hucre
    {
        public bool MayinVar { get; set; }
        private bool _acik;
        private bool _isaretli;

        public bool Acik   //Açık noktayı kapatamazsın set yok
        {
            get => _acik;
            private set => _acik = value;
        }

        public bool Isaretli   //Bayrak kaldırıp açma var get set
        {
            get => _isaretli;
            set => _isaretli = value;
        }

        public Hucre()
        {
            MayinVar = false;
            _acik = false;
            _isaretli = false;
        }

        public void Ac()
        {
            if (!_acik && !_isaretli)
                _acik = true;
        }
    }
}
