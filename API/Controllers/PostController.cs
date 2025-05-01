using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // ✅ GET /api/posts/{slug}/authors?lang=tr

        [HttpGet("{slug}/authors")]
        public async Task<IActionResult> GetArticleAuthors(string slug, [FromQuery] string lang)
        {
            var result = await _postService.GetArticleAuthorsAsync(slug, lang);
            return Ok(result);
        }


        // 📌 Header verisi
        [HttpGet("{slug}/header")]
        public async Task<IActionResult> GetPostHeader(string slug, [FromQuery] string lang = "tr")
        {
            var result = await _postService.GetPostHeaderAsync(slug, lang);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{slug}/related")]
        public async Task<IActionResult> GetRelatedArticles(string slug, [FromQuery] string lang)
        {
            var result = await _postService.GetRelatedArticlesAsync(slug, lang);
            return Ok(result);
        }

        [HttpGet("{slug}/sidebar")]
        public async Task<IActionResult> GetArticleSidebar(string slug, [FromQuery] string lang)
        {
            var result = await _postService.GetPostSidebarAsync(slug, lang);
            return Ok(result);
        }
        [HttpGet("{slug}/content")]
        public async Task<IActionResult> GetArticleContent(string slug, [FromQuery] string lang)
        {
            var result = await _postService.GetPostContentAsync(slug, lang);
            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestArticles([FromQuery] string lang = "tr")
        {
            var result = await _postService.GetLatestArticlesAsync(lang);
            return Ok(result);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadPdf(int id, [FromQuery] string lang = "tr")
        {
            var file = await _postService.GetPostPdfByLanguageAsync(id, lang);

            if (file == null)
                return NotFound("İlgili dile ait PDF dosyası bulunamadı.");

            return File(file.Content, file.ContentType, file.FileName);
        }


        // 🌍 Mock veri - Dil bazlı makale içerikleri
        private static readonly Dictionary<string, Dictionary<string, object>> Articles = new()
        {
            ["future-of-education"] = new()
            {
                ["tr"] = new
                {
                    Title = "Eğitimin Geleceği",
                    Date = "2025-03-15",
                    Authors = new[] { "Ahmet Yılmaz", "Mehmet Demir" },
                    Content = "<p>Geleceğin eğitim sistemleri, teknolojinin gelişmesiyle birlikte büyük bir dönüşüm geçirmektedir. Dijitalleşme, yapay zeka, sanal ve artırılmış gerçeklik gibi yenilikçi teknolojiler, eğitim süreçlerini daha etkili, erişilebilir ve kişiselleştirilmiş hale getirmektedir.</p>" +
    "<p>Geleneksel eğitim yöntemleri yerini, öğrencinin kendi hızında öğrenmesine olanak tanıyan adaptif öğrenme sistemlerine bırakmaktadır. Yapay zeka destekli eğitim platformları, öğrencinin eksik kaldığı konuları belirleyerek ona özel içerikler sunmaktadır. Böylece her öğrenci, bireysel öğrenme stiline uygun şekilde ilerleyebilmektedir.</p>" +
    "<p>Ayrıca, metaverse ve sanal gerçeklik teknolojileri, uzaktan eğitimde yeni bir çağ başlatmaktadır. Öğrenciler, sınıf ortamını fiziksel olarak paylaşmadan, sanal sınıflarda etkileşimli dersler alabilmektedir. Özellikle bilim, mühendislik ve tıp alanlarında, sanal laboratuvarlar sayesinde pratik deneyim kazanmak mümkün hale gelmektedir.</p>" +
    "<p>Bununla birlikte, eğitimde dijitalleşmenin getirdiği avantajların yanı sıra bazı zorluklar da bulunmaktadır. Dijital okuryazarlık eksikliği, ekonomik eşitsizlikler ve veri gizliliği konuları, geleceğin eğitim sistemleri için aşılması gereken engeller arasında yer almaktadır. Ancak, eğitim politikalarının ve teknolojik gelişmelerin doğru şekilde yönetilmesiyle, daha kapsayıcı ve sürdürülebilir bir eğitim modeline ulaşmak mümkündür.</p>" +
    "<p>Sonuç olarak, gelecekte eğitim sadece bilgi aktarmaktan ibaret olmayacak, öğrencilerin eleştirel düşünme, yaratıcılık ve problem çözme gibi becerilerini geliştirmeye odaklanacaktır. Teknoloji destekli öğrenme ortamları, eğitimin sınırlarını genişleterek her bireyin potansiyelini en üst düzeye çıkarmasına yardımcı olacaktır.</p>",

                    ReadTime = 10,
                    Views = 4200,
                    Category = "Eğitim",
                    Tags = new[] { "Eğitim", "Teknoloji", "Yapay Zeka" }
                },
                ["en"] = new
                {
                    Title = "Future of Education",
                    Date = "2025-03-15",
                    Authors = new[] { "John Doe", "Jane Smith" },
                    Content = "<p>The education systems of the future are undergoing a major transformation with the advancement of technology. Innovative technologies such as digitalization, artificial intelligence, virtual and augmented reality are making educational processes more effective, accessible, and personalized.</p>" +
    "<p>Traditional teaching methods are being replaced by adaptive learning systems that allow students to learn at their own pace. AI-powered education platforms identify areas where students struggle and provide personalized content tailored to their needs. This ensures that each student progresses according to their individual learning style.</p>" +
    "<p>Moreover, metaverse and virtual reality technologies are ushering in a new era of distance learning. Students can participate in interactive lessons in virtual classrooms without physically sharing a space. In fields such as science, engineering, and medicine, virtual laboratories provide students with hands-on experience in a safe environment.</p>" +
    "<p>However, while digitalization in education offers numerous benefits, it also presents certain challenges. Issues such as digital literacy gaps, economic inequalities, and data privacy concerns are among the barriers that must be addressed in future education systems. Nevertheless, with the right management of educational policies and technological advancements, a more inclusive and sustainable education model can be achieved.</p>" +
    "<p>Ultimately, the future of education will not be solely about transmitting knowledge but rather about fostering critical thinking, creativity, and problem-solving skills. Technology-driven learning environments will expand the boundaries of education, helping each individual reach their full potential.</p>",
                    ReadTime = 10,
                    Views = 4200,
                    Category = "Education",
                    Tags = new[] { "Education", "Technology", "AI" }
                },
                ["de"] = new
                {
                    Title = "Zukunft der Bildung",
                    Date = "2025-03-15",
                    Authors = new[] { "Hans Müller", "Lisa Schneider" },
                    Content = "<p>Die Bildungssysteme der Zukunft durchlaufen mit der Weiterentwicklung der Technologie eine große Transformation. Innovative Technologien wie Digitalisierung, künstliche Intelligenz, virtuelle und erweiterte Realität machen Bildungsprozesse effektiver, zugänglicher und individueller.</p>" +
    "<p>Traditionelle Lehrmethoden werden durch adaptive Lernsysteme ersetzt, die es den Schülern ermöglichen, in ihrem eigenen Tempo zu lernen. KI-gestützte Bildungsplattformen identifizieren die Bereiche, in denen Schüler Schwierigkeiten haben, und bieten personalisierte Inhalte, die auf ihre Bedürfnisse zugeschnitten sind. Dadurch kann jeder Schüler entsprechend seinem individuellen Lernstil vorankommen.</p>" +
    "<p>Darüber hinaus leiten Metaverse- und Virtual-Reality-Technologien eine neue Ära des Fernlernens ein. Schüler können an interaktiven Lektionen in virtuellen Klassenzimmern teilnehmen, ohne sich physisch im selben Raum zu befinden. In Bereichen wie Naturwissenschaften, Ingenieurwesen und Medizin bieten virtuelle Labore die Möglichkeit, praktische Erfahrungen in einer sicheren Umgebung zu sammeln.</p>" +
    "<p>Doch während die Digitalisierung im Bildungswesen zahlreiche Vorteile bietet, gibt es auch Herausforderungen. Probleme wie digitale Alphabetisierungslücken, wirtschaftliche Ungleichheiten und Datenschutzbedenken gehören zu den Hindernissen, die in zukünftigen Bildungssystemen überwunden werden müssen. Mit der richtigen Steuerung von Bildungspolitik und technologischen Fortschritten kann jedoch ein inklusiveres und nachhaltigeres Bildungsmodell erreicht werden.</p>" +
    "<p>Letztendlich wird die Zukunft der Bildung nicht nur darin bestehen, Wissen zu vermitteln, sondern vielmehr darin, kritisches Denken, Kreativität und Problemlösungsfähigkeiten zu fördern. Technologiegestützte Lernumgebungen werden die Grenzen der Bildung erweitern und jedem Einzelnen helfen, sein volles Potenzial auszuschöpfen.</p>",
                    ReadTime = 10,
                    Views = 4200,
                    Category = "Bildung",
                    Tags = new[] { "Bildung", "Technologie", "KI" }
                }
            },

            ["philosophy-of-mind"] = new()
            {
                ["tr"] = new
                {
                    Title = "Zihin Felsefesi",
                    Date = "2025-03-12",
                    Authors = new[] { "Elif Kaya", "Hakan Koç" },
                    Content = "<p>Zihin, bilinç ve düşünce üzerine...</p>",
                    ReadTime = 15,
                    Views = 3100,
                    Category = "Felsefe",
                    Tags = new[] { "Felsefe", "Bilinç", "Zihin" }
                },
                ["en"] = new
                {
                    Title = "Philosophy of Mind",
                    Date = "2025-03-12",
                    Authors = new[] { "Alan Watts", "Daniel Dennett" },
                    Content = "<p>On mind, consciousness, and thought...</p>",
                    ReadTime = 15,
                    Views = 3100,
                    Category = "Philosophy",
                    Tags = new[] { "Philosophy", "Consciousness", "Mind" }
                },
                ["de"] = new
                {
                    Title = "Philosophie des Geistes",
                    Date = "2025-03-12",
                    Authors = new[] { "Friedrich Nietzsche", "Martin Heidegger" },
                    Content = "<p>Über Geist, Bewusstsein und Denken...</p>",
                    ReadTime = 15,
                    Views = 3100,
                    Category = "Philosophie",
                    Tags = new[] { "Philosophie", "Bewusstsein", "Geist" }
                }
            }
        };

        [HttpGet("{slug}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult GetArticle(string slug, [FromQuery] string lang = "tr")
        {
            if (!Articles.ContainsKey(slug))
            {
                return NotFound(new { message = "Makale bulunamadı." });
            }

            var articleData = Articles[slug].GetValueOrDefault(lang, Articles[slug]["tr"]); // Eğer belirtilen dil yoksa Türkçe döndür
            return Ok(articleData);
        }
    }

}
