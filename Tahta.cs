// Tahta.cs
using System;

namespace MayinTarlasi
{
    class Tahta
    {
        private Hucre[,] _hucreler;
        private int _satirSayisi;
        private int _sutunSayisi;
        private int _mayinSayisi;
        private int _acilanHucreSayisi;
        private bool _kaybetti;
        public bool OyunBittiMi => _kaybetti || _acilanHucreSayisi == (_satirSayisi * _sutunSayisi - _mayinSayisi);
        public bool KazandiMi => !_kaybetti;
        public bool KaybettiMi => _kaybetti;

        public Tahta(int satir, int sutun, int mayinSayisi)
        {
            _satirSayisi = satir;
            _sutunSayisi = sutun;
            _mayinSayisi = mayinSayisi;
            _acilanHucreSayisi = 0;
            _kaybetti = false;
            _hucreler = new Hucre[satir, sutun];
            for (int i = 0; i < satir; i++)
                for (int j = 0; j < sutun; j++)
                    _hucreler[i, j] = new Hucre();

            MayinlariYerlestir();
        }

        private void MayinlariYerlestir()
        {
            Random rnd = new Random();
            int mayinKoyuldu = 0;
            while (mayinKoyuldu < _mayinSayisi)
            {
                int r = rnd.Next(_satirSayisi);
                int c = rnd.Next(_sutunSayisi);
                if (!_hucreler[r, c].MayinVar)
                {
                    _hucreler[r, c].MayinVar = true;
                    mayinKoyuldu++;
                }
            }
        }

        public void HucreyiAc(int satir, int sutun)
        {
            if (satir < 0 || satir >= _satirSayisi || sutun < 0 || sutun >= _sutunSayisi)
            {
                Console.WriteLine("Geçersiz koordinat!");
                return;
            }

            Hucre secilen = _hucreler[satir, sutun];
            if (secilen.Acik || secilen.Isaretli)
                return;

            secilen.Ac();

            if (secilen.MayinVar)
            {
                _kaybetti = true;
                return;
            }

            _acilanHucreSayisi++;

            if (CevredekiMayinSayisi(satir, sutun) == 0)
            {
                for (int i = satir - 1; i <= satir + 1; i++)
                {
                    for (int j = sutun - 1; j <= sutun + 1; j++)
                    {
                        if (i == satir && j == sutun) continue;
                        if (i >= 0 && i < _satirSayisi && j >= 0 && j < _sutunSayisi)
                        {
                            if (!_hucreler[i, j].Acik)
                                HucreyiAc(i, j);
                        }
                    }
                }
            }
        }

        public void HucreyiIsaretle(int satir, int sutun)
        {
            if (satir < 0 || satir >= _satirSayisi || sutun < 0 || sutun >= _sutunSayisi)
            {
                Console.WriteLine("Geçersiz koordinat!");
                return;
            }

            Hucre secilen = _hucreler[satir, sutun];

            if (secilen.Acik)
            {
                Console.WriteLine("Açık hücre işaretlenemez.");
                return;
            }

            secilen.Isaretli = !secilen.Isaretli;
        }

        private int CevredekiMayinSayisi(int satir, int sutun)
        {
            int sayi = 0;
            for (int i = satir - 1; i <= satir + 1; i++)
            {
                for (int j = sutun - 1; j <= sutun + 1; j++)
                {
                    if (i == satir && j == sutun) continue;
                    if (i >= 0 && i < _satirSayisi && j >= 0 && j < _sutunSayisi)
                    {
                        if (_hucreler[i, j].MayinVar)
                            sayi++;
                    }
                }
            }
            return sayi;
        }

        public void TahtayiYazdir(bool tumunuGoster = false)
        {
            Console.Write("   ");
            for (int c = 0; c < _sutunSayisi; c++)
                Console.Write($"{c} ");
            Console.WriteLine();

            for (int r = 0; r < _satirSayisi; r++)
            {
                Console.Write($"{r}  ");
                for (int c = 0; c < _sutunSayisi; c++)
                {
                    Hucre h = _hucreler[r, c];
                    if (tumunuGoster)
                    {
                        if (h.MayinVar)
                            Console.Write("* ");
                        else
                        {
                            int m = CevredekiMayinSayisi(r, c);
                            Console.Write(m > 0 ? $"{m} " : ". ");
                        }
                    }
                    else
                    {
                        if (h.Acik)
                        {
                            int m = CevredekiMayinSayisi(r, c);
                            Console.Write(m > 0 ? $"{m} " : ". ");
                        }
                        else if (h.Isaretli)
                            Console.Write("F ");
                        else
                            Console.Write("# ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
