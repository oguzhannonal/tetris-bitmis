using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Keys;
using  System.Windows;
using System.Windows.Forms.VisualStyles;

namespace _180707064_oguzhan_onal
{
    public partial class Form1 : Form
    {
        public static Label[][] harita = new Label[10][]; //10 a 20 label oluşturuyoruz oyun alanı için
        private const int bosluk = 40; //label boyutları
        private int sekil = 0; // hangi sekil onu kontrol ediyoruz
        private bool bitti_mi = false; // oyunun bitip bitmediğini kontrol için bool
        private bool durdurumu = false; // oyun durdu mu kontrol 
        private int hangitaraf = 0; // 0 sag 1 sol hangi tarafa dondugunu kaydediyoruz

        struct Koordinatlar // labellerin koordinatlarını tutacak struct
        {
            public int x;
            public int y;
        }

        Koordinatlar[] koordinatlar = new Koordinatlar[8]; // dizi haline getiriyoruz

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_ekle();
        }

        private void label_ekle() // labelleri 10 a 20 oluşturup tag ve renklerini hallediyoruz
        {
            for (int i = 0; i < 10; i++)
            {
                harita[i] = new Label[20];
                for (int j = 0; j < 20; j++)
                {
                    harita[i][j] = new Label();

                    harita[i][j].BackColor = Color.Black;
                    harita[i][j].Location = new System.Drawing.Point(i * bosluk, j * bosluk);
                    harita[i][j].Tag = 0;
                    harita[i][j].Size = new System.Drawing.Size(bosluk, bosluk);
                    
                }

                this.Controls.AddRange(harita[i]); // forma labelleri ekliyoruz
            }
        }

        
        

