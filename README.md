# ViShape Modu - Kurulum ve Mekanik Belgesi

*Among Us için Viper + Shapeshifter hibrit Impostor rolü. Sürüm 1.0, BepInEx IL2CPP ve Reactor 2.x hedefli oyun tasarımı ve teknik entegrasyon planı.*

**Slogan:** Yüzünü çal, izini sil, geriye bir damla asit bırak.

**Genel bilgiler**

| Başlık | Değer |
| --- | --- |
| Takım | Impostor (alt rol) |
| Rol rengi | Asit yeşili (RGB 140, 242, 51), kenarlarda mor yansıma |
| Gereksinim | Mod lobideki tüm oyuncularda yüklü olmalıdır |
| Otorite modeli | Host otoriteli: rol ataması, ayar eşitleme ve gerçek öldürme Host tarafından yapılır |
| Varsayılan tuşlar | Kill butonu: asit erimesi, F: şekil değiştirme, G: zehir gazı |

**Kurulum özeti**

1. Oyunun sürümüne uygun BepInEx IL2CPP paketini oyun klasörüne açın ve oyunu bir kez çalıştırıp kapatın.
2. Reactor.dll dosyasını BepInEx/plugins klasörüne koyun.
3. Derlenen ViShape.dll dosyasını aynı klasöre koyun.
4. Lobideki tüm oyuncuların aynı mod paketine ve aynı oyun sürümüne sahip olduğundan emin olun.
5. Host lobide ayar panelini açar, değerleri düzenler ve ayarları herkese senkronize eder.
6. Oyun başlayıp tanıtım ekranı bittiğinde Host, Impostor'lar arasından ViShape'leri rastgele seçer ve sonucu tüm oyunculara iletir.

**Uyumluluk notu:** Oyun güncellemelerinde bazı iç sınıf ve metot adları değişebilir. Her büyük oyun güncellemesinden sonra mod, kısa bir deneme lobisinde doğrulanmalıdır.

## Rolün Çalışma Mantığı

### Rolün kimliği

ViShape, Impostor takımının "temizlikçi ve illüzyonist" üyesidir. Sıradan bir Impostor ceset bırakır ve bu ceset kanıt olur. ViShape cesedi yok eder, ancak bunun bedeli olarak her yeteneğinde kendine özgü bir iz bırakır. Rol, düşük riskli görünen ama dikkatsizlikte çok pahalıya patlayan bir risk-ödül dengesi üzerine kuruludur.

### Hibrit yapı

| Kaynak rol | ViShape'e aktarılan mekanik | ViShape'e özgü bedel |
| --- | --- | --- |
| Shapeshifter | Seçilen oyuncunun görünümüne bürünme | Dönüşümde ve geri dönüşte yerde asit lekesi |
| Viper | Bölgesel zehir bulutu ve ölümcül temas | Bulut herkese görünür, ViShape'in kendi görüşü de düşer |
| Yeni | Cesetsiz asitle erime | Sınırlı yük, donma anı, havuz izi |

### Tur akışı

**Erken oyun (ilk 2-3 dakika).** ViShape öldürmez. Kalabalık koridorları, vent bağlantılarını ve kamera noktalarını öğrenir. İsterse boş bir odada gazı deneyerek Crewmate tepkisini ölçer. Rol başında tüm yetenekler yaklaşık 10 saniyelik bir başlangıç beklemesiyle açılır.

**Orta oyun.** Tipik senaryo: ViShape yalnız bir Crewmate'i takip eder, görüş dışı bir koridorda asit yeteneğini kullanır, ceset oluşmaz, rapor oluşmaz. Havuzu terk eder ve kimseyi çağırmadan uzaklaşır. Kurbanın yokluğu bir süre fark edilmez.

**Geç oyun.** Toplantılarda "kim kayıp?" tartışması başlar. ViShape, masum bir oyuncunun kılığına girip o oyuncunun yanında belirerek şüpheyi dağıtabilir. Ancak toplantı başladığında tüm kılıklar otomatik bozulur.

### Kazanma ve kaybetme

- **Kazanma:** Impostor sayısı hayatta kalan Crewmate sayısına eşitlenir veya geçer ya da kritik sabotaj süresi dolar.
- **Kaybetme:** Tüm Impostor'lar oylamayla atılır veya tüm görevler tamamlanır.
- **Kişisel ünvan (isteğe bağlı):** Oyun sonunda hiçbir cesedi raporlanmamış ViShape, "En Temiz İş" ünvanını ve zafer ekranında yeşil parıltıyı alır. Sonucu etkilemez, sadece prestij içindir.

