# 🎫 AI-Powered Ticket Reservation & Sentiment Analysis System

Bu proje, modern web teknolojileri ile yapay zekayı birleştiren, mikroservis mimarisine sahip bir bilet rezervasyon platformudur. Kullanıcı yorumlarını gerçek zamanlı olarak analiz ederek duygu durum tespiti yapar.

## 🚀 Teknolojiler
* **Web Arayüzü:** ASP.NET MVC (.NET Framework)
* **Yapay Zeka Servisi:** Python FastAPI
* **NLP Modeli:** BERT (savasy/bert-base-turkish-sentiment-cased)
* **Veritabanı:** MS SQL Server
* **Deployment:** Render (AI API) & Somee (Web & DB)

## 🏗️ Mimari Yapı
Proje iki ana bileşenden oluşmaktadır:
1. **ReservationSystem:** Kullanıcı arayüzü ve rezervasyon mantığının yönetildiği C# katmanı.
2. **ReservationSentimentAPI:** Yorumları işleyen ve duygu analizi skorlarını dönen Python tabanlı yapay zeka servisi.

## 🧠 Yapay Zeka Özellikleri
Sistem, kullanıcıların etkinlikler hakkındaki geri bildirimlerini analiz etmek için önceden eğitilmiş bir **BERT** modelini kullanır. 
- **Olumlu:** Etkinlik puanını artırır.
- **Olumsuz:** Yönetici paneline uyarı düşürür.

## 🛠️ Kurulum ve Çalıştırma
1. C# projesindeki `Web.config` dosyasındaki ConnectionString'i güncelleyin.
2. Python klasöründe `pip install -r requirements.txt` komutu ile bağımlılıkları yükleyin.
3. FastAPI servisini `uvicorn main:app` ile ayağa kaldırın.