        private bool oyun_bitti_mi() // en ust satirda tag=1 olan label varsa oyun bitiyor
        {
            for (int i = 0; i < 10; i++)
            {
                if ((int) harita[i][0].Tag == 1)
                {
                    bitti_mi = true;
                }
            }

            if (bitti_mi)
            {
               
                timer1.Stop();
                timer2.Stop();
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void baslangic_sekil_uret() // random bir şekilde şekilleri uretiyoruz timerda bunları aşağıya indereceğiz
        {
            Random rand = new Random();
            int a = rand.Next(0, 5);
            switch (a)
            {
                case 0: // cizgi uret
                    harita[4][0].BackColor = Color.Red; // x,y
                    harita[5][0].BackColor = Color.Red; // x+1,y
                    harita[6][0].BackColor = Color.Red; // x+2,y
                    harita[7][0].BackColor = Color.Red; // x+3,y
                    koordinatlar[0].x = 4;
                    koordinatlar[0].y = 0;
                    koordinatlar[1].x = 5;
                    koordinatlar[1].y = 0;
                    koordinatlar[2].x = 6;
                    koordinatlar[2].y = 0;
                    koordinatlar[3].x = 7;
                    koordinatlar[3].y = 0;
                    sekil = 0; // cizgi
                    break;
                case 1: // l uret
                    harita[4][0].BackColor = Color.Red; // x,y
                    harita[4][1].BackColor = Color.Red; // x,y+1
                    harita[5][1].BackColor = Color.Red; // x+1,y+1
                    harita[6][1].BackColor = Color.Red; // x+2,y+1
                    koordinatlar[0].x = 4;
                    koordinatlar[0].y = 0;
                    koordinatlar[1].x = 4;
                    koordinatlar[1].y = 1;
                    koordinatlar[2].x = 5;
                    koordinatlar[2].y = 1;
                    koordinatlar[3].x = 6;
                    koordinatlar[3].y = 1;
                    sekil = 1; // l
                    break;
                case 2: // kare
                    harita[4][0].BackColor = Color.Red; // x,y
                    harita[5][0].BackColor = Color.Red; // x+1,y
                    harita[4][1].BackColor = Color.Red; // x,y+1
                    harita[5][1].BackColor = Color.Red; // x+1,y+1
                    koordinatlar[0].x = 4;
                    koordinatlar[0].y = 0;
                    koordinatlar[1].x = 5;
                    koordinatlar[1].y = 0;
                    koordinatlar[2].x = 4;
                    koordinatlar[2].y = 1;
                    koordinatlar[3].x = 5;
                    koordinatlar[3].y = 1;
                    sekil = 2; // kare 
                    break;
                case 3: // z
                    harita[4][0].BackColor = Color.Red; //x,y
                    harita[5][0].BackColor = Color.Red; // x+1,y
                    harita[4][1].BackColor = Color.Red; // x,y+1
                    harita[3][1].BackColor = Color.Red; // x-1,y+1
                    koordinatlar[0].x = 4;
                    koordinatlar[0].y = 0;
                    koordinatlar[1].x = 5;
                    koordinatlar[1].y = 0;
                    koordinatlar[2].x = 4;
                    koordinatlar[2].y = 1;
                    koordinatlar[3].x = 3;
                    koordinatlar[3].y = 1;
                    sekil = 3; // z
                    break;
                case 4: // piramit
                    harita[4][0].BackColor = Color.Red; // x,y
                    harita[3][1].BackColor = Color.Red; // x-1,y+1
                    harita[4][1].BackColor = Color.Red; // x,+1
                    harita[5][1].BackColor = Color.Red; // x+1,y+1
                    koordinatlar[0].x = 4;
                    koordinatlar[0].y = 0;
                    koordinatlar[1].x = 3;
                    koordinatlar[1].y = 1;
                    koordinatlar[2].x = 4;
                    koordinatlar[2].y = 1;
                    koordinatlar[3].x = 5;
                    koordinatlar[3].y = 1;
                    sekil = 4; // piramit
                    break;
            }
        }

        private void sekil_ciz(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)//koordinatları alıp şekli çiziyoruz harita dizimize
        {
            
            harita[x][y].BackColor = Color.Blue; // x,y
            harita[x1][y1].BackColor = Color.Blue; // x-1,y+1
            harita[x2][y2].BackColor = Color.Blue; // x,+1
            harita[x3][y3].BackColor = Color.Blue; // x+1,y+1
            harita[x][y].Tag = 1; // x,y
            harita[x1][y1].Tag = 1; // x-1,y+1
            harita[x2][y2].Tag = 1; // x,+1
            harita[x3][y3].Tag = 1; // x+1,y+1
        }

        private void eski_hali_sil(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3) // timer tick inde veya sag sol donme harekette sekli siliyoruz
        {
            harita[x][y].BackColor = Color.Black; // x,y
            harita[x1][y1].BackColor = Color.Black; // x-1,y+1
            harita[x2][y2].BackColor = Color.Black; // x,+1
            harita[x3][y3].BackColor = Color.Black; // x+1,y+1
            harita[x][y].Tag = 0; // x,y
            harita[x1][y1].Tag = 0; // x-1,y+1
            harita[x2][y2].Tag = 0; // x,+1
            harita[x3][y3].Tag = 0; // x+1,y+1
        }

        private bool hareket_edebebilir_mi(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)//aşağıda cisim varsa durduruyoruz
        {
          
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            if (koordinatlar[0].y < 19 && koordinatlar[1].y < 19 && koordinatlar[2].y < 19 && koordinatlar[3].y < 19 && //sınır kontrol
                koordinatlar[0].x <= 9 && koordinatlar[1].x <= 9 && koordinatlar[2].x <= 9 && koordinatlar[3].x <= 9)
            {
                if ((int) harita[x][y + 1].Tag == 1) a = false; //  bir aşağısında kendine ait parça varsa durmasını engelliyoruz
                if ((int) harita[x1][y1 + 1].Tag == 1) b = false;
                if ((int) harita[x2][y2 + 1].Tag == 1) c = false;
                if ((int) harita[x3][y3 + 1].Tag == 1) d = false;
                if (x == x1 && y + 1 == y1 || x == x2 && y + 1 == y2 || x == x3 && y + 1 == y3) a = true; // durma kontrol
                if (x1 == x && y1 + 1 == y || x1 == x2 && y1 + 1 == y2 || x1 == x3 && y1 + 1 == y3) b = true;
                if (x2 == x1 && y2 + 1 == y1 || x2 == x && y2 + 1 == y || x2 == x3 && y2 + 1 == y3) c = true;
                if (x3 == x && y3 + 1 == y || x3 == x2 && y3 + 1 == y2 || x3 == x1 && y3 + 1 == y1) d = true;
            }

            if (a && b && c && d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = (skor + skorzaman).ToString();
            timer1.Interval = 200;
            if (koordinatlar[0].y < 19 && koordinatlar[1].y < 19 && koordinatlar[2].y < 19 && koordinatlar[3].y < 19)//sınırlar
            {
                eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                    koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);//eski hali siliyoruz
                if ((hareket_edebebilir_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                    koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y)))//hareket edebiliyrse y sini arttırıyoruz
                {
                    koordinatlar[0].y++;
                    koordinatlar[1].y++;
                    koordinatlar[2].y++;
                    koordinatlar[3].y++;
                    sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                        koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);//çiziyoruz
                }
                else
                {
                    durdurumu = true;
                }

                sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y, koordinatlar[2].x,
                    koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
            }
            else
            {
                durdurumu = true;
            }
            
            if (durdurumu)//durduysa başka şekil geliyor
            {
                timer1.Stop();
                timer1.Start();

                if (oyun_bitti_mi())//oyun bitti mi kontrol
                {
                    MessageBox.Show("Oyun bitti skor:" + (skor+skorzaman).ToString());
                }
              
                else
                { 
                   
                    baslangic_sekil_uret();
                    satir_sil(); // silinecek satir varsa siliyoruz
                    durdurumu = false;
                }
            }
        }
        
        