### Takım içindeki görevi

ViShape takım arkadaşının cinayetinin ardından gaz salarak tanıkların görüşünü keser. Böylece kendi öldürmesinin yanında bir **koruma ve kaçış** uzmanı olarak da çalışır.

### Karşı oyun

Rol ancak Crewmate'lerin de yanıt verebildiği ölçüde adildir:

- Asit havuzuna basan oyuncu yavaşlar, bu da havuzun varlığını ele verir.
- Toplantıda ölü listesinde "ERİDİ" damgası görünürse lobide bir ViShape olduğu kesinleşir. Kimin olduğu açıklanmaz.
- Aynı görünümde iki oyuncunun aynı anda görülmesi güçlü bir alarm işaretidir.
- Güvenlik kameralarında gaz bulutu görünür.
- Admin tablosu kılıktaki ViShape'i hedef oyuncunun sayısına ekler, bu da sayım tutarsızlığı yaratır.

### Ağ ve otorite modeli

| Olay | Kim başlatır | Kim onaylar | Kim görür |
| --- | --- | --- | --- |
| Ayar eşitleme | Host | Host | Herkes |
| Rol ataması | Host | Host | Herkes (rol kimliği sadece sahibine gösterilir) |
| Şekil değiştirme | ViShape | Her istemci kendi tarafında uygular | Herkes |
| Gaz salma | ViShape | Her istemci kendi tarafında uygular | Herkes |
| Asit erimesi | ViShape | Host mesafe ve canlılık doğrulaması yapar, gerçek öldürmeyi Host gerçekleştirir | Herkes |

Gerçek ölümü Host yaptığı için oyuncunun ölü durumu, hayalet moduna geçişi ve oyun sonu kontrolü vanilla oyunla aynı çalışır. Yalnızca oluşan ceset her istemcide silinir ve rapor engellenir.

## Aktif Yetenekler ve Sınırlar

### Genel kural

Her yetenek yalnızca ViShape hayattayken, toplantı veya sürgün sahnesi sürmüyorken ve görev ekranı açık değilken kullanılabilir. Her yeteneğin kendi bekleme süresi vardır ve bu süreler birbirinden bağımsız işler.

### Yetenek 1: Şekil Değiştirme

| Parametre | Varsayılan | Açıklama |
| --- | --- | --- |
| Bekleme süresi | 20 sn | Eski hale dönüşten sonra başlar |
| Kılık süresi | 20 sn | Süre bitince otomatik geri dönüş |
| Hedef | Hayattaki herhangi bir oyuncu | Kendisine dönüşemez |
| Dönüşümde iz | Asit lekesi | Hem dönüşte hem geri dönüşte oluşur |
| Toplantı etkisi | Kılık bozulur | Tartışmaya kılıkla girilemez |

**Sınırlamalar**

- Kılıktayken tekrar dönüşüm yapılamaz.
- Vent içindeyken iz bırakılmaz ama dönüşüm de yapılamaz.
- Asit erimesinden sonra 10 saniye boyunca dönüşüm kilitlenir. Böylece "öldür, hemen başkasına dön, kaç" kombosu engellenir.
- Kılıktayken ayak altında çok soluk bir yeşil ışıma vardır. Yakından bakan fark eder.
- Hedef oyuncu dönüşüm sırasında ölürse kılık o anki görünümle devam eder, ancak süre dolunca normal hale döner.

### Yetenek 2: Zehirli Gaz (Viper mekaniği)

| Parametre | Varsayılan | Açıklama |
| --- | --- | --- |
| Bekleme süresi | 30 sn | Kullanımdan sonra başlar |
| Bulut süresi | 10 sn | Son 1,5 saniyede solar |
| Yarıçap | 3 birim | Orta boy oda yaklaşık 5-6 birimdir |
| Crewmate görüş çarpanı | 0 (tam kör) | 0 ile 1 arasında ayarlanır |
| Otomatik vent gazı | Açık | Vente girişte bekleme hazırsa gaz çıkar |
| ViShape'in kendi görüşü | Yarıya iner | İstenirse kapatılabilir |

**Sınırlamalar**

