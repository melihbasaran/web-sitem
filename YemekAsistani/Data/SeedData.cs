using Microsoft.EntityFrameworkCore;
using YemekAsistani.Models;

namespace YemekAsistani.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Recipes.Any())
                {
                    return;   
                }

                context.Recipes.AddRange(
                    new Recipe
                    {
                        Title = "Karnıyarık",
                        // BURAYA ARTIK UZUN TARİF YAZIYORUZ:
                        Description = "Patlıcanları çizgili soyup, yarım saat yağ çekmemesi için tuzlu suda bekletin. İyice yıkadıktan sonra suyunu havlu ile çektirin ve az yağda kızartın. 3 adet biberi de yağda kızartın. Daha sonra aynı tavada doğranmış soğanları kavurun, kıymayı ekleyerek bir müddet daha kavurun ve biberleri, sarımsağı ekleyerek 2 dakika daha kavurun. Küp küp doğramış olduğunuz 2 adet domatesi, tuzu, baharatları ekleyerek karıştırın. Üzerine bir çay bardağı su ekleyerek 5 dk kaynatın. Tepsiye patlıcanların ortalarını keserek yerleştirin ve bu kesiklerden patlıcanın içine bastırarak iç malzemesine yer açın ve malzeme ile patlıcanları doldurun. Doldurduğunuz patlıcanların üzerine ortadan ikiye kestiğiniz çeri domatesi ya da 1 adet domatesi eşit büyüklükte olacak şekilde paylaştırın ve kızarttığımız biberlerden birer tane koyun. Ayrı bir yerde 1 kaşık salçayı, 1 su bardağı sıcak suda ezerek patlıcanların aralarına dökün. Kıymalar çıkmasın diye üzerine dökmeyin. Daha sonra 170 derece de ısıttığınız fırına sürerek 20-25 dk pişirin. Dilerseniz bu işlemi pilav tenceresi gibi bir tencerede ocakta yapabilirsiniz. Aynı sürede tencerede de pişecektir. Afiyet olsun..",
                        PrepTime = 45,
                        Servings = 4,
                        Ingredients = "Patlıcan, Kıyma, Domates, Biber, Soğan, Sarımsak, Salça"
                    },

                    new Recipe
                    {
                        Title = "Mercimek Çorbası",
                        Description = "Kırmızı mercimek çorbası için sıvı yağı tencereye alınarak yemeklik doğranan soğanlar hafif pembeleşinceye kadar kavrulur. Daha sonra un ilave edilerek kısık ateşte kavurmaya devam edilir. Salça kullanılacak ise salça ilave edilir, kavrulduktan sonra küp küp doğranmış havuç ve iyice yıkanıp suyu süzülen mercimekler ilave edilir. Üzerine su eklenerek karıştırılır ve tencerenin kapağı kapatılır. Çorbamız kaynayana kadar orta ateşte, kaynadıktan sonra mercimekler ve havuçlar yumuşayana kadar ara ara karıştırılarak kısık ateşte pişirilir. Çorba piştikten sonra el blenderı ile güzelce ezilir. Eğer blenderiniz yoksa süzgeçten de geçirebilirsiniz. Karabiber, tuz ve isteğe bağlı olarak kimyon eklenir ve karıştırılır. 5 dakika daha pişirilerek ocaktan alınır. Kıvamı koyu gelirse size, bir miktar su ilave edilerek bir taşım kaynatılır. Bu arada küçük bir tavaya iki yemek kaşığı tereyağı alınır, kızdırılır ve bir tatlı kaşığı kırmızı toz biber eklenerek ocaktan alınır. Mercimek çorbası servis kasesine alındıktan sonra üzerine kırmızı biberli sos gezdirilir ve bir dilim limon ile servis edilir.",
                        PrepTime = 30,
                        Servings = 6,
                        Ingredients = "Mercimek, Soğan, Havuç, Patates, Su, Tuz, Yağ"
                    },

                    new Recipe
                    {
                        Title = "Menemen",
                        Description = "Sıvı yağı ve biberleri tavaya alarak biberlerin rengi dönünceye kadar kavurun. Üzerine kabukları soyulup küçük küçük doğranmış domatesleri ilave edin. Kısık ocakta tavanın kapağını kapatarak domateslerin iyice pişmesini bekleyin. Domatesler çok suyu değil, tavaya yapışıyorsa birazcık kaynar su ekleyebilirsiniz. Genellikle de bu duruma gerek kalmayacaktır. Fotoğraftaki gibi domatesler piştikten sonra yumurtaları kırabilirsiniz. Yumurtaları ister ayrı bir kapta çırpıp ekleyin isterseniz de benim gibi tavaya kırıp tavada karıştırabilirsiniz. Üzerine tuz ve dilediğiniz baharatları ekleyerek yumurtalar pişene kadar bekleyin. Kaşar peyniri eklemek istiyorsanız bu aşamada peynirleri de ilave edebilirsiniz. Menemeni sıcak olarak servis yapın.",
                        PrepTime = 15,
                        Servings = 2,
                        Ingredients = "Yumurta, Domates, Biber, Tuz, Yağ"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}