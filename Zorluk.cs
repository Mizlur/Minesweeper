namespace MayinTarlasi
{
    interface IZorluk
    {
        int Satir { get; }
        int Sutun { get; }
        int MayinSayisi { get; }
    }

    class Kolay : IZorluk
    {
        public int Satir => 8;
        public int Sutun => 8;
        public int MayinSayisi => 10;
    }

    class Orta : IZorluk
    {
        public int Satir => 12;
        public int Sutun => 12;
        public int MayinSayisi => 20;
    }

    class Zor : IZorluk
    {
        public int Satir => 16;
        public int Sutun => 16;
        public int MayinSayisi => 40;
    }
}