- Bulut dışarıdan görünür. Bu hem keşif hem uyarı işlevi görür.
- Impostor'lar varsayılan olarak bağışıktır. Ayardan etkilenir hale getirilebilir.
- Aynı anda en fazla iki bulut sahada bulunabilir. Üçüncüsü en eski bulutu siler.
- Toplantı başladığında tüm bulutlar kaldırılır.
- Bulut duvarların arkasına sızmaz, yalnızca bulunduğu görüş alanında etkilidir.

### Yetenek 3: Asit Erimesi

| Parametre | Varsayılan | Açıklama |
| --- | --- | --- |
| Bekleme süresi | 30 sn | Vanilla öldürme beklemesinin yerini alır |
| Menzil | 1,4 birim | Duvar veya engel arada olmamalı |
| Başlangıç yükü | 2 | Yük bitince yetenek kullanılamaz |
| Toplantıda geri gelen yük | 1 | Toplamda başlangıç yükünü geçmez |
| Donma süresi | 1,5 sn | ViShape bu sürede hareket edemez |
| Havuz süresi | 25 sn | Havuz yavaş yavaş küçülüp kurur |
| Havuzda hız | %60 | Havuzdaki Crewmate yavaşlar |

**Sınırlamalar**

- Yalnızca Crewmate hedeflere işler, diğer Impostor'lara işlemez.
- Vent içindeki oyuncuya ve vent içindeyken kullanıcıya işlemez.
- Hedefin ceset bırakmaması için Host öldürme olayını tamamladıktan sonra oluşan ceset her istemcide silinir.
- Erimiş oyuncunun cesedi raporlanamaz. Toplantıda yalnızca "ERİDİ" damgası ve havuz izleri ipucu olarak kalır.
- Host menzil, canlılık ve takım doğrulaması yapar. Geçersiz istekler sessizce reddedilir.

### Yetenekler arası kilitler

| Kural | Etki |
| --- | --- |
| Asitten sonra kılık kilidi | 10 sn |
| Gaz içinde asit kullanımı | Havuz daha parlak görünür, dışarıdan ışık patlaması fark edilir |
| Aktif kılık sırasında asit | Kullanılabilir, fakat dönüşüm sona erdiğinde havuz izi ek olarak bırakılır |
| Toplantı | Tüm kılıklar bozulur, bulutlar ve havuzlar silinir |

### Zayıf nokta özeti

| Güç | Zayıf nokta |
| --- | --- |
| Cesetsiz öldürme | Havuz izi, sınırlı yük, donma anı |
| Kılık değiştirme | Leke izi, ayak ışıması, kısa süre |
| Kör edici gaz | Bulut görünür, ViShape de görüşünü kaybeder |
| Kombolar | Yetenekler arası kilitler |

## Görsel Efekt Algoritmaları

Tüm efektler yeşil-mor palete dayanır: canlı asit yeşili, koyu mor gölgeler ve hafif parlama. Dış görsel dosya gerektirmeyen, çalışma anında üretilen dokularla çalışacak şekilde tasarlanmıştır.

### Doku üretimi

1. Yuvarlak bir doku oluşturulur (128 x 128 piksel).
2. Her pikselin merkeze uzaklığı hesaplanır ve yarıçapa oranlanır.
3. Uzaklık 1'e ulaştığında saydamlık sıfırlanır, merkezde tam opak kalır.
4. Kenar yumuşaklığı iki dokuda farklıdır: gaz dokusunda yumuşaklık yüksek (0,7), asit dokusunda düşük (0,25). Gaz bulutu sisli, asit havuzu keskin kenarlı görünür.
5. Doku bir kez üretilir ve tüm bölgeler tarafından paylaşılır, böylece bellek maliyeti sabit kalır.

### Bölge modeli (gaz ve havuz)

Her bulut veya havuz, aşağıdaki bilgileri taşıyan bir "bölge" kaydıdır: konum, yarıçap, başlangıç zamanı, bitiş zamanı, gaz mı havuz mu olduğu, temel ölçek ve temel renk. Bölgeler tek bir listede tutulur ve her karede güncellenir.

**Her karede yapılan işlemler**

