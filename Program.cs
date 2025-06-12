using System;

namespace MayinTarlasi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mayın Tarlası Oyununa Hoşgeldiniz!");
            Console.WriteLine("Zorluk seviyelerini seçin:");
            Console.WriteLine("Kolay: 1");
            Console.WriteLine("Orta: 2");
            Console.WriteLine("Zor: 3");
            Console.Write("Seçiminizi yapın (1-3): ");

            string? secim = Console.ReadLine() ?? "1";
            IZorluk zorluk = secim switch
            {
                "2" => new Orta(),
                "3" => new Zor(),
                _ => new Kolay(),
            };

            Tahta tahta = new Tahta(zorluk.Satir, zorluk.Sutun, zorluk.MayinSayisi);

            while (!tahta.OyunBittiMi)
            {
                tahta.TahtayiYazdir();

                Console.WriteLine("İşlem seçin: \nAçmak için A tuşuna basın\nBayrak ekleme-kaldırma için İ tuşuna basın");
                string? islem = Console.ReadLine()?.ToUpper();

                int satir, sutun;

                while (true)
                {
                    Console.Write($"Satır girin (0 - {zorluk.Satir - 1}): ");
                    if (int.TryParse(Console.ReadLine(), out satir) && satir >= 0 && satir < zorluk.Satir) break;
                    Console.WriteLine("Geçersiz satır girdiniz, tekrar deneyin.");
                }

                while (true)
                {
                    Console.Write($"Sütun girin (0 - {zorluk.Sutun - 1}): ");
                    if (int.TryParse(Console.ReadLine(), out sutun) && sutun >= 0 && sutun < zorluk.Sutun) break;
                    Console.WriteLine("Geçersiz sütun girdiniz, tekrar deneyin.");
                }

                if (islem == "A" || islem == "a")
                {
                    tahta.HucreyiAc(satir, sutun);
                }
                else if (islem == "İ" || islem == "I" || islem == "i" || islem == "ı") // İ ve I harflerini kabul et
                {
                    tahta.HucreyiIsaretle(satir, sutun);
                }
                else
                {
                    Console.WriteLine("Geçersiz işlem seçildi, tur atlandı.");
                }
            }

            tahta.TahtayiYazdir(true);

            if (tahta.KaybettiMi)
                Console.WriteLine("Mayına bastın! Kaybettin.");
            else
                Console.WriteLine("Tüm güvenli hücreleri açtın! Kazandın!");

            Console.WriteLine("Oyunu kapatmak için bir tuşa bas...");
            Console.ReadKey();
        }
    }
}
