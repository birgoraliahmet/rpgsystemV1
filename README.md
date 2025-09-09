## 🇬🇧 English 

### 🎮 Dialogue-Based Save System – Modular RPG Framework for Unity

This project provides a modular and expandable **save system** for dialogue-driven games built in Unity. It tracks player choices, position, and scene interactions. Supports both manual and automatic saving.

### 🚀 Features

- One-time NPC interaction → prevents repeated dialogue  
- Choice-based response display and button hiding  
- Save / Load / New Game buttons  
- Auto-save zones (trigger-based)  
- Saves player position and all choices to a log file  
- Culture-independent float parsing to prevent position errors  
- Restores player to initial spawn point on New Game

### 📁 Save File Format

```
POS:12.5,3.2,0  
CHOICE:0:1  
CHOICE:1:0  
```

### 🧩 Usage

- `GameSaveManager.cs` → handles all save operations  
- `DialogueManager.cs` → tracks choices and controls dialogue  
- Connect Save / Load / New Game buttons via UI  
- Assign player object to `playerTransform`  
- Add `GameSaveManager` to trigger zones for auto-save functionality

---

### 🤝 Contribution Invitation

This system is **actively under development**, and contributions are welcome.  
Feel free to submit pull requests or share your ideas and suggestions.

📧 Email: **solmazahmet1031@hotmail.com**  
💬 Discord: **amirtocha**

---
## 🇹🇷 Türkçe

### 🎮 Diyalog Tabanlı Kayıt Sistemi – Unity için RPG Altyapısı

Bu proje, Unity'de geliştirilen diyalog tabanlı oyunlar için modüler ve genişletilebilir bir **kayıt sistemi** sunar. Oyuncu seçimlerini, pozisyonunu ve sahne içi etkileşimleri kaydeder; geri yükler; sıfırlar. Hem manuel hem otomatik kayıt desteklenir.

### 🚀 Özellikler

- NPC ile bir kez konuşma → tekrar konuşulamaz  
- Seçim sonrası cevap gösterimi ve butonların gizlenmesi  
- Save / Load / New Game butonları  
- Otomatik kayıt alanları (trigger tabanlı)  
- Kayıt dosyasına pozisyon ve tüm seçimler yazılır  
- Kültür bağımsız float parse ile pozisyon hatası engellenir  
- Başlangıç pozisyonuna dönüş desteği

### 📁 Kayıt Formatı

```
POS:12.5,3.2,0  
CHOICE:0:1  
CHOICE:1:0  
```

### 🧩 Kullanım

- `GameSaveManager.cs` → tüm kayıt işlemleri  
- `DialogueManager.cs` → seçim takibi ve diyalog kontrolü  
- UI üzerinden Save / Load / New Game butonları bağlanmalı  
- Oyuncu objesi `playerTransform` olarak tanımlanmalı  
- Trigger alanlarına `GameSaveManager` eklenerek otomatik kayıt sağlanabilir

---

### 🤝 Katkı Çağrısı

Bu sistemin geliştirme süreci **aktif olarak devam etmektedir**.  
Kod katkısı sağlamak isteyen herkes projeye pull request gönderebilir.  
Görüş, öneri veya teknik tavsiyeleriniz için bana ulaşabilirsiniz:

📧 E-posta: **solmazahmet1031@hotmail.com**  
💬 Discord: **amirtocha**