        private bool Dondurulebilir_mi(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)//donerken sınırları ihlal ediyorsa dondurmuyoruz
        {
            
            if (sekil == 0)//şekil çizgiyse merkezi farklı oldugu icin farklı kontrol ediyoruz
            {
                Point merkezPoint = new Point(x, y);
                Point dondurelecek1 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                if (hangitaraf == 0)//sag
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90);
                }
                else if (hangitaraf == 1)//sol
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90);
                }

                if ((dondurelecek1.Y < 19 && dondurelecek2.Y < 19 && dondurelecek3.Y < 19 && dondurelecek1.X < 9 &&
                     dondurelecek2.X < 9 && dondurelecek3.X < 9 && dondurelecek1.X > 0 && dondurelecek2.X > 0 &&
                     dondurelecek3.X > 0 && dondurelecek1.Y > 0 && dondurelecek2.Y > 0 && dondurelecek3.Y > 0))//sınır kontroller
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (sekil == 3)// z için
            {
                Point merkezPoint = new Point(x2, y2);
                Point dondurelecek1 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                if (hangitaraf == 0)
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90);
                }
                else if (hangitaraf == 1)
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90);
                }

                if ((dondurelecek1.Y < 19 && dondurelecek2.Y < 19 && dondurelecek3.Y < 19 && dondurelecek1.X < 9 &&
                     dondurelecek2.X < 9 && dondurelecek3.X < 9 && dondurelecek1.X > 0 && dondurelecek2.X > 0 &&
                     dondurelecek3.X > 0 && dondurelecek1.Y > 0 && dondurelecek2.Y > 0 && dondurelecek3.Y > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else// digerleri için
            {
                Point merkezPoint = new Point(x1, y1);
                Point dondurelecek1 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                if (hangitaraf == 0)
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90);
                }
                else if (hangitaraf == 1)
                {
                    dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90);
                    dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90);
                    dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90);
                }

                if ((dondurelecek1.Y < 19 && dondurelecek2.Y < 19 && dondurelecek3.Y < 19 && dondurelecek1.X < 9 &&
                     dondurelecek2.X < 9 && dondurelecek3.X < 9 && dondurelecek1.X > 0 && dondurelecek2.X > 0 &&
                     dondurelecek3.X > 0 && dondurelecek1.Y > 0 && dondurelecek2.Y > 0 && dondurelecek3.Y > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static Point Nokta_Nokta_Etrafında_Dondur(Point dondurelecek_nokta, Point merkez_nokta,
            double kac_derece)// bir noktayı nokta etrafında dondurme formulu c# a aktardım
        {
            double kacRadyan = kac_derece * (Math.PI / 180);
            double costeta = Math.Cos(kacRadyan);
            double sinteta = Math.Sin(kacRadyan);
            return new Point
            {
                X = (int) (costeta * (dondurelecek_nokta.X - merkez_nokta.X) -
                           sinteta * (dondurelecek_nokta.Y - merkez_nokta.Y) + merkez_nokta.X),
                Y = (int) (sinteta * (dondurelecek_nokta.X - merkez_nokta.X) +
                           costeta * (dondurelecek_nokta.Y - merkez_nokta.Y) + merkez_nokta.Y)
            };
        }

        private bool dondugu_yerde_sekil_var_mi(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)//sekiller iç içe girmesin diye dondugunde sekil var mı kontrol
        {
            switch (sekil)//sekile gore dondugu yerde tag ı 1 olan label vaarsa dondurmuyoruz
            {
                case 0:
                    Point merkezPoint = new Point(x, y);
                    Point dondurelecek1 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                    Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                    Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                    if (hangitaraf == 0)
                    {
                        dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90);
                        dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90);
                        dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90);
                    }
                    else if (hangitaraf == 1)
                    {
                        dondurelecek1 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90);
                        dondurelecek2 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90);
                        dondurelecek3 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90);
                    }

                    if ((int) harita[dondurelecek1.X][dondurelecek1.Y].Tag != 1 &&
                        (int) harita[dondurelecek2.X][dondurelecek2.Y].Tag != 1 &&
                        (int) harita[dondurelecek3.X][dondurelecek3.Y].Tag != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 1:
                    Point merkezPoint1 = new Point(x1, y1);
                    Point dondurelecek11 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                    Point dondurelecek21 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                    Point dondurelecek31 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                    if (hangitaraf == 0)
                    {
                        dondurelecek11 = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, 90);
                        dondurelecek21 = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, 90);
                        dondurelecek31 = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, 90);
                        if ((int) harita[dondurelecek21.X][dondurelecek21.Y].Tag != 1 &&
                            (int) harita[dondurelecek31.X][dondurelecek31.Y].Tag != 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (hangitaraf == 1)
                    {
                        dondurelecek11 = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, -90);
                        dondurelecek21 = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, -90);
                        dondurelecek31 = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, -90);
                        if ((int) harita[dondurelecek11.X][dondurelecek11.Y].Tag != 1 &&
                            (int) harita[dondurelecek31.X][dondurelecek31.Y].Tag != 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    break;
                case 3:
                    Point merkezPoint11 = new Point(x2, y2);
                    Point dondurelecek111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                    Point dondurelecek211 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                    Point dondurelecek311 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                    if (hangitaraf == 0)
                    {
                        dondurelecek111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, 90);
                        dondurelecek211 = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, 90);
                        dondurelecek311 = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, 90);
                        if ((int) harita[dondurelecek211.X][dondurelecek211.Y].Tag != 1 &&
                            (int) harita[dondurelecek111.X][dondurelecek111.Y].Tag != 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (hangitaraf == 1)
                    {
                        dondurelecek111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, -90);
                        dondurelecek211 = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, -90);
                        dondurelecek311 = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, -90);
                        if ((int) harita[dondurelecek311.X][dondurelecek311.Y].Tag != 1 &&
                            (int) harita[dondurelecek211.X][dondurelecek211.Y].Tag != 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    break;

                case 4:
                    Point merkezPoint111 = new Point(x1, y1);
                    Point dondurelecek1111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                    Point dondurelecek2111 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                    Point dondurelecek3111 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                    if (hangitaraf == 0)
                    {
                        dondurelecek1111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, 90);
                        dondurelecek2111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, 90);
                        dondurelecek3111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, 90);
                    }
                    else if (hangitaraf == 1)
                    {
                        dondurelecek1111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, -90);
                        dondurelecek2111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, -90);
                        dondurelecek3111 = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, -90);
                    }

                    if ((int) harita[dondurelecek2111.X][dondurelecek2111.Y].Tag != 1 &&
                        (int) harita[dondurelecek1111.X][dondurelecek1111.Y].Tag != 1 &&
                        (int) harita[dondurelecek3111.X][dondurelecek3111.Y].Tag != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return true;
        }

        private void saga_dondur()//sekilleri silip dondurup çiziyoruz
        {
            int x;
            int y;
            int gecici = 0;
            switch (sekil)
            {
                case 0:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[0].x;
                        y = koordinatlar[0].y;
                        Point merkezPoint = new Point(x, y);
                        Point dondurelecek1 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                        Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[1].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90).X;
                        koordinatlar[1].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, 90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, 90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, 90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 1:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[1].x;
                        y = koordinatlar[1].y;

                        Point merkezPoint1 = new Point(x, y);
                        Point dondurelecek11 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek21 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek31 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, 90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, 90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, 90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, 90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, 90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, 90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 2:
                    break;
                case 3:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[2].x;
                        y = koordinatlar[2].y;
                        Point merkezPoint11 = new Point(x, y);
                        Point dondurelecek111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek211 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                        Point dondurelecek311 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, 90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, 90).Y;
                        koordinatlar[1].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, 90).X;
                        koordinatlar[1].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, 90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, 90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, 90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 4:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[1].x;
                        y = koordinatlar[1].y;
                        Point merkezPoint111 = new Point(x, y);
                        Point dondurelecek1111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek2111 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek3111 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, 90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, 90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, 90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, 90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, 90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, 90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
            }
        }

        private void sola_dondur()// sag a uygulanan işlemin -90 versiyonu
        {
            int x;
            int y;
            int gecici = 0;
            switch (sekil)
            {
                case 0:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[0].x;
                        y = koordinatlar[0].y;
                        Point merkezPoint = new Point(x, y);
                        Point dondurelecek1 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                        Point dondurelecek2 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek3 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[1].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90).X;
                        koordinatlar[1].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek1, merkezPoint, -90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek2, merkezPoint, -90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek3, merkezPoint, -90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 1:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[1].x;
                        y = koordinatlar[1].y;

                        Point merkezPoint1 = new Point(x, y);
                        Point dondurelecek11 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek21 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek31 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, -90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek11, merkezPoint1, -90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, -90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek21, merkezPoint1, -90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, -90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek31, merkezPoint1, -90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 2:
                    break;
                case 3:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[2].x;
                        y = koordinatlar[2].y;
                        Point merkezPoint11 = new Point(x, y);
                        Point dondurelecek111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek211 = new Point(koordinatlar[1].x, koordinatlar[1].y);
                        Point dondurelecek311 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, -90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek111, merkezPoint11, -90).Y;
                        koordinatlar[1].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, -90).X;
                        koordinatlar[1].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek211, merkezPoint11, -90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, -90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek311, merkezPoint11, -90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
                case 4:
                    if (dondugu_yerde_sekil_var_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                        koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                        x = koordinatlar[1].x;
                        y = koordinatlar[1].y;
                        Point merkezPoint111 = new Point(x, y);
                        Point dondurelecek1111 = new Point(koordinatlar[0].x, koordinatlar[0].y);
                        Point dondurelecek2111 = new Point(koordinatlar[2].x, koordinatlar[2].y);
                        Point dondurelecek3111 = new Point(koordinatlar[3].x, koordinatlar[3].y);
                        koordinatlar[0].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, -90).X;
                        koordinatlar[0].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek1111, merkezPoint111, -90).Y;
                        koordinatlar[2].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, -90).X;
                        koordinatlar[2].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek2111, merkezPoint111, -90).Y;
                        koordinatlar[3].x = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, -90).X;
                        koordinatlar[3].y = Nokta_Nokta_Etrafında_Dondur(dondurelecek3111, merkezPoint111, -90).Y;
                        sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                            koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                    }

                    break;
            }
        }

     
        private int c = 0;//satir kontrol 10 sa silinecek
       
        private bool doldumu = false; //satir doldu mu
        int skor = 0; // skor

        private void satir_sil()
        {
            c = 0;
            
            int kacsatir = 0; // kac satirin silinecegi
            int kacincisatirda = 0; // kacinci satir silinecek

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((int) harita[j][i].Tag == 1)
                    {
                        c++;
                    }

                    if (c == 10)// 10 tane mavi varsa silinecek
                    {
                        skor = skor + 100;
                        label2.Text = skor.ToString();
                        doldumu = true;
                        kacsatir++;
                        kacincisatirda = i;
                    }
                }

                c = 0;
            }

            if (doldumu)
            {
                if (kacincisatirda != 19) //ortalardaysa dolan yer ayrı bir şekilde silecegiz
                {
                   

                    while (kacsatir != 0)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if ((int) harita[j][kacincisatirda].Tag == 1)//satirda ki labelleri siliyoruz kac satir varsa dolan
                            {
                                harita[j][kacincisatirda].BackColor = Color.Black;
                                harita[j][kacincisatirda].Tag = 0;
                            }
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = kacincisatirda; j > 0; j--) // ustte kalan labelleri 1 kaydırıyoruz
                            {
                                if ((int)harita[i][j].Tag == 1)
                                {
                                    harita[i][j].Tag = 0;
                                    harita[i][j].BackColor = Color.Black;
                                    harita[i][j + 1].Tag = 1;
                                    harita[i][j + 1].BackColor = Color.Blue;
                                }
                            }
                        }

                        kacsatir--;
                    }

                   
                }
                else
                {
                    for (int i = kacincisatirda; i >= 20 - kacsatir; i--) //aşağıdan başlıyorsa silinecek yerler
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if ((int) harita[j][i].Tag == 1)
                            {
                                harita[j][i].BackColor = Color.Black;
                                harita[j][i].Tag = 0;
                            }
                        }
                    }

                    while (kacsatir != 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = kacincisatirda - 1; j > 0; j--)
                            {
                                if ((int) harita[i][j].Tag == 1)
                                {
                                    harita[i][j].Tag = 0;
                                    harita[i][j].BackColor = Color.Black;
                                    harita[i][j + 1].Tag = 1;
                                    harita[i][j + 1].BackColor = Color.Blue;
                                }
                            }
                        }

                        kacsatir--;
                    }
                }

                doldumu = false;
            }
        }

        private bool sag_sol_hareket_edebilirmi(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3) // sag sol hareket ederken sınır veya farklı bir cisim varsa duruyoruz
        {
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            if (koordinatlar[0].x < 9 && koordinatlar[1].x < 9 && koordinatlar[2].x < 9 && koordinatlar[3].x < 9 &&
                koordinatlar[0].x > 0 && koordinatlar[1].x > 0 && koordinatlar[2].x > 0 && koordinatlar[3].x > 0)
            {
                if ((int) harita[x + 1][y].Tag == 1) a = false;
                if ((int) harita[x1 + 1][y1].Tag == 1) b = false;
                if ((int) harita[x2 + 1][y2].Tag == 1) c = false;
                if ((int) harita[x3 + 1][y3].Tag == 1) d = false;
                if (x + 1 == x1 && y == y1 || x + 1 == x2 && y == y2 || x + 1 == x3 && y == y3) a = true;
                if (x1 + 1 == x && y1 == y || x1 + 1 == x2 && y1 == y2 || x1 + 1 == x3 && y1 == y3) b = true;
                if (x2 + 1 == x1 && y2 == y1 || x2 + 1 == x && y2 == y || x2 + 1 == x3 && y2 == y3) c = true;
                if (x3 + 1 == x && y3 == y || x3 + 1 == x2 && y3 == y2 || x3 + 1 == x1 && y3 == y1) d = true;
            }

            if (a && b && c && d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool sol_hareket_edebilirmi(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)//sol için ayrı kontrol
        {
            bool a = true;
            bool b = true;
            bool c = true;
            bool d = true;
            if (koordinatlar[0].x < 9 && koordinatlar[1].x < 9 && koordinatlar[2].x < 9 && koordinatlar[3].x < 9 &&
                koordinatlar[0].x > 0 && koordinatlar[1].x > 0 && koordinatlar[2].x > 0 && koordinatlar[3].x > 0)
            {
                if ((int) harita[x - 1][y].Tag == 1) a = false;
                if ((int) harita[x1 - 1][y1].Tag == 1) b = false;
                if ((int) harita[x2 - 1][y2].Tag == 1) c = false;
                if ((int) harita[x3 - 1][y3].Tag == 1) d = false;
                if (x - 1 == x1 && y == y1 || x - 1 == x2 && y == y2 || x - 1 == x3 && y == y3) a = true;
                if (x1 - 1 == x && y1 == y || x1 - 1 == x2 && y1 == y2 || x1 - 1 == x3 && y1 == y3) b = true;
                if (x2 - 1 == x1 && y2 == y1 || x2 - 1 == x && y2 == y || x2 - 1 == x3 && y2 == y3) c = true;
                if (x3 - 1 == x && y3 == y || x3 - 1 == x2 && y3 == y2 || x3 - 1 == x1 && y3 == y1) d = true;
            }

            if (a && b && c && d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Sag_Sol_Hareket(object sender, PreviewKeyDownEventArgs e)//hangi tusa bastigimiz algılanıyor
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (koordinatlar[0].x < 9 && koordinatlar[1].x < 9 && koordinatlar[2].x < 9 &&
                        koordinatlar[3].x < 9)
                    {
                        if (sag_sol_hareket_edebilirmi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                            koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x,
                            koordinatlar[3].y))
                        {
                            eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                                koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                            if (hareket_edebebilir_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                                koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x,
                                koordinatlar[3].y))
                            {
                                koordinatlar[0].x++;
                                koordinatlar[1].x++;
                                koordinatlar[2].x++;
                                koordinatlar[3].x++;
                                sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                                    koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                            }
                        }
                    }

                    break;
                case Keys.Left:
                    if (koordinatlar[0].x > 0 && koordinatlar[1].x > 0 && koordinatlar[2].x > 0 &&
                        koordinatlar[3].x > 0)
                    {
                        if (sol_hareket_edebilirmi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                            koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x,
                            koordinatlar[3].y))
                        {
                            eski_hali_sil(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                                koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                            if (hareket_edebebilir_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x,
                                koordinatlar[1].y, koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x,
                                koordinatlar[3].y))
                            {
                                koordinatlar[0].x--;
                                koordinatlar[1].x--;
                                koordinatlar[2].x--;
                                koordinatlar[3].x--;
                                sekil_ciz(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                                    koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y);
                            }
                        }
                    }

                    break;
                case Keys.Up:
                    hangitaraf = 0;
                    if (Dondurulebilir_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                        koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        saga_dondur();
                    }

                    break;
                case Keys.Down:
                    hangitaraf = 1;
                    if (Dondurulebilir_mi(koordinatlar[0].x, koordinatlar[0].y, koordinatlar[1].x, koordinatlar[1].y,
                        koordinatlar[2].x, koordinatlar[2].y, koordinatlar[3].x, koordinatlar[3].y))
                    {
                        sola_dondur();
                    }

                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)//oyunu baslatma
        {
            timer1.Start();
            timer2.Start();
            baslangic_sekil_uret();
        }

        private void pictureBox2_Click(object sender, EventArgs e) // formu bastan baslatma
        {
            
            Application.Restart();
           

        }

        private int skorzaman = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            skorzaman++;
        }
    }
    }

