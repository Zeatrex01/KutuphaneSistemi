using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class LibrarySystem : MonoBehaviour
{
    public TMP_InputField baslikInput;
    public TMP_InputField yazarInput;
    public TMP_InputField anahtarInput;

    public TextMeshProUGUI sonucText;

    public Transform kitapPaneli; // Kitap bilgilerinin g�r�nt�lendi�i panel
    public GameObject kitapBilgisiPrefab;

    private List<Kitap> kitaplar = new List<Kitap>();

    [Serializable]
    public class Kitap
    {
        public string Baslik;
        public string Yazar;
        public string ISBN;
        public int KopyaSayisi;
        public int OduncAlinanKopyaSayisi;

        public Kitap(string baslik, string yazar, string isbn, int kopyaSayisi)
        {
            Baslik = baslik;
            Yazar = yazar;
            ISBN = isbn;
            KopyaSayisi = kopyaSayisi;
            OduncAlinanKopyaSayisi = 0;
        }
    }

    private void EkleKitapListesi(Kitap kitap)
    {
        GameObject kitapBilgisi = Instantiate(kitapBilgisiPrefab, kitapPaneli);
        kitapBilgisi.SetActive(true);

        // Kitap bilgilerini prefab i�indeki TextMeshProUGUI elemanlar�na ata
        kitapBilgisi.transform.Find("BaslikText").GetComponent<TextMeshProUGUI>().text = $"Ba�l�k: {kitap.Baslik}";
        kitapBilgisi.transform.Find("YazarText").GetComponent<TextMeshProUGUI>().text = $"Yazar: {kitap.Yazar}";
        kitapBilgisi.transform.Find("ISBNText").GetComponent<TextMeshProUGUI>().text = $"ISBN: {kitap.ISBN}";
        kitapBilgisi.transform.Find("KopyaSayisiText").GetComponent<TextMeshProUGUI>().text = $"Kopya Say�s�: {kitap.KopyaSayisi}";
        kitapBilgisi.transform.Find("OduncAlinanKopyaText").GetComponent<TextMeshProUGUI>().text = $"�d�n� Al�nan Kopya Say�s�: {kitap.OduncAlinanKopyaSayisi}";
    }

    public void KitapEkle()
    {
        string baslik = baslikInput.text;
        string yazar = yazarInput.text;

        // Gerekli alanlar� kontrol et
        if (string.IsNullOrEmpty(baslik) || string.IsNullOrEmpty(yazar))
        {
            sonucText.text = "Ba�l�k ve yazar alanlar� bo� b�rak�lamaz.";
            return;
        }

        // Otomatik olarak ISBN ve kopya say�s� ata
        string isbn = Random.Range(0, 100).ToString("D2");
        int kopyaSayisi = 1;

        // Kitap var m� kontrol et
        if (KitapVarMi(baslik, yazar))
        {
            sonucText.text = $"{baslik} ba�l�kl� kitap zaten mevcut.";
        }
        else
        {
            Kitap yeniKitap = new Kitap(baslik, yazar, isbn, kopyaSayisi);
            kitaplar.Add(yeniKitap);

            sonucText.text = $"{baslik} ba�l�kl� kitap ba�ar�yla eklendi.";

            // Kitap bilgilerini g�ncelle
            EkleKitapListesi(yeniKitap);
        }
    }

    private bool KitapVarMi(string baslik, string yazar)
    {
        foreach (var kitap in kitaplar)
        {
            if (kitap.Baslik.Equals(baslik, StringComparison.OrdinalIgnoreCase) && kitap.Yazar.Equals(yazar, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}

/*
public void KitapOduncAl()
    {
        string isbn = Random.Range(0, 100).ToString("D2");
        Kitap kitap = kitaplar.Find(k => k.ISBN == isbn);

        if (kitap != null && kitap.KopyaSayisi > kitap.OduncAlinanKopyaSayisi)
        {
            kitap.OduncAlinanKopyaSayisi++;
            sonucText.text = "Kitap �d�n� al�nd�.";
        }
        else
        {
            sonucText.text = "Kitap �d�n� al�namad�. T�m kopyalar �d�n�te veya kitap bulunamad�.";
        }
    }

    public void KitapIadeEt()
    {
        string isbn = Random.Range(0, 100).ToString("D2");
        Kitap kitap = kitaplar.Find(k => k.ISBN == isbn);

        if (kitap != null && kitap.OduncAlinanKopyaSayisi > 0)
        {
            kitap.OduncAlinanKopyaSayisi--;
            sonucText.text = "Kitap iade edildi.";
        }
        else
        {
            sonucText.text = "Kitap iade edilemedi. �d�n�te olan kopya bulunamad�.";
        }
    }

    public void SureAsimiKitaplar()
    {
        sonucText.text = "S�resi ge�mi� kitaplar:\n";
        foreach (var kitap in kitaplar)
        {
            if (kitap.OduncAlinanKopyaSayisi > 0)
            {
                sonucText.text += $"Ba�l�k: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}\n";
            }
        }
    }
}
*/