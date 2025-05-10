# 📧 Identity Email Projesi – .NET Core 8.0

Bu proje, M\&Y Akademi danışmanlık bünyesinde ve Murat Yücedağ hocamın eğitmenliğinde geliştirilmiştir. ASP.NET Core 8.0 kullanılarak hazırlanmış, kullanıcıların e-posta benzeri mesajlaşma işlemlerini gerçekleştirebildiği bir **Email & Chat uygulamasıdır**.

## 🔧 Kullanılan Teknolojiler

* **ASP.NET Core 8.0**
* **Entity Framework Core** (Code First)
* **MS SQL Server**
* **ASP.NET Core Identity**
* **Bootstrap 4**
* **LINQ**
* **Razor Views**

## 🏗️ Genel Özellikler

* **Tek katmanlı mimari** kullanıldı.
* UI bileşenleri **PartialView** yapısıyla yönetilebilir hale getirildi.
* Veritabanı bağlantısı EF Core ile kuruldu.
* Kullanıcı yönetimi ve oturum işlemleri Identity ile güvenli şekilde sağlandı.
* Arama işlemleri için `Contains()` yöntemi kullanıldı.

## 🖥️ Sayfa Yapısı

### 1. Kayıt Sayfası (Register)

* Kullanıcı kayıt işlemi yapılır.
* Şifreler hash’lenerek veritabanında saklanır.
* Doğru şifre yapısı için validator kuralları tanımlanmıştır.
![Register](https://github.com/user-attachments/assets/4f79c1ff-c451-4558-88aa-7c48c3d40bbf)
![Validation](https://github.com/user-attachments/assets/0c2bc803-751d-4c7c-a673-086dc97f2eb1)


### 2. Giriş Sayfası (Login)

* Kullanıcı kimlik doğrulama işlemi gerçekleştirilir.
* 5 hatalı giriş denemesinden sonra 5 dakika süreyle erişim engellenir.
![Login](https://github.com/user-attachments/assets/3b83ad1e-f1ba-4def-b5a5-7fe6249b3cb3)


### 3. Kullanıcı Paneli

* **Profilim**
* **Gelen Kutusu**
* **Giden Kutusu**
* **Çöp Kutusu**
* **Yeni Mesaj Gönder**
* **Arama Çubuğu**
* **Çıkış Yap**

### Mesaj Yönetimi Özellikleri

* Gelen ve giden mesajlar görüntülenebilir.
* Arama çubuğu ile filtreleme yapılabilir.
* Arama sonucunda eşleşme yoksa boş içerik döner.
* Çoklu mesaj seçimi yapılabilir.
* Seçilen mesajlar "Okundu olarak işaretle" özelliğiyle güncellenebilir.
* Mesajlar çöp kutusuna taşınabilir.
* Sidebar’daki mesaj sayısı anlık güncellenir.
![Inbox](https://github.com/user-attachments/assets/26e6e855-1a2f-490a-bf9d-adfab2a2333b)
![search](https://github.com/user-attachments/assets/81ea25f2-c814-44c4-9244-ede12c295dab)
![SendBox](https://github.com/user-attachments/assets/d0bfd278-75af-48d9-8c82-8d6c34beb5d1)
![MessageDetail](https://github.com/user-attachments/assets/22b20b15-8810-493b-a3ca-5472896a8397)
![TrashBox](https://github.com/user-attachments/assets/24e3bd8b-eb08-4748-ba20-03288c05888f)

### Yeni Mesaj Oluşturma

* Alıcı bilgileri, konu ve içerik girilerek mesaj gönderilir.
* Başarılı gönderim sonrası kullanıcıya **SweetAlert** ile bildirim gösterilir.
![CreateMessage](https://github.com/user-attachments/assets/4ae66bfb-c8cb-45aa-8237-e4a4b7a1285d)
![sweetalert](https://github.com/user-attachments/assets/b527f345-8582-4fb5-9053-ecee96e9afe2)

### Profil Sayfası

* Kullanıcı bilgileri (ad, soyad, e-posta, kullanıcı adı, profil fotoğrafı) görüntülenir.
* Gönderilen ve alınan mesaj sayıları dinamik olarak hesaplanır.
* Tüm bilgiler güncellenebilir.
![UserInfo](https://github.com/user-attachments/assets/8c680712-2b81-4ff4-8412-f530a411eb8a)
![UserUpdate](https://github.com/user-attachments/assets/415e7351-b8e4-4386-8d29-54b59fe82734)
