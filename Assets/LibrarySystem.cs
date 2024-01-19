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

    public Transform kitapPaneli; // Kitap bilgilerinin görüntülendiði panel
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

        // Kitap bilgilerini prefab içindeki TextMeshProUGUI elemanlarýna ata
        kitapBilgisi.transform.Find("BaslikText").GetComponent<TextMeshProUGUI>().text = $"Baþlýk: {kitap.Baslik}";
        kitapBilgisi.transform.Find("YazarText").GetComponent<TextMeshProUGUI>().text = $"Yazar: {kitap.Yazar}";
        kitapBilgisi.transform.Find("ISBNText").GetComponent<TextMeshProUGUI>().text = $"ISBN: {kitap.ISBN}";
        kitapBilgisi.transform.Find("KopyaSayisiText").GetComponent<TextMeshProUGUI>().text = $"Kopya Sayýsý: {kitap.KopyaSayisi}";
        kitapBilgisi.transform.Find("OduncAlinanKopyaText").GetComponent<TextMeshProUGUI>().text = $"Ödünç Alýnan Kopya Sayýsý: {kitap.OduncAlinanKopyaSayisi}";
    }

    public void KitapEkle()
    {
        string baslik = baslikInput.text;
        string yazar = yazarInput.text;

        // Gerekli alanlarý kontrol et
        if (string.IsNullOrEmpty(baslik) || string.IsNullOrEmpty(yazar))
        {
            sonucText.text = "Baþlýk ve yazar alanlarý boþ býrakýlamaz.";
            return;
        }

        // Otomatik olarak ISBN ve kopya sayýsý ata
        string isbn = Random.Range(0, 100).ToString("D2");
        int kopyaSayisi = 1;

        // Kitap var mý kontrol et
        if (KitapVarMi(baslik, yazar))
        {
            sonucText.text = $"{baslik} baþlýklý kitap zaten mevcut.";
        }
        else
        {
            Kitap yeniKitap = new Kitap(baslik, yazar, isbn, kopyaSayisi);
            kitaplar.Add(yeniKitap);

            sonucText.text = $"{baslik} baþlýklý kitap baþarýyla eklendi.";

            // Kitap bilgilerini güncelle
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
            sonucText.text = "Kitap ödünç alýndý.";
        }
        else
        {
            sonucText.text = "Kitap ödünç alýnamadý. Tüm kopyalar ödünçte veya kitap bulunamadý.";
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
            sonucText.text = "Kitap iade edilemedi. Ödünçte olan kopya bulunamadý.";
        }
    }

    public void SureAsimiKitaplar()
    {
        sonucText.text = "Süresi geçmiþ kitaplar:\n";
        foreach (var kitap in kitaplar)
        {
            if (kitap.OduncAlinanKopyaSayisi > 0)
            {
                sonucText.text += $"Baþlýk: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}\n";
            }
        }
    }
}
*/