1. Süresi dolan bölgeler görsel nesneleriyle birlikte silinir.
2. **Belirme:** Bölge yaşı 0,6 saniyeye ulaşana kadar saydamlık 0'dan tam değere doğrusal artar.
3. **Solma:** Bitişe 1,5 saniye kala saydamlık tam değerden 0'a iner.
4. **Nefes alma (yalnızca gaz):** Ölçek, yaşın sinüsüne bağlı olarak yaklaşık %5 oranında büyüyüp küçülür.
5. Sıralama düzeyi: gaz oyuncuların üstünde (yüksek sıra), asit havuzu zeminde (düşük sıra) çizilir.

### Bölge içi etkilerin hesaplanması

| Etki | Algoritma |
| --- | --- |
| Gazda görüş kısıtlama | Yerel oyuncu Crewmate ise ve herhangi bir gaz bölgesinin yarıçapı içindeyse, oyunun hesapladığı ışık yarıçapı görüş çarpanıyla çarpılır (0 değeri neredeyse tam körlük verir) |
| Havuzda yavaşlama | Yerel Crewmate havuz yarıçapı içindeyse yürüme hızı yavaşlama oranıyla çarpılır, havuzdan çıkınca varsayılan hıza geri döner |
| Toplantı başı temizliği | Tüm bölgeler silinir ve hız varsayılana döndürülür |

### Asitle eritme sahnesi (yaklaşık 2 saniye)

| Zaman | Olay | Görsel/ses |
| --- | --- | --- |
| 0,0 sn | ViShape kill tuşuna basar, ceset üretimi baskılanır | Elinden yeşil damlalar süzülür |
| 0,2 sn | Temas anı, kurban yeşil parlar | Kısa cızırtı sesi |
| 0,2-1,3 sn | Kurbanın sprite'ı yukarıdan aşağıya yeşile boyanır, ölçeği küçülür, yüzeyde kabarcıklar çıkar | Kabarcık ve cızırtı katmanı |
| 1,3 sn | Kurban kaybolur, yerinde asit havuzu belirir | Islak "pop" sesi |
| 1,5 sn | ViShape elini silker ve hareket yeniden açılır | Hafif nefes sesi |

**Kurbanın ekranı:** Son anda ekran yeşile döner, "ERİDİN" yazısı çıkar, ardından hayalet moduna geçilir.

**Ceset silme mantığı:** Erime işareti oyuncunun kimliğiyle bir "erimiş" listesine yazılır. Sonraki 8 saniye boyunca her 0,05 saniyede bir sahnedeki ceset nesneleri taranır, sahibi erimiş listesinde olan ceset anında yok edilir. Süre dolunca bekleyen kayıt temizlenir. Rapor isteği erimiş kimlikler için baştan reddedilir, bu nedenle ceset bir an görünse bile raporlanamaz.

### Gaz salma sahnesi

1. **Vent girişi:** Kapak açılırken kenarlardan yeşil buhar sızar. Kapak kapandıktan sonra bulut büyüyerek yarıçapına ulaşır (yaklaşık 0,6 saniye).
2. **Yaşam döngüsü:** Merkez koyu yeşil, kenarlar şeffaf ve dalgalı. Küçük parlak parçacıklar yukarı süzülür.
3. **İçerideki Crewmate:** Ekran kenarlarından yeşil sis kararır, görüş çember gibi daralır. Hafif öksürük ve boğuk kalp atışı sesi duyulur.
4. **Dışarıdaki oyuncu:** Kalın yeşil bir bulut görür. İçerisi seçilemez ama hareket eden gölgeler fark edilir.
5. **Kamera ve admin:** Güvenlik kamerasında bulut aynı ölçek ve renkte görünür (ayardan kapatılabilir).

### Kılık değiştirme sahnesi (1 saniye)

1. ViShape'in vücudu yarı saydam bir jöle gibi dalgalanır.
2. Renk ve kıyafet bilgisi hedefin görünüm bilgisiyle değiştirilir. Geçiş, renklerin akışkan biçimde kayması ile gösterilir.
3. Yere birkaç damla düşer ve küçük bir asit lekesi bırakır.
4. Kılıktayken ayak altında soluk yeşil ışıma oynar.
5. Geri dönüşte vücut titreşir, kendi görünümüne kavuşur ve yine bir leke bırakır.
6. ViShape'in kendi ekranında, kılık süresi hedefin renginde bir halka olarak butonun çevresinde azalır.

### Arayüz ve ses

