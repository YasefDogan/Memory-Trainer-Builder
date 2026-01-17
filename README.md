# C#-based memory reading and writing tool
 This project focuses on developing a C#-based application inspired by Cheat Engine to understand and demonstrate process memory manipulation techniques. Throughout the project, the working principles of memory management, including reading and writing data from a running process, were explored in detail. The application was designed to interact with system memory safely and efficiently, providing insight into how values are stored, accessed, and modified in real time. Additionally, the project helped improve understanding of low-level system operations, API usage, and the interaction between software and operating system memory. This implementation served as an educational tool to strengthen knowledge of memory structures, data flow, and software architecture.

-------------------------------------------------------------------------------------------------------------------------------------------------------------


# 🎮 Memory Scanner - Game Memory Editor

Windows oyunları için geliştirilmiş profesyonel bellek düzenleme aracı. Tek oyunculu oyunlarda bellek değerlerini (can, para, puan vb.) bulup değiştirmenizi sağlar.

[![Platform](https://img.shields.io/badge/platform-Windows-blue.svg)]()
[![.NET](https://img.shields.io/badge/.NET-Framework%204.7.2-purple.svg)]()

![Screenshot](docs/screenshot.png)

---

## 🚀 Hızlı Başlangıç

### 📦 İndirme

**[⬇️ En Son Sürümü İndir (Releases)](https://github.com/YasefDogan/Memory-Trainer-Builder/releases/tag/memTrainer)**

> Zip dosyasını indirip çıkarın, `MemoryScanner.exe` dosyasını çalıştırın. Kurulum gerektirmez!

---

## 🔧 Kurulum

### Seçenek 1: Hazır EXE (Önerilen)

1. [Releases sayfasından]((https://github.com/YasefDogan/Memory-Trainer-Builder/releases/tag/memTrainer)) son sürümü indirin
2. Zip'i çıkarın
3. `MemoryScanner.exe` dosyasını çalıştırın
4. **Yönetici olarak çalıştırın** (bazı oyunlar için gerekli)

### Seçenek 2: Kaynak Koddan
```bash
# 1. Projeyi klonlayın
git clone https://github.com/yourusername/memory-scanner.git
cd memory-scanner

# 2. Visual Studio ile açın
# cheat-engine.sln dosyasını açın

# 3. Build edin
# Build → Build Solution (Ctrl+Shift+B)

# 4. Çalıştırın
# bin/Debug/cheat-engine.exe
```

**Gereksinimler:**
- Windows 7/8/10/11
- .NET Framework 4.7.2 veya üzeri
- Visual Studio 2019+ (kaynak koddan build için)

---

## 🗄️ Backend (Opsiyonel)

Memory Scanner, bulunan adresleri kaydetmek için **opsiyonel** bir backend kullanır.

### Backend Kurulumu

Backend projesi ayrı repository'de:

**🔗 [Backend Repository'ye Git](https://github.com/YasefDogan/Memory-Trainer-Backend)**



> ⚠️ **Not:** Backend kullanmadan da program çalışır! Backend sadece adres geçmişini saklamak içindir.



## 📖 Nasıl Kullanılır?

### 1️⃣ Process Seçimi

![Process Selection](<img width="701" height="575" alt="image" src="https://github.com/user-attachments/assets/29cd6113-7500-432b-881b-90f2fd074f06" />
)

1. **Oyunu başlatın** (örn: Witcher 3, Euro Truck Sim 2)
2. Memory Scanner'ı açın
3. Üst dropdown'dan oyun process'ini seçin
4. **Yenile** butonuna basarak listeyi güncelleyebilirsiniz

---

### 2️⃣ İlk Tarama (First Scan)

![First Scan](docs/step2.png)

**Senaryo:** witcherda'da 1234567 param var, bunu bulmak istiyoruz.
<img width="696" height="335" alt="image" src="https://github.com/user-attachments/assets/d2c7ca07-c557-42ac-8182-05101601b760" />


1. **Veri Tipi** seçin: `Int` (tam sayılar için)
2. **Değer** girin: `1234567`
3. **Tara** butonuna basın
4. Bekleyin... (oyunun kullandığı bellek miktarina göre artıp azalacak)
5. Sonuç listesinde muhtemelen binlerce adres göreceksiniz
```
📊 Sonuç: 3,247 adres bulundu
```
<img width="697" height="578" alt="image" src="https://github.com/user-attachments/assets/961e4a59-8ed1-4236-957a-520b78d77e53" />

> 🤔 **Çok fazla sonuç mu?** Normal! Bir sonraki adıma geçin.

---

### 3️⃣ Tekrar Tarama (Rescan)

![Rescan](docs/step3.png)

**Amaç:** Yanlış adresleri elemek.

1. **Oyunda değeri değiştirin** (örn:para harcayın)
2. <img width="727" height="414" alt="image" src="https://github.com/user-attachments/assets/5c895a77-556c-48e0-baf2-c1c24f878653" />

3. Memory Scanner'da **yeni değeri girin**: `1234437`
4. **Tekrar Tara** butonuna basın
5. Sadece değişen adresler kalır
```
📊 İlk tarama: 3,247 adres
📊 2. tarama:  127 adres
📊 3. tarama:  8 adres
📊 4. tarama:  1 adres ✅
```

> 💡 **İpucu:** 1-10 adres kalana kadar tekrarlayın!

---

### 4️⃣ Değer Değiştirme (Write)

![Write Value](<img width="723" height="590" alt="image" src="https://github.com/user-attachments/assets/805a6528-23c4-4e99-944f-2cc1c04c44c6" />
)

1. Doğru adresi bulduktan sonra
2. **Yazılacak Değer** alanına yeni değeri girin: `161616`
3. **Yaz** butonuna basın
4. Oyuna dönün → para 161616 olmalı! 🎉
   <img width="692" height="485" alt="image" src="https://github.com/user-attachments/assets/490f18c3-292d-4662-a981-2d5741a9f47c" />

---

### 4️.1 Adresleri Kaydetme

![Export](<img width="693" height="576" alt="image" src="https://github.com/user-attachments/assets/aa7b5399-797c-46c9-a628-65c6354eefae" />
)


1. **Dışa Aktar** butonuna basın
2. Her adrese **özel isim** verebilirsiniz (opsiyonel):
   - `0x1A2B3C4D` → "PlayerHealth"
   - `0x5E6F7A8B` → "Money"
3. Dosyayı kaydedin (örn: `minesweeper.txt`)

**Dosyadan Yükleme İçin:**

1. **İçe Aktar** sekmesine gidin
2. **Gözat** → Kaydettiğiniz dosyayı seçin
3. **Yükle** butonuna basın
4. Her adres için **Oku** / **Yaz** butonlarını kullanın

![Import](<img width="671" height="554" alt="image" src="https://github.com/user-attachments/assets/f8d76b4d-75d8-4ed9-b09b-61d8a0724001" />
)

---

## 🎯 Desteklenen Veri Tipleri

| Tip | Açıklama | Örnek |
|-----|----------|-------|
| **Int** | Tam sayılar | Can: 100, Para: 5000 |
| **Float** | Ondalıklı sayılar | Hız: 5.5, Sıcaklık: 36.6 |
| **Double** | Büyük ondalıklı | Koordinat: 123.456789 |
| **ByteArray** | Hex değerler | `48 65 6C 6C 6F` |

---


### 📊 İlerleme Takibi

Tarama sırasında **progress bar** ile ilerlemeyi görürsünüz.

### 📝 Detaylı Loglama

Her işlem alt panelde loglanır:
```
[14:30:15] INFO: Tarama başlatıldı - Process: game.exe
[14:30:18] INFO: 247 adres bulundu
[14:30:25] INFO: Belleğe yazıldı - 0x1A2B3C4D
```

---

## ❓ Sık Sorulan Sorular

### 🔴 "Access Denied" hatası alıyorum

**Çözüm:**
1. Memory Scanner'ı **Yönetici olarak çalıştırın**
2. Sağ tık → "Yönetici olarak çalıştır"


### 🔴 Tarama hiç sonuç vermiyor

**Çözüm:**
1. Doğru **process** seçtiğinizden emin olun
1. Doğru **veri tipini** seçtiğinizden emin olun
2. Değerin **tam olarak** doğru olduğunu kontrol edin
3. Oyun değeri şifreliyor olabilir (karmaşık oyunlar için)

### 🔴 Yazma işlemi çalışmıyor

**Çözüm:**
1. Anti-cheat korumalı oyunlarda çalışmaz

### 🔴 Backend'e bağlanamıyorum

**Çözüm:**
1. Backend sunucusunun çalıştığından emin olun: `http://localhost:3000`
2. Node.js yüklü ve çalışıyor mu kontrol edin
3. Backend olmadan da program çalışır (opsiyonel özellik)

---

## 🛡️ Güvenlik ve Etik Kullanım

### ✅ İzin Verilen Kullanım

- ✅ **Tek oyunculu oyunlar**
- ✅ Kendi offline oyunlarınız
- ✅ Test ve öğrenme amaçlı
- ✅ Oyun modlama

### ❌ İzin Verilmeyen Kullanım

- ❌ **Çevrimiçi/multiplayer oyunlar**
- ❌ Rekabetçi oyunlarda hile
- ❌ Başkalarının deneyimini bozma
- ❌ Anti-cheat sistemlerini bypass etme

> ⚠️ **UYARI:** Bu araç yalnızca eğitim amaçlıdır. Online oyunlarda kullanımı yasaktır ve hesap banına yol açabilir.

---

## 🐛 Sorun Bildirme

Hata bulduysanız veya öneriniz varsa:

1. [Issues sayfasını](https://github.com/YasefDogan/Memory-Trainer-Builder/issues) açın
2. **New Issue** butonuna tıklayın
3. Şunları ekleyin:
   - Hatanın açıklaması
   - Adım adım nasıl tekrarlanacağı
   - Ekran görüntüsü (varsa)
   - Log çıktısı

---

## 🤝 Katkıda Bulunma

Pull request'ler kabul edilir!
```bash
# 1. Fork edin
# 2. Feature branch oluşturun
git checkout -b feature/amazing-feature

# 3. Commit edin
git commit -m 'Add some amazing feature'

# 4. Push edin
git push origin feature/amazing-feature

# 5. Pull Request açın
```


---

## 👨‍💻 Geliştirici

**YasefDogan**

- GitHub: [@yasefdogan(https://github.com/YasefDogan)]

---

## 🙏 Teşekkürler

- [Cheat Engine](https://www.cheatengine.org/) - İlham kaynağı
- .NET Framework ekibi

---

## 📚 Teknik Detaylar

İleri düzey kullanıcılar için:

- **Mimari:** MVVM-like pattern with dependency injection
- **Memory API:** Windows `ReadProcessMemory` / `WriteProcessMemory`
- **Threading:** Asynchronous Task-based operations
- **UI:** WinForms with custom dark theme
- **Backend:** Node.js + SQLite (opsiyonel)

---

<div align="center">

**⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!**

Made with C# by YasefDogan

</div>