- Rol ikonu: yeşil damla içinde mor göz.
- Butonlar: kılık için mor maske, gaz için yeşil bulut, asit için damlayan yumruk.
- Tanıtım ekranı: yeşil sisle açılış, "VİSHAPE" yazısı damlalar halinde belirir, alt başlıkta slogan görünür.
- Ses paleti: derin ıslak "glug" ana tema, gaz için tıslama, erime için cızırtı ve kabarcık, kılık için yumuşak "şlop".
- Rolün ismi sahibine yeşil renkle gösterilir, diğer oyunculara rol bilgisi sızdırılmaz.

## Lobi Ayarları Kataloğu

Tüm ayarlar yalnızca oda sahibi (Host) tarafından değiştirilebilir. Değişiklikler "Uygula ve Herkese Senkronize Et" ile, ayrıca yeni bir oyuncu katıldığında ve oyun başlarken otomatik olarak tüm oyunculara gönderilir.

### Genel ayarlar

| Ayar | Varsayılan | Aralık |
| --- | --- | --- |
| ViShape Açık / Kapalı | Açık | Açık - Kapalı |
| Maksimum ViShape sayısı | 1 | 1 - 3 |
| Rolün çıkma ihtimali | %100 | %10 - %100 |

### Şekil değiştirme

| Ayar | Varsayılan | Aralık |
| --- | --- | --- |
| Şekil değiştirme bekleme süresi | 20 sn | 5 - 60 sn |
| Şekil değiştirme süresi | 20 sn | 5 - 60 sn |
| Dönüşümde asit lekesi bırak | Açık | Açık - Kapalı |
| Kılıktayken ayak ışıması görünsün | Açık | Açık - Kapalı |

### Zehirli gaz

| Ayar | Varsayılan | Aralık |
| --- | --- | --- |
| Zehir (gaz) bekleme süresi | 30 sn | 10 - 90 sn |
| Gaz bulutu süresi | 10 sn | 3 - 30 sn |
| Gaz yarıçapı | 3 birim | 1 - 6 birim |
| Gazda Crewmate görüş çarpanı | 0 (tam kör) | 0 - 1 |
| Vent'e girince otomatik gaz | Açık | Açık - Kapalı |
| ViShape gazda görüşü azalsın | Açık | Açık - Kapalı |

### Asit erimesi

| Ayar | Varsayılan | Aralık |
| --- | --- | --- |
| Asit bekleme süresi | 30 sn | 10 - 90 sn |
| Asit erime menzili | 1,4 birim | 0,5 - 2,5 birim |
| Başlangıç asit yükü | 2 | 1 - 5 |
| Toplantıda geri gelen yük | 1 | 0 - 2 |
| Erime donma süresi | 1,5 sn | 0,5 - 3 sn |
| Asit havuzu kalma süresi | 25 sn | 5 - 60 sn |
| Havuzda hız çarpanı | 0,6 | 0,2 - 1 |
| Toplantıda "ERİDİ" damgası | Açık | Açık - Kapalı |

### Denge ayarları

| Ayar | Varsayılan | Aralık |
| --- | --- | --- |
| Asitten sonra kılık kilidi | 10 sn | 0 - 30 sn |
| Diğer Impostor'lar gazdan etkilensin | Kapalı | Açık - Kapalı |
| Kamera ve admin tablosunda gaz görünsün | Açık | Açık - Kapalı |

### Hazır ayar paketleri

| Paket | Hedef kitle | Önemli değerler |
| --- | --- | --- |
| Kolay | Yeni oyuncular | Gaz 15 sn, asit yükü 3, bekleme süreleri 20 sn |
| Standart | Önerilen | Yukarıdaki varsayılan değerler |
| Zor | Dedektif lobileri | Tek yük, bekleme 45 sn, havuz 40 sn, ayak ışıması açık |

### Test kontrol listesi

1. Gaz her odayı dolduruyorsa ve oyun "sis oyununa" dönüyorsa gaz bekleme süresini artırın.
2. İki yükle oyun çok hızlı bitiyorsa başlangıç yükünü 1'e indirin.
3. Oyuncular havuzları fark etmiyorsa havuz parlaklığını ve kalma süresini artırın.
4. 5-6 kişilik küçük lobilerde tek ViShape fazla güçlü kalıyorsa rol çıkma ihtimalini düşürün.
5. Host dışındaki bir oyuncuyla ayar eşitlemesini, yeni katılan oyuncunun doğru değerleri almasını ve oyun sonunda tüm durumun sıfırlanmasını mutlaka doğrulayın.
